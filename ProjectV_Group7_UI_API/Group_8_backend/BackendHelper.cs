using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyServices.Group8
{
    internal static class BackendHelper
    {
        public static List<ProcessingInfo> DisasterProcessingInfo;

        public static List<UserDisasterReport> UserDisasterReports = new List<UserDisasterReport>();

        private const ulong acceptedTimeDiffClauseOne = 36000000000; // currently single hour binary 

        private const ulong acceptedTimeDiffClauseTwo = 3000000000; // currently 5 minute binary
        public static Notification JsonToNotification(string json)
        {
            dynamic jsonContents = JObject.Parse(json);
            // *** NEW CODE TO SUPPORT HERE **
            NewNotificationFormat tmpNotif = new NewNotificationFormat();
            tmpNotif.id = jsonContents.id;
            tmpNotif.notiforigin = jsonContents.notiforigin;
            tmpNotif.longitude = jsonContents.longitude;
            tmpNotif.latitude = jsonContents.latitude;
            tmpNotif.city = jsonContents.city;
            tmpNotif.disastertype = jsonContents.disastertype;
            tmpNotif.disasterlevel = jsonContents.disasterlevel;
            tmpNotif.notifdate = jsonContents.notifdate;


            Notification newNotif = new Notification();
            newNotif.Id = tmpNotif.id;
            newNotif.Timestamp = DateTime.Now;
            newNotif.DisasterType = tmpNotif.disastertype;
            newNotif.Priority = MapIntToPriority(tmpNotif.disasterlevel);
            newNotif.Description = tmpNotif.city;
            newNotif.SeverityLevel = 0f;
            newNotif.Source = tmpNotif.notiforigin;
            newNotif.Latitude = tmpNotif.latitude; // NEW FIELD
            newNotif.Longitude = tmpNotif.longitude; // NEW FIELD
            return newNotif;
        }

        public static string MapIntToPriority(int val) // FOR USE WITH NEW NOTIF STRUCTURE
        {
            switch (val)
            {
                case 1:
                    return "Watch";
                case 2:
                    return "Warning";
                case 3:
                    return "Urgent";
                case 4:
                    return "Critical";
                default:
                    return "";
            }
        }

        public static int MapPriorityToInt(string val) // FOR USE WITH NEW NOTIF STRUCTURE
        {
            switch (val)
            {
                case "Watch":
                    return 1;
                case "Warning":
                    return 2;
                case "Urgent":
                    return 3;
                case "Critical":
                    return 4;
                default:
                    return 1;
            }
        }

        public static string ProcessedDisasterToJson(ProcessedDisaster p) // UNUSED NOW
        {
            return JsonConvert.SerializeObject(p);
        }

        public static async Task<bool> PopulateProcessingInfoList()
        {
            DisasterProcessingInfo = new List<ProcessingInfo>();
            var res = await EmergencyBackend.supabase.From<ProcessingInfo>().Get();
            var models = res.Models;
            if (models.Count == 0)
                return false;
            foreach (ProcessingInfo p in models)
            {
                DisasterProcessingInfo.Add(p);
            }
            return true;
        }

        internal static ProcessedDisaster ConvertToProcessedDisaster(Notification notif)
        {
            // Convert the notification's disaster type to uppercase for case-insensitive matching
            string notificationDisasterType = notif.DisasterType.ToUpper();

            ProcessingInfo matchingInfo = null;

            // Loop through each entry in DisasterProcessingInfo to find a match
            for (int i = 0; i < DisasterProcessingInfo.Count; i++)
            {
                // Retrieve the disaster type from the database, convert it to uppercase, and compare
                string databaseDisasterType = DisasterProcessingInfo[i].DisasterType.ToUpper();
                if (databaseDisasterType == notificationDisasterType)
                {
                    matchingInfo = DisasterProcessingInfo[i];
                    break;
                }
            }

            // Create a new ProcessedDisaster object and copy info from Notification
            var processedDisaster = new ProcessedDisaster
            {
                DisasterType = notif.DisasterType,
                Priority = notif.Priority,
                Description = notif.Description,
                SeverityLevel = notif.SeverityLevel ?? 0,
                Source = notif.Source,

                // NEW LINES BELOW FOR REVAMPED NOTIF FORMAT
                Latitude = notif.Latitude,
                Longitude = notif.Longitude
            };

            // Populate steps based on matching ProcessingInfo, or set to default steps if not found
            if (matchingInfo != null)
            {
                processedDisaster.PreparationSteps = matchingInfo.PrecautionSteps;
                processedDisaster.ActiveSteps = matchingInfo.DuringDisasterSteps;
                processedDisaster.RecoverySteps = matchingInfo.RecoverySteps;
            }
            else
            {
                processedDisaster.PreparationSteps = "Listen to your local news station, review and practice evacuation routes, and make sure that your home and belongings are secured";
                processedDisaster.ActiveSteps = "Watch for signs of a disaster and be prepared to evacuate or find proper shelter";
                processedDisaster.RecoverySteps = "Lookout for instructions from officials and community leaders, inspect your area for damages, listen to your local radio or news channel for further instructions";

            }

            return processedDisaster;
        }

        internal static bool VerifyUserReport(UserDisasterReport usrReport)
        {
            if (usrReport == null)
                return false;

            foreach (UserDisasterReport r in UserDisasterReports)
            {
                if ((r.user_id == usrReport.user_id && DateSeperation(usrReport.timestamp, r.timestamp, acceptedTimeDiffClauseOne) == false) || (String.Compare(r.weather_event_type.ToUpper(), usrReport.weather_event_type.ToUpper()) == 0 && DateSeperation(usrReport.timestamp, r.timestamp, acceptedTimeDiffClauseTwo) == false))
                    return false;
            }

            UserDisasterReports.Add(usrReport); // only add to list if approved in order to adhere to spam timing rules accurately 
            return true;
        }

        internal static bool DateSeperation(DateTime incoming, DateTime existing, ulong diff) // true if greater than time limit set
        {
            if (((ulong)incoming.ToBinary() - (ulong)existing.ToBinary()) > diff)
                return true;
            return false;
        }

        public static string GetProcessedDisasterJsonInNewNotifFormat(ProcessedDisaster procObj)
        {
            NewNotificationFormat newNotif = new NewNotificationFormat();
            newNotif.id = procObj.Id;
            newNotif.notiforigin = procObj.Source;
            newNotif.longitude = procObj.Longitude;
            newNotif.latitude = procObj.Latitude;
            newNotif.city = procObj.Description;
            newNotif.disastertype = procObj.DisasterType;
            newNotif.disasterlevel = MapPriorityToInt(procObj.Priority);
            newNotif.notifdate = procObj.Timestamp.ToString();

            // PROCESSED DISASTER SPECIFICS
            newNotif.preparationsteps = procObj.PreparationSteps;
            newNotif.activesteps = procObj.ActiveSteps;
            newNotif.recoverysteps = procObj.RecoverySteps;

            return JsonConvert.SerializeObject(newNotif);
        }
    }
}
