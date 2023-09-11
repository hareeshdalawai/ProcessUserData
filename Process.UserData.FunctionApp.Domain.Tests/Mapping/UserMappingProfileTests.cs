using AutoMapper;
using Process.UserData.FunctionApp.Domain.Mapping;
using Process.UserData.FunctionApp.Domain.Models;

namespace Process.UserData.FunctionApp.Domain.Tests.Mapping
{
    [TestClass]
    public class UserMappingProfileTests
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
        public void UserMappingProfile_Test_Map_User_To_NotificationMessage()
        {
            var user = new User
            {
                RecordId = 1,
                UserId = 1,
                UserName = "User 1",
                Email = "user1@abc.com",
                NotificationFlag = false,
                DataValue = "Value",
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now                
            };

            var notificationMessage = _mapper.Map<NotificationMessage>(user);

            Assert.IsNotNull(notificationMessage);
            Assert.AreEqual(user.UserId, notificationMessage.UserId);
            Assert.AreEqual(user.UserName, notificationMessage.UserName);
            Assert.AreEqual(user.DataValue, notificationMessage.DataValue);            
            Assert.AreEqual(user.Email, notificationMessage.Email);
            Assert.AreEqual(string.Empty, notificationMessage.CorrelationId);
        }

        [TestMethod]
        public void UserMappingProfile_Test_Map_User_To_NotificationMessage_List()
        {
            var users = new List<User>
            {
                new User { RecordId = 1, UserId= 1, UserName = "user 1", Email = "user1@abc.com", DataValue = "Some value 1", NotificationFlag = true, CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now } ,
                new User { RecordId = 2, UserId= 2, UserName = "user 2", Email = "user2@abc.com", DataValue = "Some value 2", NotificationFlag = true, CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now }
            };
            var notificationMessages = _mapper.Map<List<NotificationMessage>>(users);

            Assert.IsNotNull(notificationMessages);
            Assert.AreEqual(users.Count, notificationMessages.Count);
            Assert.AreEqual(users[1].UserId, notificationMessages[1].UserId);
            Assert.AreEqual(users[1].UserName, notificationMessages[1].UserName);
            Assert.AreEqual(users[1].DataValue, notificationMessages[1].DataValue);
            Assert.AreEqual(users[1].Email, notificationMessages[1].Email);
            Assert.AreEqual(string.Empty, notificationMessages[1].CorrelationId);
        }
    }
}
