using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BalanceController : Controller
{
    private readonly SriTelContext _context;

    public BalanceController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. Get the data balance of a particular user
    // task of a user
    [HttpGet("data/{uid}")]
    // [Authorize]
    public async Task<ActionResult<Usage>> GetDataBalance(long uid)
    {
        var usageList = new List<Usage>();
        // get package balance if any exist
        var service = await _context.Service.FirstOrDefaultAsync(s => s.Type == ServiceType.Data);
        if (service == null) return NotFound();
        var packageUsage = await _context.PackageUsage.FirstOrDefaultAsync(pu => pu.UserId == uid
                                                        && pu.Month == DateTime.Now.Month
                                                        && pu.Year == DateTime.Now.Year
                                                        && pu.ServiceId == service.Id);
        if (packageUsage != null)
        {
            var package = await _context.Package.FindAsync(packageUsage.PackageId);
            if (package != null)
            {
                if(package.PeekData != 0) usageList.Add(new Usage{Title = $"{package.Name} (Peek)", Unit = UnitOfMeasure.Gb, TotalAmount = package.PeekData, UsageAmount = packageUsage.PeekDataUsage});
                if(package.OffPeekData != 0) usageList.Add(new Usage{Title = $"{package.Name} (OffPeek)", Unit = UnitOfMeasure.Gb, TotalAmount = package.OffPeekData, UsageAmount = packageUsage.OffPeekDataUsage});
                if(package.AnytimeData != 0) usageList.Add(new Usage{Title = $"{package.Name} (Any)", Unit = UnitOfMeasure.Gb, TotalAmount = package.AnytimeData, UsageAmount = packageUsage.AnytimeDataUsage});
            }
        }
        
        // get extra Gb balance one by one
        double totalData = 0;
        double totalUsage = 0;
        var extraGbs = await _context.AddOnActivation.Where(ad => ad.UserId == uid
                                                                  && ad.Type == AddOnType.ExtraGb
                                                                  && ad.ExpireDateTime.Month >= DateTime.Now.Month
                                                                  && ad.ExpireDateTime.Year >= DateTime.Now.Year).ToListAsync();
        foreach (var extraGb in extraGbs)
        {
            totalData += extraGb.TotalData;
            totalUsage += extraGb.DataUsage;
        }
        if (totalData > 0) usageList.Add(new Usage{Title = "Extra GB", Unit = UnitOfMeasure.Gb, TotalAmount = totalData, UsageAmount = totalUsage});
        
        // get addon balance one by one
        var addOns = await _context.AddOnActivation.Where(ad => ad.UserId == uid
                                                                && ad.Type == AddOnType.Other
                                                                && ad.ExpireDateTime.Month >= DateTime.Now.Month
                                                                && ad.ExpireDateTime.Year >= DateTime.Now.Year).ToListAsync();
        foreach (var addOn in addOns)
        {
            var ado = await _context.AddOn.FindAsync(addOn.AddOnId);
            if(ado == null) continue;
            usageList.Add(new Usage{Title = ado.Name, Unit = UnitOfMeasure.Gb, TotalAmount = ado.DataAmount, UsageAmount = addOn.DataUsage});
        }
        
        return Ok(usageList);
    }
    
    [HttpGet("voice/{uid}")]
    // [Authorize]
    public async Task<ActionResult<Usage>> GetVoiceBalance(long uid)
    {
        var usageList = new List<Usage>();
        // get package balance if any exist
        var service = await _context.Service.FirstOrDefaultAsync(s => s.Type == ServiceType.Voice);
        if (service == null) return NotFound();
        var packageUsage = await _context.PackageUsage.FirstOrDefaultAsync(pu => pu.UserId == uid
                                                        && pu.Month == DateTime.Now.Month
                                                        && pu.Year == DateTime.Now.Year
                                                        && pu.ServiceId == service.Id);
        if (packageUsage == null) return NotFound();
        var package = await _context.Package.FindAsync(packageUsage.PackageId);
        if (package == null) return NotFound();
        
        if(package.AnyNetCallMins != 0) usageList.Add(new Usage{Title = $"{package.Name} (Any Call)", Unit = UnitOfMeasure.Minutes, TotalAmount = package.AnyNetCallMins, UsageAmount = packageUsage.AnyNetCallMinsUsage});
        if(package.S2SCallMins != 0) usageList.Add(new Usage{Title = $"{package.Name} (S2S Call)", Unit = UnitOfMeasure.Minutes, TotalAmount = package.S2SCallMins, UsageAmount = packageUsage.S2SCallMinsUsage});
        if(package.AnyNetSmsCount != 0) usageList.Add(new Usage{Title = $"{package.Name} (Any SMS)", Unit = UnitOfMeasure.Sms, TotalAmount = package.AnyNetSmsCount, UsageAmount = packageUsage.AnyNetSmsCountUsage});
        if(package.S2SSmsCount != 0) usageList.Add(new Usage{Title = $"{package.Name} (S2S SMS)", Unit = UnitOfMeasure.Sms, TotalAmount = package.S2SSmsCount, UsageAmount = packageUsage.S2SSmsCountUsage});
        
        return Ok(usageList);
    }
}