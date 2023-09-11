namespace Process.UserData.FunctionApp.Domain.Notification
{
    /// <summary>
    /// Provides methods to access notification infrastructure
    /// </summary>
    public interface INotificationService
    {
        void SendNotificationMessage(DateTime lastExecutionTime);
    }
}
