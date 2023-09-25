using Microsoft.AspNetCore.Mvc;
using SriTel.Models;

namespace SriTel.Controllers;

public class NotificationController : Controller
{
    private readonly SriTelContext _context;

    public NotificationController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. Get all notifications for a particular user
}