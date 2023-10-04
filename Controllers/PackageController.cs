using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PackageController : Controller
{
    private readonly SriTelContext _context;

    public PackageController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. Add a new Package to the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpPost]
    public async Task<ActionResult<PackageDTO>> CreateAddOn(PackageDTO packageDto)
    {
        var package = packageDto.ToPackage();
        _context.Package.Add(package);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPackage), new { id = package.Id }, PackageDTO.FromPackage(package));
    }
    
    // 2. Remove an existing Package
    // done using postman (no front end for user) 
    // task of a admin
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletePackage(long id)
    {
        var package = await _context.Package.FindAsync(id);
        if (package == null)
        {
            return NotFound();
        }

        _context.Package.Remove(package);
        await _context.SaveChangesAsync();

        return Ok(new { Status = "Success" });
    }
    
    // 3. Update an existing Package in the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<PackageDTO>> UpdatePackage(long id, PackageDTO packageDto)
    {
        if (id != packageDto.Id)
        {
            return BadRequest();
        }
        var package = await _context.Package.FindAsync(id);
        if (package == null)
        {
            return NotFound();
        }
        if(packageDto.Name != string.Empty) package.Name = packageDto.Name;
        if(packageDto.Renewal != null) package.Renewal = packageDto.Renewal;
        if(packageDto.Description != string.Empty) package.Description = packageDto.Description;
        if(packageDto.Image != string.Empty) package.Image = packageDto.Image;
        if(packageDto.Charge != 0) package.Charge = packageDto.Charge;
        if(packageDto.OffPeekData != 0) package.OffPeekData = packageDto.OffPeekData;
        if(packageDto.PeekData != 0) package.PeekData = packageDto.PeekData;
        if(packageDto.AnytimeData != 0) package.AnytimeData = packageDto.AnytimeData;
        if(packageDto.S2SCallMins != 0) package.S2SCallMins = packageDto.S2SCallMins;
        if(packageDto.S2SSmsCount != 0) package.S2SSmsCount = packageDto.S2SSmsCount;
        if(packageDto.AnyNetCallMins != 0) package.AnyNetCallMins = packageDto.AnyNetCallMins;
        if(packageDto.AnyNetSmsCount != 0) package.AnyNetSmsCount = packageDto.AnyNetSmsCount;
       try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PackageExists(id))
        {
            return NotFound();
        }
        return PackageDTO.FromPackage(package);
    }
    
    // 4. Get all the Packages in the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PackageDTO>>> GetPackages()
    {
        var packages = await _context.Package
            .Select(u => PackageDTO.FromPackage(u))
            .ToListAsync();

        return Ok(packages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PackageDTO>> GetPackage(long id)
    {
        var package = await _context.Package.FindAsync(id);
        if (package == null)
        {
            return NotFound();
        }
        return PackageDTO.FromPackage(package);
    }

    
    // 5. Activate a package for a user
    // hint: get the package id and user id from front-end.
    // task of a user'
    [HttpGet("toggle/{userId}/{packageId}")]
    [Authorize]
    public async Task<ActionResult> TogglePackageUsage(long userId, long packageId)
    {
        // get the package type first
        var package = await _context.Package.FindAsync(packageId);
        var serviceId = package != null ? (await _context.Service.Where(s => s.Type == package.Type).FirstAsync()).Id
            : (await _context.Service.FirstAsync()).Id;
        
        // look if there is an activated package for the user for current month
        var activePackageUsage = await _context.PackageUsage
            .Where(pu => pu.UserId == userId && pu.Year == DateTime.Now.Year && pu.Month == DateTime.Now.Month && pu.ServiceId == serviceId)
            .FirstOrDefaultAsync();
        
        // if package exist
        if (activePackageUsage != null)
        {
            // check if the activatedPackage and given package is same
            // if same then toggle it
            if (package != null && package.Id == activePackageUsage.PackageId)
            {
                activePackageUsage.State = 1 - activePackageUsage.State;
            }
            // if not same
            // then updgrade or downgrade
            else if (package != null)
            {
                var currentPackage = await _context.Package.FindAsync(activePackageUsage.PackageId);
                var oldPackageId = activePackageUsage.PackageId;
                // upgrade
                if (currentPackage != null && package.Charge > currentPackage.Charge)
                {
                    activePackageUsage.PackageId = package.Id;
                    await _context.SaveChangesAsync();
                }
                // downgrade
                else if (currentPackage != null)
                {
                    activePackageUsage.PackageId = package.Id;
                    activePackageUsage.AnytimeDataUsage = activePackageUsage.AnytimeDataUsage == 0 ? 0 : Math.Min(activePackageUsage.AnytimeDataUsage, package.AnytimeData);
                    activePackageUsage.OffPeekDataUsage = activePackageUsage.OffPeekDataUsage == 0 ? 0 : Math.Min(activePackageUsage.OffPeekDataUsage, package.OffPeekData);
                    activePackageUsage.PeekDataUsage = activePackageUsage.PeekDataUsage == 0 ? 0 : Math.Min(activePackageUsage.PeekDataUsage, package.PeekData);
                    activePackageUsage.S2SCallMinsUsage = activePackageUsage.S2SCallMinsUsage == 0 ? 0 : Math.Min(activePackageUsage.S2SCallMinsUsage, package.S2SCallMins);
                    activePackageUsage.AnyNetCallMinsUsage = activePackageUsage.AnyNetCallMinsUsage == 0 ? 0 : Math.Min(activePackageUsage.AnyNetCallMinsUsage, package.AnyNetCallMins);
                    activePackageUsage.S2SSmsCountUsage = activePackageUsage.S2SSmsCountUsage == 0 ? 0 : Math.Min(activePackageUsage.S2SSmsCountUsage, package.S2SSmsCount);
                    activePackageUsage.AnyNetSmsCountUsage = activePackageUsage.AnyNetSmsCountUsage == 0 ? 0 : Math.Min(activePackageUsage.AnyNetSmsCountUsage, package.AnyNetSmsCount);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }

                activePackageUsage.UpdateDateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                // return the old package
                return Ok(new { OldPackageId = oldPackageId });
            }
            else
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
        }
        else
        {
            var packageUsage = new PackageUsage
            {
                UserId = userId,
                ServiceId = serviceId,
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month,
                PackageId = packageId,
                UpdateDateTime = DateTime.Now,
                OffPeekDataUsage = 0,
                PeekDataUsage = 0,
                AnytimeDataUsage = 0,
                S2SCallMinsUsage = 0,
                S2SSmsCountUsage = 0,
                AnyNetCallMinsUsage = 0,
                AnyNetSmsCountUsage = 0,
                State = 1
            };
            _context.PackageUsage.Add(packageUsage);
            await _context.SaveChangesAsync();
        }
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    // 6. Get Activated package ids of a user for current month
    // task of a user
    [HttpGet("activatedpackageids/{userId}")]
    [Authorize]
    public async Task<ActionResult<List<long>>> GetActivePackageIds(long userId)
    {
        Console.WriteLine("hello there");
        return await _context.PackageUsage
            .Where(pu => pu.UserId == userId && pu.UpdateDateTime.Year == DateTime.Now.Year && pu.UpdateDateTime.Month == DateTime.Now.Month)
            .Select(pu => pu.PackageId)
            .ToListAsync();
    }

    [HttpGet("use/data/{uid}")]
    [Authorize]
    public async Task<IActionResult> UseDataPackage(long uid)
    {
        float dataAmount = 5;
        var service = await _context.Service.FirstOrDefaultAsync(s => s.Type == ServiceType.Data);
        if (service == null) return NotFound();
        var packageUsage = await _context.PackageUsage.Where(pu => pu.UserId == uid
                                          && pu.Year == DateTime.Now.Year
                                          && pu.Month == DateTime.Now.Month
                                          && pu.ServiceId == service.Id).FirstOrDefaultAsync();
        if (packageUsage == null) return NotFound();
        var package = await _context.Package.FindAsync(packageUsage.PackageId);
        if (package == null) return NotFound();

        if ((DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 24) ||
            (DateTime.Now.Hour >= 8 && DateTime.Now.Hour == 24 && DateTime.Now.Minute == 0))
        {
            if (packageUsage.PeekDataUsage + dataAmount <= package.PeekData) packageUsage.PeekDataUsage += dataAmount;
            else if (packageUsage.PeekDataUsage < package.PeekData)
            {
                dataAmount -= package.PeekData - packageUsage.PeekDataUsage;
                packageUsage.PeekDataUsage = package.PeekData;
                if (package.AnytimeData > 0 && packageUsage.AnytimeDataUsage < package.AnytimeData)
                {
                    dataAmount -= package.AnytimeData - packageUsage.AnytimeDataUsage;
                    packageUsage.AnytimeDataUsage = package.AnytimeData;
                }
            }

        }
        else
        {
            if (packageUsage.OffPeekDataUsage + dataAmount <= package.OffPeekData) packageUsage.OffPeekDataUsage += dataAmount;
            else if (packageUsage.OffPeekDataUsage < package.OffPeekData)
            {
                dataAmount -= package.OffPeekData - packageUsage.OffPeekDataUsage;
                packageUsage.OffPeekDataUsage = package.OffPeekData;
                if (package.AnytimeData > 0 && packageUsage.AnytimeDataUsage < package.AnytimeData)
                {
                    dataAmount -= package.AnytimeData - packageUsage.AnytimeDataUsage;
                    packageUsage.AnytimeDataUsage = package.AnytimeData;
                }
            }
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpGet("use/voice/call/{uid}")]
    [Authorize]
    public async Task<IActionResult> UseCallPackage(long uid)
    {
        int callAmount = 20;
        var service = await _context.Service.FirstOrDefaultAsync(s => s.Type == ServiceType.Voice);
        if (service == null) return NotFound();
        var packageUsage = await _context.PackageUsage.Where(pu => pu.UserId == uid
                                          && pu.Year == DateTime.Now.Year
                                          && pu.Month == DateTime.Now.Month
                                          && pu.ServiceId == service.Id).FirstOrDefaultAsync();
        if (packageUsage == null) return NotFound();
        var package = await _context.Package.FindAsync(packageUsage.PackageId);
        if (package == null) return NotFound();

        if (packageUsage.S2SCallMinsUsage + callAmount <= package.S2SCallMins) packageUsage.S2SCallMinsUsage += callAmount;
        else if (packageUsage.S2SCallMinsUsage < package.S2SCallMins)
        {
            callAmount -= package.S2SCallMins - packageUsage.S2SCallMinsUsage;
            packageUsage.S2SCallMinsUsage = package.S2SCallMins;
            if (package.AnyNetCallMins > 0 && packageUsage.AnyNetCallMinsUsage < package.AnyNetCallMins)
            {
                callAmount -= package.AnyNetCallMins - packageUsage.AnyNetCallMinsUsage;
                packageUsage.AnyNetCallMinsUsage = package.AnyNetCallMins;
            }
        }
        
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpGet("use/voice/sms/{uid}")]
    [Authorize]
    public async Task<IActionResult> UseSMSPackage(long uid)
    {
        int callAmount = 20;
        var service = await _context.Service.FirstOrDefaultAsync(s => s.Type == ServiceType.Voice);
        if (service == null) return NotFound();
        var packageUsage = await _context.PackageUsage.Where(pu => pu.UserId == uid
                                                                   && pu.Year == DateTime.Now.Year
                                                                   && pu.Month == DateTime.Now.Month
                                                                   && pu.ServiceId == service.Id).FirstOrDefaultAsync();
        if (packageUsage == null) return NotFound();
        var package = await _context.Package.FindAsync(packageUsage.PackageId);
        if (package == null) return NotFound();

        if (packageUsage.S2SSmsCountUsage + callAmount <= package.S2SSmsCount) packageUsage.S2SSmsCountUsage += callAmount;
        else if (packageUsage.S2SSmsCountUsage < package.S2SSmsCount)
        {
            callAmount -= package.S2SSmsCount - packageUsage.S2SSmsCountUsage;
            packageUsage.S2SSmsCountUsage = package.S2SSmsCount;
            if (package.AnyNetSmsCount > 0 && packageUsage.AnyNetSmsCountUsage < package.AnyNetSmsCount)
            {
                callAmount -= package.AnyNetSmsCount - packageUsage.AnyNetSmsCountUsage;
                packageUsage.AnyNetSmsCountUsage = package.AnyNetSmsCount;
            }
        }
        
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    private bool PackageExists(long id)
    {
        return _context.AddOn.Any(e => e.Id == id);
    }
}