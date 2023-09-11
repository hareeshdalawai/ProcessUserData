using Process.UserData.FunctionApp.Domain.Models;

namespace Process.UserData.FunctionApp.Domain.Interfaces
{
    /// <summary>
    /// Provides methods for accessing service bus queues.
    /// </summary>
    public interface IMessagingRepository
    {
        Task SendToNotificationQueue(NotificationMessage userData);
    }
}
