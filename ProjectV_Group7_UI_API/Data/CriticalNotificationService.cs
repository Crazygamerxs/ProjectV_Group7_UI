using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EmergencyServices.Group8;

public class CriticalNotificationService
{
    private List<Critical_Notification> notifications; // OLD CODE THAT IS NOT LINKED TO GROUP 8

    public CriticalNotificationService()
    {
        notifications = LoadNotifications();
    }

    private List<Critical_Notification> LoadNotifications()
    {
        if (!File.Exists("critical-notifications.json"))
            return new List<Critical_Notification>();

        var json = File.ReadAllText("critical-notifications.json");
        return JsonSerializer.Deserialize<List<Critical_Notification>>(json) ?? new List<Critical_Notification>();
    }

    public List<Critical_Notification> GetNotifications()
    {
        return notifications;
    }

    public void AddNotification(Critical_Notification notification)
    {
        notifications.Add(notification);
        SaveNotifications();
    }

    private void SaveNotifications()
    {
        var json = JsonSerializer.Serialize(notifications, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("critical-notifications.json", json);
    }
}
