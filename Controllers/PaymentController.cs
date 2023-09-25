using Microsoft.AspNetCore.Mvc;
using SriTel.Models;

namespace SriTel.Controllers;

public class PaymentController : Controller
{
    private readonly SriTelContext _context;

    public PaymentController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. Make payment to a bill
    // user may not pay for the total bill OR over pays the bill (consider those aspects as well).
    // task of a user
    
    // 2. Get all the payments made by a user
    // task of a user
}