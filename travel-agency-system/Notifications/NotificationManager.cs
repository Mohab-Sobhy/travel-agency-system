using travel_agency_system.Models;

namespace travel_agency_system.Notifications;

public class NotificationManager
{
    private Queue<Notification> NotificationQueue = new Queue<Notification>();
    private readonly object queueLock = new object();
    private bool isRunning = true;
    private int timeToCheckQueue = 30000;

    // Add a notification to the queue
    public void AddNotification(Notification notification)
    {
        lock (queueLock)
        {
            NotificationQueue.Enqueue(notification);
        }
    }

    // Start the thread to send notifications
    public void StartNotificationSender()
    {
        Thread notificationThread = new Thread(() =>
        {
            while (isRunning)
            {
                Notification notificationToSend = null;

                lock (queueLock)
                {
                    if (NotificationQueue.Count > 0)
                    {
                        notificationToSend = NotificationQueue.Dequeue();
                    }
                }

                if (notificationToSend != null)
                {
                    Console.WriteLine($"Sending to: {notificationToSend.recieverId}, Message: {notificationToSend.content}");
                }

                Thread.Sleep(timeToCheckQueue); // Wait for 30 seconds
            }
        });

        notificationThread.IsBackground = true; // Ensure thread stops when the application exits
        notificationThread.Start();
    }

    // Stop the notification sender thread
    public void StopNotificationSender()
    {
        isRunning = false;
    }
}