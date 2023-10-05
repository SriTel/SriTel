using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;
[Route("api/[controller]")]
[ApiController]
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

    [HttpGet("data/{uid}")]
    [Authorize]
    public async Task<ActionResult> GetDataBill(long uid)
    {
        var response = new DetailedBillDto();
        // get the service cost for data and voice
        var dataService = await _context.DataService.Where(ds => ds.UserId == uid).FirstOrDefaultAsync();
        if (dataService != null)
        {
            var service = _context.Service.FirstOrDefault(s => s.Type == ServiceType.Data);
            if (service != null)
            {
                response.DataServiceCharge = service.Charge;
                response.ServiceId = service.Id;
            }
            if (dataService.IsDataRoaming == 1) response.DataRoamingCharge = dataService.DataRoamingCharge;
        }

        response.TotalAmount += response.DataRoamingCharge + response.DataServiceCharge;
        
        // get data package const if any package activated for previous month
        var todayDateTime = DateTime.Now;
        var packageUsage = await _context.PackageUsage.Where(pu => pu.UserId == uid 
                                                                   && pu.ServiceId == response.ServiceId
                                                                   && pu.Year == todayDateTime.AddMonths(-1).Year 
                                                                   && pu.Month == todayDateTime.AddMonths(-1).Month).FirstOrDefaultAsync();
        if (packageUsage != null)
        {
            var package = await _context.Package.Where(p => p.Id == packageUsage.PackageId).FirstOrDefaultAsync();
            if (package != null)
            {
                Console.WriteLine(package.Charge);
                response.packageCharge = package.Charge; 
                response.packageName = package.Name;
            }
            response.TotalAmount += response.packageCharge;
        }
        
        // get total addon cost if any exist for previous month
        var addOnUsages = await _context.AddOnActivation
            .Where(ad => ad.UserId == uid && ad.Type == AddOnType.Other && ad.ActivatedDateTime.Month == todayDateTime.AddMonths(-1).Month && ad.ActivatedDateTime.Year == todayDateTime.AddMonths(-1).Year).ToListAsync();
        foreach (var addOnUsage in addOnUsages)
        {
            var addOn = await _context.AddOn.FirstOrDefaultAsync(ad => ad.Id == addOnUsage.AddOnId);
            if (addOn != null)
            {
                response.DataAddOnCharge += addOnUsage.TotalData * addOn.ChargePerGb;
            }
        }
        response.TotalAmount += response.DataAddOnCharge;
        
        // get total extraGb cost if any exist
        var extraGbUsages = await _context.AddOnActivation
            .Where(ad => ad.UserId == uid && ad.Type == AddOnType.ExtraGb && ad.ActivatedDateTime.Month == todayDateTime.AddMonths(-1).Month && ad.ActivatedDateTime.Year == todayDateTime.AddMonths(-1).Year).ToListAsync();
        foreach (var extraGbUsage in extraGbUsages)
        {
            var addOn = await _context.AddOn.FirstOrDefaultAsync(ad => ad.Id == extraGbUsage.AddOnId);
            if (addOn != null)
            {
                response.ExtraGbCharge += extraGbUsage.TotalData * addOn.ChargePerGb;
            }
        }
        response.TotalAmount += response.ExtraGbCharge;

        
        // check if no bill generated for previous month
        // if not create one
        var bill = await _context.Bill
            .Where(b => b.UserId == uid 
                        && b.Month == todayDateTime.AddMonths(-1).Month 
                        && b.Year == todayDateTime.AddMonths(-1).Year 
                        && b.ServiceId == response.ServiceId)
            .FirstOrDefaultAsync();
        if (bill == null)
        {
            // then insert the bill info and create one
            // get the due amount from previous -> previous month
            double dueAmount = 0;
            var prevBill = await _context.Bill.FirstOrDefaultAsync(b =>
                b.UserId == uid 
                && b.Year == todayDateTime.AddMonths(-2).Year
                && b.Month == todayDateTime.AddMonths(-2).Month
                && b.ServiceId == response.ServiceId);
            
            // if a bill exist for previous -> previous month then get total + tax - total_payment for that bill
            if (prevBill != null)
            {
                // get the total payed amount for that bill
                double totalPaymentForPrevBill = 0;
                var allPaymentsForLastBill = await _context.Payment.Where(p => p.BillId == prevBill.Id).ToListAsync();
                if (allPaymentsForLastBill.Count > 0)
                {
                    totalPaymentForPrevBill = allPaymentsForLastBill.Sum(p => p.PayAmount);
                }

                dueAmount = prevBill.TotalAmount + prevBill.TaxAmount - totalPaymentForPrevBill;
            }
            // if no bill exist then the due amount will be 0
            
            var newBill = new Bill
            {
                UserId = uid,
                ServiceId = response.ServiceId,
                Month = todayDateTime.AddMonths(-1).Month,
                Year = todayDateTime.AddMonths(-1).Year,
                TaxAmount = response.TotalAmount*0.05,
                TotalAmount = response.TotalAmount,
                DueAmount = dueAmount
            };
            _context.Bill.Add(newBill);
            await _context.SaveChangesAsync();
            
            // then take the generated bill again
            bill = await _context.Bill
                .Where(b => b.UserId == uid 
                            && b.Month == todayDateTime.AddMonths(-1).Month 
                            && b.Year == todayDateTime.AddMonths(-1).Year
                            && b.ServiceId == response.ServiceId)
                .FirstOrDefaultAsync();

            if (bill == null) return Ok();
            response.BillId = bill.Id;
            response.TaxAmount = bill.TaxAmount;
            response.DueAmount = bill.DueAmount;
        }
        else
        {
            // check if any payments made to that bill
            var totalPayment = _context.Payment
                .Where(p => p.BillId == bill.Id)
                .Sum(p => p.PayAmount);
            // use existing bill information
            response.BillId = bill.Id;
            response.TaxAmount = bill.TaxAmount;
            response.DueAmount = bill.DueAmount;
            response.PaidAmount = totalPayment;
        }

        return Ok(response);
    }

    [HttpGet("voice/{uid}")]
    [Authorize]
    public async Task<ActionResult> GetVoiceBill(long uid)
    {
        var response = new DetailedBillDto();
        var voiceService = await _context.VoiceService.Where(vs => vs.UserId == uid).FirstOrDefaultAsync();
        if (voiceService != null)
        {
            if (voiceService.IsVoiceRoaming == 1) response.VoiceRoamingCharge = voiceService.VoiceRoamingCharge;
            if (voiceService.IsRingingTone == 1) response.RingingToneCharge = voiceService.RingingToneCharge;
            var service = _context.Service.FirstOrDefault(s => s.Type == ServiceType.Voice);
            // return Ok(service);
            if (service != null)
            {
                response.VoiceServiceCharge = service.Charge; 
                response.ServiceId = service.Id;
            }
        }
        response.TotalAmount = response.VoiceRoamingCharge + response.VoiceServiceCharge + response.RingingToneCharge;
        
        // get data package const if any package activated for previous month
        var todayDateTime = DateTime.Now;
        var packageUsage = await _context.PackageUsage.Where(pu => pu.UserId == uid 
                                                                   && pu.ServiceId == response.ServiceId
                                                                   && pu.Year == todayDateTime.AddMonths(-1).Year 
                                                                   && pu.Month == todayDateTime.AddMonths(-1).Month).FirstOrDefaultAsync();
        if (packageUsage != null)
        {
            var package = await _context.Package.Where(p => p.Id == packageUsage.PackageId).FirstOrDefaultAsync();
            if (package != null)
            {
                Console.WriteLine(package.Charge);
                response.packageCharge = package.Charge; 
                response.packageName = package.Name;
            }
            response.TotalAmount += response.packageCharge;
        }
        
        // check if no bill generated for previous month
        // if not create one
        var bill = await _context.Bill
            .Where(b => b.UserId == uid 
                        && b.Month == todayDateTime.AddMonths(-1).Month 
                        && b.Year == todayDateTime.AddMonths(-1).Year 
                        && b.ServiceId == response.ServiceId)
            .FirstOrDefaultAsync();
        if (bill == null)
        {
            // then insert the bill info and create one
            // get the due amount from previous -> previous month
            double dueAmount = 0;
            var prevBill = await _context.Bill.FirstOrDefaultAsync(b =>
                b.UserId == uid 
                && b.Year == todayDateTime.AddMonths(-2).Year 
                && b.Month == todayDateTime.AddMonths(-2).Month
                && b.ServiceId == response.ServiceId);
            
            // if a bill exist for previous -> previous month then get total + tax - total_payment for that bill
            if (prevBill != null)
            {
                // get the total payed amount for that bill
                double totalPaymentForPrevBill = 0;
                var allPaymentsForLastBill = await _context.Payment.Where(p => p.BillId == prevBill.Id).ToListAsync();
                if (allPaymentsForLastBill.Count > 0)
                {
                    totalPaymentForPrevBill = allPaymentsForLastBill.Sum(p => p.PayAmount);
                }

                dueAmount = prevBill.TotalAmount + prevBill.TaxAmount - totalPaymentForPrevBill;
            }
            // if no bill exist then the due amount will be 0
            
            var newBill = new Bill
            {
                UserId = uid,
                ServiceId = response.ServiceId,
                Month = todayDateTime.AddMonths(-1).Month,
                Year = todayDateTime.AddMonths(-1).Year,
                TaxAmount = response.TotalAmount*0.05,
                TotalAmount = response.TotalAmount,
                DueAmount = dueAmount
            };
            _context.Bill.Add(newBill);
            await _context.SaveChangesAsync();
            
            // then take the generated bill again
            bill = await _context.Bill
                .Where(b => b.UserId == uid 
                            && b.Month == todayDateTime.AddMonths(-1).Month 
                            && b.Year == todayDateTime.AddMonths(-1).Year
                            && b.ServiceId == response.ServiceId)
                .FirstOrDefaultAsync();

            if (bill == null) return Ok();
            response.BillId = bill.Id;
            response.TaxAmount = bill.TaxAmount;
            response.DueAmount = bill.DueAmount;
        }
        else
        {
            // check if any payments made to that bill
            var totalPayment = _context.Payment
                .Where(p => p.BillId == bill.Id)
                .Sum(p => p.PayAmount);
            // use existing bill information
            response.BillId = bill.Id;
            response.TaxAmount = bill.TaxAmount;
            response.DueAmount = bill.DueAmount;
            response.PaidAmount = totalPayment;
        }
        return Ok(response);
    }

}