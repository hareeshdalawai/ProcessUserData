using Microsoft.Extensions.Logging;
using Process.UserData.FunctionApp.Domain.Interfaces;
using Process.UserData.FunctionApp.Domain.Models;
using Process.UserData.FunctionApp.Infrastructure.Context;
using System.Text.Json;

namespace Process.UserData.FunctionApp.Infrastructure.Repository
{
    /// <summary>
    /// Implements methods for accessing db context and logging info . .
    /// </summary>
    public class DataRepository : IDataRepository
    {
        private IFunctionAppDbContext _dbContext;
        private ILogger _logger;

        public DataRepository(IFunctionAppDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public IList<User> GetUsers(DateTime lastExecutionTime)
        {
            var updatedUsers = _dbContext.GetUpdatedUsers(lastExecutionTime).ToList();

            LogUpdatedUsersDetails(updatedUsers);

            return updatedUsers;
        }

        public void LogUpdatedUsersDetails(List<User> updatedUsers)
        {
            const string logMessage = "Fetched updated users details, updated users count is = [{count}], details = [{updatesUsers}]";
            var userDetails = JsonSerializer.Serialize(updatedUsers);

            _logger.LogInformation(logMessage, updatedUsers.Count, userDetails);
        }
    }
}
