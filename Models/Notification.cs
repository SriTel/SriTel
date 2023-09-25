using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SriTel.Models
{
    public class Notification
    {
        [Key]public long Id { get; set; } //p
        public long UserId { get; set; }
        public required DateTime DateTime { get; set; } = DateTime.Now;
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int Priority { get; set; }


        [ForeignKey("UserId")] public List<User> Notification_User { get; set; } = null!;

    }
}