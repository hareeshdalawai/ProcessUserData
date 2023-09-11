using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Moq;
using Process.UserData.FunctionApp.Domain.Models;
using Process.UserData.FunctionApp.Infrastructure.Repository;
using System.Text.Json;

namespace Process.UserData.FunctionApp.Infrastructure.Test.Repository
{
    [TestClass]
    public class MessagingRepositoryTests
    {
        [TestMethod]
        public async Task MessagingRepository_Test_Success()
        {
            var messagingRepositoryStub = new MessagingRepositoryStub();

            var message = new NotificationMessage { CorrelationId = "correlation_id", UserId = 1, UserName = "user 1", Email = "user1@abc.com", DataValue ="Some value" };
            await messagingRepositoryStub.messagingRepository.SendToNotificationQueue(message);

            messagingRepositoryStub.serviceBusClientMock.Verify(mock => mock.CreateSender("notification-queue"), Times.Once);
            messagingRepositoryStub.serviceBusSenderMock.Verify(mock => mock.CloseAsync(It.IsAny<CancellationToken>()), Times.Once);

            ValidateMessagePayload(messagingRepositoryStub.sentMessage, message);
            ValidateLogging(messagingRepositoryStub);
        }

        [TestMethod]
        public async Task MessagingRepository_Test_Failed()
        {
            var messagingRepositoryStub = new MessagingRepositoryStub(new ServiceBusException());

            var message = new NotificationMessage { CorrelationId = "correlation_id", UserId = 1, UserName = "user 1", Email = "user1@abc.com", DataValue = "Some value" };

            await Assert.ThrowsExceptionAsync<ServiceBusException>(async () => await messagingRepositoryStub.messagingRepository.SendToNotificationQueue(message));

            messagingRepositoryStub.serviceBusClientMock.Verify(mock => mock.CreateSender("notification-queue"), Times.Once);
            messagingRepositoryStub.serviceBusSenderMock.Verify(mock => mock.CloseAsync(It.IsAny<CancellationToken>()), Times.Once);

            ValidateMessagePayload(messagingRepositoryStub.sentMessage, message);
            ValidateLogging(messagingRepositoryStub);
        }

        [TestMethod]
        public async Task MessagingRepository_Test_ServiceBus_Connection_Closed()
        {
            var messagingRepositoryStub = new MessagingRepositoryStub(null, senderConnectionClosed: true);

            var message = new NotificationMessage { CorrelationId = "correlation_id", UserId = 1, UserName = "user 1", Email = "user1@abc.com", DataValue = "Some value" };

            await messagingRepositoryStub.messagingRepository.SendToNotificationQueue(message);

            messagingRepositoryStub.serviceBusSenderMock.Verify(mock => mock.CloseAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        private static void ValidateMessagePayload(ServiceBusMessage sentMessage, NotificationMessage message)
        {
            Assert.AreEqual(message.CorrelationId, sentMessage.MessageId);
            Assert.AreEqual("application/json", sentMessage.ContentType);

            var expectedBody = JsonSerializer.Serialize(GetNotificationMessage());
            Assert.AreEqual(expectedBody, sentMessage.Body.ToString());
        }

        private static void ValidateLogging(MessagingRepositoryStub messagingRepositoryStub)
        {
            var expectedMessage = $"Sending notification message to queue messageId = [correlation_id], message = [{messagingRepositoryStub.sentMessage.Body}]";
            messagingRepositoryStub.loggerMock.Verify(mock =>
            mock.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((obj, type) => string.Equals(expectedMessage, obj.ToString())),
                null,
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);
        }

        private static NotificationMessage GetNotificationMessage()
        {
            return new NotificationMessage
            {
                CorrelationId = "correlation_id",
                UserId = 1,
                UserName = "user 1",
                Email = "user1@abc.com",
                DataValue = "Some value"
            };
        }

        public class MessagingRepositoryStub
        {
            public ServiceBusMessage sentMessage;
            public Mock<ServiceBusSender> serviceBusSenderMock;
            public Mock<ServiceBusClient> serviceBusClientMock;
            public MessagingRepository messagingRepository;
            public Mock<ILogger> loggerMock;

            public MessagingRepositoryStub(Exception exceptionToThrow = null, bool senderConnectionClosed = false)
            {
                sentMessage = new ServiceBusMessage();

                serviceBusSenderMock = GetServiceBusSenderMock((message, token) => { sentMessage = message; }, exceptionToThrow);
                serviceBusSenderMock.SetupGet(mock => mock.IsClosed).Returns(senderConnectionClosed);
                serviceBusClientMock = GetServiceBusClientMock(serviceBusSenderMock);
                loggerMock = new Mock<ILogger>();

                messagingRepository = new MessagingRepository(serviceBusClientMock.Object, loggerMock.Object);
            }

            private static Mock<ServiceBusClient> GetServiceBusClientMock(Mock<ServiceBusSender> serviceBusSenderMock)
            {
                var clientMock = new Mock<ServiceBusClient>();
                clientMock.Setup(mock => mock.CreateSender(It.IsAny<string>())).Returns(serviceBusSenderMock.Object);
                return clientMock;
            }

            private static Mock<ServiceBusSender> GetServiceBusSenderMock(Action<ServiceBusMessage, CancellationToken> callbackAction, Exception? exception)
            {
                var senderMock = new Mock<ServiceBusSender>();
                if (exception != null)
                {
                    senderMock.Setup(mock => mock.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                                    .Callback(callbackAction)
                                    .Throws(exception);
                }
                else
                {
                    senderMock.Setup(mock => mock.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                                    .Callback(callbackAction);
                }
                return senderMock;
            }
        }
    }
}
