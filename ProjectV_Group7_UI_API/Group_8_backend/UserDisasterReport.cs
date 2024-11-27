namespace EmergencyServices.Group8
{
    internal class UserDisasterReport
    {
        public string? report_id { get; set; }
        public int user_id { get; set; }
        public DateTime timestamp { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string? address { get; set; }
        public string? weather_event_type { get; set; }
        public string? weather_event_severity { get; set; }
        public string? weather_event_description { get; set; }
    }
}

