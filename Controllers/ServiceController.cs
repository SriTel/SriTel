using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;

public class ServiceController : Controller
{
    private readonly SriTelContext _context;
    
    public ServiceController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. Add a new Service to the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpPost]
    public async Task<ActionResult<ServiceDTO>> CreatService(ServiceDTO serviceDto)
    {
        var service = serviceDto.ToService();
        _context.Service.Add(service);
        await _context.SaveChangesAsync();
       return CreatedAtAction(nameof(GetService), new { id = service.Id }, ServiceDTO.FromService(service));
    }
    
    // 2. Remove an existing Service
    // done using postman (no front end for user) 
    // task of a admin
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteService(long id)
    {
        var service = await _context.Service.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        _context.Service.Remove(service);
        await _context.SaveChangesAsync();

        return Ok(new { Status = "Success" });

        // AFTER DELETION OF THE SERVICE, RELEVANT DELETION MUST BE DONE ON VOICE OR DATA TOO. 
        // CONSIDER THIS?? DONT KNNOW WHETHER THESE CAN BE DELETED OR NOT
    }
    
    // 3. Update an existing Service in the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ServiceDTO>> UpdateService(long id, ServiceDTO serviceDto)
    {
        if (id != serviceDto.Id)
        {
            return BadRequest();
        }
        var service = await _context.Service.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        if(serviceDto.Name != string.Empty) service.Name = serviceDto.Name;
        if(serviceDto.Charge != 0) service.Charge = serviceDto.Charge;
        if(serviceDto.State != string.Empty) service.State = serviceDto.State;
        if(serviceDto.Type != string.Empty) service.Type = serviceDto.Type; 
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ServiceExists(id))
        {
            return NotFound();
        }
        return ServiceDTO.FromService(service);

        // AFTER CHANGES OF THE SERVICE, RELEVANT CHANGES MUST BE DONE ON VOICE OR DATA TOO. CONSIDER THIS??
    }
    
    
    // 4. Get all the Services in the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
    {
        var services = await _context.Service
            .Select(u => ServiceDTO.FromService(u))
            .ToListAsync();
        return services;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ServiceDTO>> GetService(long id)
    {
        var service = await _context.Service.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        return ServiceDTO.FromService(service);
    }
    
    
    
    
    
    
    
    
    
    
    
    private bool ServiceExists(long id)
    {
        return _context.Service.Any(e => e.Id == id);
    }
    
}