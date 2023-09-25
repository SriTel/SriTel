using Microsoft.AspNetCore.Mvc;
using SriTel.Models;

namespace SriTel.Controllers;

public class BalanceController : Controller
{
    private readonly SriTelContext _context;

    public BalanceController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. Get the balance of a particular service for a particular user
    // task of a user
}