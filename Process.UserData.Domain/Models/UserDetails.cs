namespace Process.UserData.FunctionApp.Domain.Models
{
    /// <summary>
    /// Represents user data.
    /// </summary>
    public class NotificationMessage
    {
        public string CorrelationId { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DataValue { get; set; } = string.Empty;
    }
}
