using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using Process.UserData.FunctionApp.Domain.Notification;

namespace Process.UserData.FunctionApp.Tests
{
    [TestClass]
    public class ProcessUserDataTests
    {
        [TestMethod]
        public void ProcessUserData_Test_Initial_Trigger()
        {
            var notificationService = new Mock<INotificationService>();
            var loggerMock = new Mock<ILogger>();

            var processData = new ProcessUserData(notificationService.Object, loggerMock.Object);

            processData.ProcessData(new TimerInfo { IsPastDue = false, ScheduleStatus = null });

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ProcessUserData_Test_Trigger()
        {
            var notificationService = new Mock<INotificationService>();
            var loggerMock = new Mock<ILogger>();

            var processData = new ProcessUserData(notificationService.Object, loggerMock.Object);

            processData.ProcessData(new TimerInfo { IsPastDue = false, ScheduleStatus = new ScheduleStatus { Last = DateTime.Now.AddMinutes(-15), LastUpdated = DateTime.Now, Next = DateTime.Now.AddMinutes(15)} });

            Assert.IsTrue(true);
        }
    }
}