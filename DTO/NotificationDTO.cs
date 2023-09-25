using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class NotificationDTO
{
    [Required]public long Id { get; set; } //p
    public long UserId { get; set; }
    public required DateTime DateTime { get; set; } = DateTime.Now;
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int Priority { get; set; }


    public static NotificationDTO FromNotification(Notification notification)
    {
        return new NotificationDTO
        {
            Id = notification.Id,
            UserId = notification.UserId,
            DateTime = notification.DateTime,
            Title = notification.Title,
            Description = notification.Description,
            Priority = notification.Priority
        };
    }

    public Notification ToNotification()
    {
        return new Notification
        {
            Id = Id,
            UserId = UserId,
            DateTime = DateTime,
            Title = Title,
            Description = Description,
            Priority = Priority
        };
    }
}