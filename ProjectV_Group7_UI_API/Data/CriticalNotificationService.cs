public class CriticalNotificationService
{
    private List<Critical_Notification> notifications;

    public CriticalNotificationService()
    {
        // Sample data for demonstration; replace with real data retrieval.
        notifications = new List<Critical_Notification>
        {
            new Critical_Notification { Date = DateTime.Now.AddHours(-1), Recipient = "Alice", Type = "Medical Emergency Request", Status = "Delivered", Priority = "High" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-2), Recipient = "911 Dispatcher", Type = "Traffic Accident", Status = "Pending", Priority = "Very High" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-3), Recipient = "EMS Paramedic Unit", Type = "Overdose", Status = "In Progress", Priority = "High" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-4), Recipient = "Public Utility Department", Type = "Power Grid Failure Alert", Status = "Delivered", Priority = "Medium" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-5), Recipient = "EMS", Type = "Alert", Status = "Pending", Priority = "Low" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-6), Recipient = "911 Dispatcher", Type = "Traffic Accident", Status = "In Progress", Priority = "High" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-7), Recipient = "Public Utility Department", Type = "Power Grid Failure Alert", Status = "Delivered", Priority = "Very High" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-8), Recipient = "EMS Paramedic Unit", Type = "Medical Emergency Request", Status = "Pending", Priority = "Medium" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-9), Recipient = "EMS", Type = "Overdose", Status = "Delivered", Priority = "High" },
            new Critical_Notification { Date = DateTime.Now.AddHours(-10), Recipient = "911 Dispatcher", Type = "Alert", Status = "In Progress", Priority = "Very High" }
        };
    }

    public List<Critical_Notification> GetNotifications()
    {
        return notifications;
    }
}
