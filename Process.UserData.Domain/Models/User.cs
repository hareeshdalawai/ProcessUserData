using System.ComponentModel.DataAnnotations;

namespace Process.UserData.FunctionApp.Domain.Models
{
    /// <summary>
    /// Represents updated user data in database.
    /// </summary>
    public class User
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DataValue { get; set; } = string.Empty;
        public bool NotificationFlag { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
