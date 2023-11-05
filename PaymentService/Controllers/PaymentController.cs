using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.DTO;
using PaymentService.Models;

namespace PaymentService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : Controller
{
    private readonly PaymentServiceContext _context;

    public PaymentController(PaymentServiceContext context)
    {
        _context = context;
    }
    
    // 1. Make payment to a bill
    // user may not pay for the total bill OR over pays the bill (consider those aspects as well).
    // task of a user
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PaymentDTO>> CreatePayment(PaymentDTO paymentDto)
    {
        // take the bill Id and calculate the due amount
        // return Ok("Gotcha");
        var bill = await _context.Bill.FindAsync(paymentDto.BillId);
        if (bill != null)
        {
            var outstanding = bill.DueAmount + bill.TaxAmount + bill.TotalAmount;
            // reduce payments  if any exists
            var totalPayment = _context.Payment
                .Where(p => p.BillId == bill.Id)
                .Sum(p => p.PayAmount);
            outstanding -= totalPayment;
            paymentDto.Outstanding = outstanding;
        }
        var payment = paymentDto.ToPayment();
        _context.Payment.Add(payment);
        await _context.SaveChangesAsync();
        // return Ok("Got");
        return Ok(new {Status = "Success"});
    }
    
    
    // 2. Get a particular payments made by a user
    // task of a user
    [HttpGet("{uid}/{id}")]
    [Authorize]
    public async Task<ActionResult<PaymentDTO>> GetPayment(long uid, long id)
    {
        var payment = await _context.Payment.FindAsync(id);
        if (payment == null)
        {
            return NotFound();
        }
        return PaymentDTO.FromPayment(payment);
    }
    
    
    // 3. Get all the payments made by a user
    // task of a user
    [HttpGet("{uid}")]
    [Authorize]
    public async Task<ActionResult<List<PaymentDTO>>> GetPayments(long uid)
    {
        var payments = await _context.Payment.Where(p => p.UserId == uid).ToListAsync();
        if (payments.Count == 0)
        {
            return NotFound();
        }
        return payments.Select(payment => PaymentDTO.FromPayment(payment)).ToList();
    }
    
    // 3. Make a payment
    // task of a user
    [HttpPost("/pay/")]
    [Authorize]
    public async Task<ActionResult> MakePayment([FromBody] PaymentDTO paymentDto)
    {
        var payment = new Payment
        {
            BillId = paymentDto.BillId,
            PayDateTime = DateTime.Now,
            UserId = paymentDto.UserId,
            ServiceId = paymentDto.ServiceId,
            PayMethod = PaymentMethodType.Card,
            PayAmount = paymentDto.PayAmount,
            Outstanding = paymentDto.Outstanding
        };
        _context.Payment.Add(payment);
        await _context.SaveChangesAsync();
        return Ok(new {Status = "Success"});
    }
}