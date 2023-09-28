using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpPost("{uid}/{sid}/{puid}")]  //get user id,service id,packageUsage id from frontend

    public async Task<ActionResult> GetBalanceUsage(long uid, long sid, long puid)
    {
        // get the package details for current month
        DateTime currentDate = DateTime.Now;
        
        //get the user activated package current usage
        var userPackageUsage = _context.PackageUsage.Where(pu =>pu.Id == puid && pu.UserId==uid && pu.Month==currentDate.Month && pu.Year==currentDate.Year && pu.ServiceId==sid && pu.State==1).FirstOrDefault();
        
        if ( userPackageUsage != null )
        {
            //get the user package details(total data,mins amounts)
            var userPackageDetails = _context.Package.Where(p =>  p.Id == userPackageUsage.PackageId).FirstOrDefault();

            var OffPeekDataUsage =  userPackageUsage.OffPeekDataUsage;
            var PeekDataUsage =  userPackageUsage.PeekDataUsage;
            var AnytimeDataUsage =  userPackageUsage.AnytimeDateUsage;
            var S2SCallMinsUsage = userPackageUsage.S2SCallMinsUsage;
            var S2SSmsCountUsage = userPackageUsage.S2SSmsCountUsage;
            var AnyNetCallMinsUsage =  userPackageUsage.AnyNetCallMinsUsage;
            var AnyNetSmsCountUsage =  userPackageUsage.AnyNetSmsCountUsage;
            
            var TotalOffPeekData =  userPackageDetails.OffPeekData;
            var TotalPeekData =   userPackageDetails.PeekData;
            var TotalAnytimeData =   userPackageDetails.AnytimeData;
            var TotalS2SCallMins =  userPackageDetails.S2SCallMins;
            var TotalS2SSmsCount =  userPackageDetails.S2SSmsCount;
            var TotalAnyNetCallMins =   userPackageDetails.AnyNetCallMins;
            var TotalAnyNetSmsCount =   userPackageDetails.AnyNetSmsCount;

            // Create a dictionary to store the key-value pairs (package usage , package total amount)
            var packageUsageDetails = new Dictionary<string, (float,float)>
            {
                { "offpeekdata", (OffPeekDataUsage, TotalOffPeekData) },
                { "peekdata", (PeekDataUsage, TotalPeekData) },
                { "anytimedata", (AnytimeDataUsage, TotalAnytimeData) },
                { "s2scallmins", (S2SCallMinsUsage, TotalS2SCallMins) },
                { "s2ssmscount", (S2SSmsCountUsage, TotalS2SSmsCount) },
                { "anynetcallmins", (AnyNetCallMinsUsage, TotalAnyNetCallMins) },
                { "anynetsmscount", (AnyNetSmsCountUsage, TotalAnyNetSmsCount) },
                // Add more categories as needed
            };

            
            await _context.SaveChangesAsync();
            return Ok(packageUsageDetails);
        } else {
            return BadRequest("Your package is deactivated.No Package.");
        }
        

    } 

}