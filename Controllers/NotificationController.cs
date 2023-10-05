using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotificationController : Controller
{
    private readonly SriTelContext _context;

    public NotificationController(SriTelContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<NotificationDTO>> CreateNotification(NotificationDTO notificationDTO)
    {
        var notification = notificationDTO.ToNotification();
        _context.Notification.Add(notification);
        await _context.SaveChangesAsync();
        return Ok(new { Status = "Notification Created Successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteNotification(long id)
    {
        var notification = await _context.Notification.FindAsync(id);
        if (notification == null)
        {
            return NotFound();
        }

        _context.Notification.Remove(notification);
        await _context.SaveChangesAsync();

        return Ok(new { Status = "Success" });
    }
    
    // 1. Get all notifications for a particular user
    [HttpGet("{uid}")]
    public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetAllUserNotifications(long uid)
    {
        var notificationList = await _context.Notification.Where(notification => notification.UserId == uid).Select(notification => NotificationDTO.FromNotification(notification)).ToListAsync();
        return notificationList;
    }
}