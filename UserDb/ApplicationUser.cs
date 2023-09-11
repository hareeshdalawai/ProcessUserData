using System.ComponentModel.DataAnnotations;

namespace UserDb
{
    public class ApplicationUser
    {
        public int RecordId { get; set; }
        
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? DataValue { get; set; } = string.Empty;
        public bool? NotificationFlag { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
