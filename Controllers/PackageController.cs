using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;

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
        if(packageDto.Renewal != string.Empty) package.Renewal = packageDto.Renewal;
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
        return packages;
    }

    [HttpGet("{id}")]
    [Authorize]
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
    // task of a user
   
    // 6. Upgrade or Downgrade a package for a user
    // task of a user
    
    // 7. Deactivate a package for a user
    // task of a user
    
    
    private bool PackageExists(long id)
    {
        return _context.AddOn.Any(e => e.Id == id);
    }
}