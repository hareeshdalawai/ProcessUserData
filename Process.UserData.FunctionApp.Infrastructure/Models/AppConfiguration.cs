namespace Process.UserData.FunctionApp.Infrastructure.Models
{
    /// <summary>
    /// Represents the app settings.
    /// </summary>
    public class AppConfiguration
    {
        public string ServiceBusConnectionString { get; set; } = string.Empty;
        public string DatabaseConnectionString { get; set; } = string.Empty;
        public string AppInsightsDefaultLogLevel { get; set; } = string.Empty;
    }
}
