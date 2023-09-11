using Microsoft.Extensions.DependencyInjection;
using Process.UserData.FunctionApp.Domain.Notification;

namespace Process.UserData.FunctionApp.Domain.Extensions
{
    /// <summary>
    /// Provides extension methods for registering domain services with service provider.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void AddNotifiactionServices(this IServiceCollection services)
        {
            services.AddTransient<INotificationService, NotificationService>();
        }
    }
}
