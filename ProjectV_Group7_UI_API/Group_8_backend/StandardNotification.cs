// using System;

// namespace EmergencyServices.Group8
// {
//     internal class Notification // This is an exact match of the Notification issued to us by the NWS, which 
//     {
//         public int Id { get; set; }
//         public string DisasterType { get; set; }
//         public string Priority { get; set; }  // Watch, Warning, Urgent, Critical
//         public string Description { get; set; }
//         public DateTime Timestamp { get; set; }
//         public double? SeverityLevel { get; set; }  // E.g., Rainfall in mm, Hurricane category
//         public string Source { get; set; }  // NWS or Emergency Services

//         public override string ToString()
//         {
//             return Id + " " + DisasterType + " " + Description + " " + Timestamp.ToString() + " " + SeverityLevel + " " + Source + "\n";
//         }
//     }
// }
