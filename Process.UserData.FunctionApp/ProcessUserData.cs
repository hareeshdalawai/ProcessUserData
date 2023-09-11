using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Process.UserData.FunctionApp.Domain.Notification;

namespace Process.UserData.FunctionApp
{
    public class ProcessUserData
    {
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;

        public ProcessUserData(INotificationService notificationService, ILogger logger)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [Function("ProcessData")]
        public void ProcessData([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer)
        {
            DateTime lastExecutionTime = myTimer.ScheduleStatus != null ? myTimer.ScheduleStatus.Last : DateTime.Now.AddMinutes(-15);

            _logger.LogInformation($"Timer trigger function executed at: {DateTime.Now}");

            _notificationService.SendNotificationMessage(lastExecutionTime);
        }
    }
}