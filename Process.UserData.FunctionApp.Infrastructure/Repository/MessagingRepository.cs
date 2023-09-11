using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Process.UserData.FunctionApp.Domain.Interfaces;
using Process.UserData.FunctionApp.Domain.Models;
using System.Text.Json;

namespace Process.UserData.FunctionApp.Infrastructure.Repository
{
    /// <summary>
    /// Implements methods for accessing service bus queues.
    /// </summary>
    public class MessagingRepository : IMessagingRepository
    {
        private readonly string _transactionRequestQueueName = "notification-queue";
        private readonly ServiceBusSender _serviceBusSender;
        private readonly ILogger _logger;

        public MessagingRepository(ServiceBusClient serviceBusClient, ILogger logger)
        {
            _serviceBusSender = serviceBusClient.CreateSender(_transactionRequestQueueName);
            _logger = logger;
        }

        public async Task SendToNotificationQueue(NotificationMessage userDetails)
        {
            try
            {
                var message = GetMessage(userDetails);

                LogNotificationMessage(message);
                await _serviceBusSender.SendMessageAsync(message);
            }
            finally
            {
                if (!_serviceBusSender.IsClosed)
                {
                    await _serviceBusSender.CloseAsync();
                }
            }
        }

        private ServiceBusMessage GetMessage(NotificationMessage notificationMessage)
        {
            var userDetailsJson = JsonSerializer.Serialize(notificationMessage);

            var message = new ServiceBusMessage(userDetailsJson)
            {
                ContentType = "application/json",
                MessageId = notificationMessage.CorrelationId
            };
            return message;
        }

        private void LogNotificationMessage(ServiceBusMessage serviceBusMessage)
        {
            const string logMessage = "Sending notification message to queue messageId = [{notificationMessageId}], message = [{notificationMessage}]";
            var notificationMessage = serviceBusMessage.Body.ToString();

            _logger.LogInformation(logMessage, serviceBusMessage.MessageId, notificationMessage);
        }
    }
}
