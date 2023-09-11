using Process.UserData.FunctionApp.Domain.Models;
using Process.UserData.FunctionApp.Infrastructure.Models;

namespace Process.UserData.FunctionApp.Infrastructure.Context
{
    public interface IFunctionAppDbContext
    {
        IList<User> GetUpdatedUsers(DateTime lastExecutedTime);
    }
}
