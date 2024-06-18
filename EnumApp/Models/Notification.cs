using System.ComponentModel.DataAnnotations;

namespace EnumApp.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public NotificationType Level { get; set; }
    }

    public enum NotificationType
    {
        ERROR = 1,
        SUCCESS = 2,
        INFORMATION = 3,
        WARNING = 4
    }
}