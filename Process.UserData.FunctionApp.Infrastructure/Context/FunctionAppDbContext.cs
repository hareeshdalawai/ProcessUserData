using Microsoft.EntityFrameworkCore;
using Process.UserData.FunctionApp.Domain.Models;
using Process.UserData.FunctionApp.Infrastructure.Models;

namespace Process.UserData.FunctionApp.Infrastructure.Context
{
    /// <summary>
    /// DataBase context 
    /// </summary>
    public class FunctionAppDbContext : DbContext, IFunctionAppDbContext
    {
        public FunctionAppDbContext(DbContextOptions<FunctionAppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public IList<User> GetUpdatedUsers(DateTime lastExecutedTime)
        {
            return this.Users
                .FromSqlInterpolated($"[dbo].[GetUsers] {lastExecutedTime}")
                .ToArray(); ;
        }
    }
}
