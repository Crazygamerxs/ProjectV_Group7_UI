using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Supabase;
using System.Diagnostics.CodeAnalysis;

namespace EmergencyServices.Group8
{
    public static class EmergencyBackend
    {
        public static Client supabase;
        public static async void Init()
        {
            DotNetEnv.Env.Load();

            var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
            var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            if (await BackendHelper.PopulateProcessingInfoList() == false)
            {
                Debug.WriteLine("ERROR: Failed to retrieve disaster processing data OR table was empty!"); // Change to console call if not in visual studio debug mode
                Environment.Exit(-1);
            }
        }

        public static ProcessedDisaster ProcessNotification(string notifJson)
        {
            if (notifJson == null)
                return null;
            Notification notif = BackendHelper.JsonToNotification(notifJson);

            ProcessedDisaster processedDisaster = BackendHelper.ConvertToProcessedDisaster(notif);
            var ret = supabase.From<ProcessedDisaster>().Insert(processedDisaster);
            return ret.Result.Model; // We need to return this since it has the correct creation time info AND the correct ID
        }

        public static async Task<List<ProcessedDisaster>> GetAllProcessedDisastersAsync()
        {
            try
            {
                //Query the 'disaster_processed' table
                var response = await supabase
                    .From<ProcessedDisaster>()   //Target the production table model
                    .Select("*")
                    .Get();

                return response.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving processed disasters from disaster_processed table: {ex.Message}");
                return new List<ProcessedDisaster>();
            }
        }

        public static async Task<List<ProcessedDisaster>> GetDisastersByPriorityAsync(DisasterTypeEnums priorityLevel)
        {
            try
            {
                string priorityAsString = priorityLevel.ToString();
                Debug.WriteLine($"Filtering production table for priority: {priorityAsString}");

                //Query the 'disaster_processed' table to filter by priority level
                var response = await supabase
                    .From<ProcessedDisaster>()    //Target the production model
                    .Select("*")
                    .Filter(x => x.Priority, Postgrest.Constants.Operator.Equals, priorityAsString)
                    .Get();

                return response.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving disasters with priority '{priorityLevel}' from production table: {ex.Message}");
                return new List<ProcessedDisaster>();
            }
        }

        public static async Task<bool> MarkDisasterAsCriticalAsync(int disasterId)
        {
            try
            {
                //Step 1: Retrieve the current disaster by ID
                var currentDisasterResponse = await supabase
                    .From<ProcessedDisaster>()
                    .Where(d => d.Id == disasterId)
                    .Get();

                var currentDisaster = currentDisasterResponse.Models.FirstOrDefault();

                //Step 2: Check if the current priority is 'Urgent'
                if (currentDisaster == null)
                {
                    Debug.WriteLine("Disaster not found.");
                    return false;
                }

                if (currentDisaster.Priority != "Urgent")
                {
                    Debug.WriteLine("Disaster priority is not 'Urgent', cannot update to 'Critical'.");
                    return false;
                }

                //Step 3: Update the priority to 'Critical'
                var updatedDisaster = new { Priority = "Critical" };

                var response = await supabase
                    .From<ProcessedDisaster>()
                    .Where(d => d.Id == disasterId)
                    .Set(d => d.Priority, "Critical")
                    .Update();

                //Returns true if at least one record was updated

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error marking disaster as critical: {ex.Message}");
                return false; //Return false if an error occurs
            }
        }

        public static async Task<string> GetNewNotifFormatProcessedDisasterJson(int id)
        {
            var currentDisasterResponse = await supabase
                    .From<ProcessedDisaster>()
                    .Where(d => d.Id == id)
                    .Get();

            ProcessedDisaster currentDisaster = currentDisasterResponse.Models.FirstOrDefault();
            return BackendHelper.GetProcessedDisasterJsonInNewNotifFormat(currentDisaster);
        }

        public static bool VerifyUserReport(string usrReportJson)
        {
            UserDisasterReport deSerializedReport = JsonConvert.DeserializeObject<UserDisasterReport>(usrReportJson);
            return BackendHelper.VerifyUserReport(deSerializedReport);

        }

        public static void AddNewSurvivor(Survivor_Information survivor) // Based on method found in G7 survior information service which accepted a survivor information object when adding to list
        {
            if (survivor != null)
                supabase.From<Survivor_Information>().Insert(survivor);
        }

        public static async Task<List<Survivor_Information>> GetSurvivors() // Name does not follow similar methods defined here (like getting disasters) because G7 already has method with same functionality as here that we want to integrate more easily, so we are keeping this name (for now)
        {
            try
            {
                //Query the 'disaster_processed' table
                var response = await supabase
                    .From<Survivor_Information>()   //Target the production table model
                    .Select("*")
                    .Get();

                return response.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving processed disasters from disaster_processed table: {ex.Message}");
                return new List<Survivor_Information>();
            }
        }

    }
}
