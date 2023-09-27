using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;

public class BillController : Controller
{
    private readonly SriTelContext _context;

    public BillController(SriTelContext context)
    {
        _context = context;
    }
    

    [HttpPost]
    public async Task<ActionResult<BillDTO>> CreateBill(BillDTO BillDTO)
    {
        var bill = BillDTO.ToBill();
        _context.Bill.Add(bill);
        await _context.SaveChangesAsync();
        return Ok(new { Status = "Bill Created Successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBill(long id)
    {
        var bill = await _context.Bill.FindAsync(id);
        if (bill == null)
        {
            return NotFound();
        }

        _context.Bill.Remove(bill);
        await _context.SaveChangesAsync();

        return Ok(new { Status = "Success" });
    }
    
    // 1. GET all the bills of a particular user
    // task of a user
    [HttpGet("{uid}")]
    public async Task<ActionResult<IEnumerable<BillDTO>>> GetAllUserBills(long uid)
    {
        var billList = await _context.Bill.Where(bill => bill.UserId == uid).Select(bill => BillDTO.FromBill(bill)).ToListAsync();
        return billList;
    }
    
}