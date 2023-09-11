using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Process.UserData.FunctionApp.Domain.Interfaces;
using Process.UserData.FunctionApp.Infrastructure.Context;
using Process.UserData.FunctionApp.Infrastructure.Models;
using Process.UserData.FunctionApp.Infrastructure.Repository;

namespace Process.UserData.FunctionApp.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods to register respositories with service provider.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services, AppConfiguration configuration)
        {
            services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddServiceBusClient(configuration.ServiceBusConnectionString);
            });
            services.AddTransient<IFunctionAppDbContext, FunctionAppDbContext>();

            services.AddDbContext<FunctionAppDbContext>(options => options.UseSqlServer(configuration.DatabaseConnectionString));

            services.AddTransient<IMessagingRepository, MessagingRepository>();
            services.AddTransient<IDataRepository, DataRepository>();
        }
    }
}
