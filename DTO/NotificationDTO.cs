using System.ComponentModel.DataAnnotations;
using SriTel.Models;

namespace SriTel.DTO;

public class NotificationDTO
{
    [Required]public long Id { get; set; } //p
    public long UserId { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }


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
            Title = Title!,
            Description = Description!,
            Priority = Priority
        };
    }
}