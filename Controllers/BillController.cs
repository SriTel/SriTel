using Microsoft.AspNetCore.Mvc;
using SriTel.Models;

namespace SriTel.Controllers;

public class BillController : Controller
{
    private readonly SriTelContext _context;

    public BillController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. GET all the bills of a particular user
    // task of a user
}