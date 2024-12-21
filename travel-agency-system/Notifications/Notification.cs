namespace travel_agency_system.Notifications;

public class Notification
{
    public Notification(string recieverId, string content)
    {
        this.recieverId = recieverId;
        this.content = content;
    }
    public string recieverId { get; set; }
    public string content { get; set; }
}