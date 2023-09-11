using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Process.UserData.FunctionApp.Domain.Extensions;
using Process.UserData.FunctionApp.Domain.Mapping;
using Process.UserData.FunctionApp.ExceptionHandler.Extensions;
using Process.UserData.FunctionApp.Infrastructure.Extensions;
using Process.UserData.FunctionApp.Infrastructure.Models;
using System.Reflection;


AppConfiguration appConfiguration = new();
const string functionLoggingCategory = "Process.UserData.FunctionApp";

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<AppConfiguration>(options => context.Configuration.Bind(options));
        appConfiguration = context.Configuration.Get<AppConfiguration>();
        
        services.AddLogging();      

        services.AddSingleton(typeof(ILogger), (serviceProvider) => {
            var factory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return factory.CreateLogger(functionLoggingCategory);
        });

        services.AddAutoMapper(typeof(UserMappingProfile).GetTypeInfo().Assembly);

        services.AddRepositories(appConfiguration);

        services.AddNotifiactionServices();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .ConfigureFunctionsWorkerDefaults(configure => {

        configure.UseGlobalExceptionHandler();
    })
    .ConfigureLogging(logging => logging
        .AddFilter<ApplicationInsightsLoggerProvider>(functionLoggingCategory, (LogLevel)Enum.Parse(typeof(LogLevel), appConfiguration!.AppInsightsDefaultLogLevel))
        )
    .Build();

host.Run();
