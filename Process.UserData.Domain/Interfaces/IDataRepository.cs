using Process.UserData.FunctionApp.Domain.Models;

namespace Process.UserData.FunctionApp.Domain.Interfaces
{
    /// <summary>
    /// Provides methods for accessing user data in sql
    /// </summary>
    public interface IDataRepository
    {
        IList<User> GetUsers(DateTime lastExecutionTime);
    }
}
