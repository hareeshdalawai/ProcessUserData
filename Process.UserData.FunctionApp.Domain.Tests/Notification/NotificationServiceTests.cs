using AutoMapper;
using Moq;
using Process.UserData.FunctionApp.Domain.Interfaces;
using Process.UserData.FunctionApp.Domain.Mapping;
using Process.UserData.FunctionApp.Domain.Models;
using Process.UserData.FunctionApp.Domain.Notification;

namespace Process.UserData.FunctionApp.Domain.Tests.Notification
{
    [TestClass]
    public class NotificationServiceTests
    {
        private IMapper _mapper;

        [TestInitialize()]
        public void SetupMapperConfiguration()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            });

            _mapper = mockMapper.CreateMapper();
        }

        [TestMethod]
        public void NotificationService_Test_SendNotificationMessage_EmptyList()
        {
            var lastExecutionTime = DateTime.Now.AddMinutes(-15);

            var dataRepositoryMock = new Mock<IDataRepository>();
            var messagingRepositoryMock = new Mock<IMessagingRepository>();

            dataRepositoryMock.Setup(x => x.GetUsers(It.IsAny<DateTime>())).Returns(new List<User>());

            var notificationService = new NotificationService(dataRepositoryMock.Object, messagingRepositoryMock.Object, _mapper);

            notificationService.SendNotificationMessage(lastExecutionTime);
            
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void NotificationService_Test_SendNotificationMessage_Success()
        {
            var lastExecutionTime = DateTime.Now.AddMinutes(-15);

            var updatedUsers = new List<User>
            { 
                new User { RecordId = 1, UserId= 1, UserName = "user 1", Email = "user1@abc.com", DataValue = "Some value 1", NotificationFlag = true, CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now } ,
                new User { RecordId = 2, UserId= 2, UserName = "user 2", Email = "user2@abc.com", DataValue = "Some value 2", NotificationFlag = true, CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now } 
            };

            var dataRepositoryMock = new Mock<IDataRepository>();
            var messagingRepositoryMock = new Mock<IMessagingRepository>();

            dataRepositoryMock.Setup(x => x.GetUsers(It.IsAny<DateTime>())).Returns(updatedUsers);

            var notificationService = new NotificationService(dataRepositoryMock.Object, messagingRepositoryMock.Object, _mapper);

            notificationService.SendNotificationMessage(lastExecutionTime);

            Assert.IsTrue(true);
        }
    }
}
