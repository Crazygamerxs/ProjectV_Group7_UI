// using System;

// namespace EmergencyServices.Group8
// {
//     internal class UserDisasterReport
//     {
//         public UserDisasterReport()
//         {
//             location = new Location();
//             @event = new Event();
//             media = new Media();
//         }
//         public int report_id { get; set; }
//         public int user_id { get; set; }
//         public DateTime created_at { get; set; }
//         public Location location { get; set; }
//         public Event @event { get; set; }
//         public Media media { get; set; }
//     }

//     internal class Location
//     {
//         public double latitude { get; set; }
//         public double longitude { get; set; }
//         public string address { get; set; }
//     }

//     internal class Event
//     {
//         public string type { get; set; }
//         public string severity { get; set; }
//         public string description { get; set; }
//     }

//     internal class Media
//     {
//         public string type { get; set; }
//         public string url { get; set; }
//         public string description { get; set; }
//     }
// }
