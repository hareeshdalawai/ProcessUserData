using Microsoft.EntityFrameworkCore;
using System;

namespace UserDb
{
    public class FunctionAppDbContext : DbContext
    {
        public FunctionAppDbContext(DbContextOptions<FunctionAppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser()
                {
                    RecordId = 1,
                    UserId = 1,
                    UserName = "User 1",
                    Email = "user1@abc.com",
                    DataValue = "Some value",
                    NotificationFlag = false,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now

                },
                new ApplicationUser()
                {
                    RecordId = 2,
                    UserId = 2,
                    UserName = "User 2",
                    Email = "user2@abc.com",
                    DataValue = "Some value",
                    NotificationFlag = false,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                });
            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<ApplicationUser> SP_GetUpdatedUsers(DateTime lastExecutedTime)
        {
            return this.Users
                .FromSqlInterpolated($"[dbo].[GetUsers] {lastExecutedTime}")
                .ToArray();
        }
    }
}
