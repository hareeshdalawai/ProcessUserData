using Microsoft.Extensions.Logging;
using Moq;
using Process.UserData.FunctionApp.Domain.Models;
using Process.UserData.FunctionApp.Infrastructure.Context;
using Process.UserData.FunctionApp.Infrastructure.Repository;

namespace Process.UserData.FunctionApp.Infrastructure.Test.Repository
{
    [TestClass]
    public class DataRepositoryTests
    {
        [TestMethod]
        public void  DataRepository_Test_GetUsers_Return_Empty_List()
        {
            var users = new List<User>();
            var dbContextMock = new Mock<IFunctionAppDbContext>();
            var loggerMock = new Mock<ILogger>();

            dbContextMock.Setup(mock => mock.GetUpdatedUsers(It.IsAny<DateTime>())).Returns(users);

            var dataRepository = new DataRepository(dbContextMock.Object, loggerMock.Object);

            var result = dataRepository.GetUsers(DateTime.Now);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DataRepository_Test_GetUsers_Return_Result()
        {
            var users = new List<User>() { new User(), new User() };
            var dbContextMock = new Mock<IFunctionAppDbContext>();
            var loggerMock = new Mock<ILogger>();

            dbContextMock.Setup(mock => mock.GetUpdatedUsers(It.IsAny<DateTime>())).Returns(users);

            var dataRepository = new DataRepository(dbContextMock.Object, loggerMock.Object);

            var result = dataRepository.GetUsers(DateTime.Now);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void DataRepository_Test_LogUpdatedUsersDetails_Success()
        {
            var users = new List<User>() { new User(), new User() };
            var dbContextMock = new Mock<IFunctionAppDbContext>();
            var loggerMock = new Mock<ILogger>();

            var dataRepository = new DataRepository(dbContextMock.Object, loggerMock.Object);

            dataRepository.LogUpdatedUsersDetails(users);

            Assert.IsTrue(true);            
        }
    }    
}
