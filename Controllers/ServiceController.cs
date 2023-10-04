using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;
[Route("api/[controller]")]
[ApiController]
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
        // if (service.Type == ServiceType.Data)
        // {
        //     var dataService = new DataService
        //     {
        //         UserId = service.Id,
        //         IsDataRoaming = 0,
        //         DataRoamingCharge = 100
        //     };
        //     _context.DataService.Add(dataService);
        // }
        // else
        // {
        //     var voiceService = new VoiceService
        //     {
        //         UserId = service.Id,
        //         IsRingingTone = 0,
        //         RingingToneName = "Alone - Alan Walker",
        //         RingingToneCharge = 100,
        //         IsVoiceRoaming = 0,
        //         VoiceRoamingCharge = 100
        //     };
        //     _context.VoiceService.Add(voiceService);
        // }
        await _context.SaveChangesAsync();
       return CreatedAtAction(nameof(GetService), new { id = service.Id }, ServiceDTO.FromService(service));
    }
    
    // 2. Remove an existing Service
    // done using postman (no front end for user) 
    // task of a admin
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(long id)
    {
        var service = await _context.Service.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        // if (service.Type == ServiceType.Data)
        // {
        //     var dataService = await _context.DataService.FindAsync(id);
        //     if(dataService != null) _context.DataService.Remove(dataService);
        // }
        // else
        // {
        //     var voiceService = await _context.VoiceService.FindAsync(id);
        //     if(voiceService != null) _context.VoiceService.Remove(voiceService);
        // }
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
        service.Type = serviceDto.Type; 
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
    
    // 5. Get a particular Service in the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDTO>> GetService(long id)
    {
        var service = await _context.Service.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        return ServiceDTO.FromService(service);
    }
    
    // 6. Get Data Service of a particular user in the database ///CHECK
    // task of a user
    [HttpGet("data/{userId}")]
    public async Task<ActionResult<DataServiceDTO>> GetDataService(long userId)
    {
        var service = await _context.DataService.FindAsync(userId);
        if (service == null)
        {
            return NotFound();
        }
        return DataServiceDTO.FromDataService(service);
    }
    
    // 7. Get Voice Service of a particular user in the database  /// CHECK 
    // task of a user
    [HttpGet("voice/{userId}")]
    public async Task<ActionResult<VoiceServiceDTO>> GetVoiceService(long userId)
    {
        var service = await _context.VoiceService.FindAsync(userId);
        if (service == null)
        {
            return NotFound();
        }
        return VoiceServiceDTO.FromVoiceService(service);
    }
    
    // 8. Enable or Disable Existing Ringing Tone for a user
    // task of a user
    [HttpGet("ringingtone/toggle/{userId}")]
    [Authorize]
    public async Task<ActionResult> ToggleRingingTone(long userId)
    {
        var service = await _context.VoiceService.FindAsync(userId);
        if (service == null)
        {
            var voiceService = new VoiceService
            {
                UserId = userId,
                IsRingingTone = 1,
                RingingToneName = "Alone - Alan Walker",
                RingingToneCharge = 100,
                IsVoiceRoaming = 0,
                VoiceRoamingCharge = 100
            };
            _context.VoiceService.Add(voiceService);
        }
        else
        {
            service.IsRingingTone = 1 - service.IsRingingTone;
        }
        await _context.SaveChangesAsync();
        return Ok(new { Status = "Success" });
    }
    
    [HttpPost("ringingtone/change/{userId}")]
    [Authorize]
    public async Task<ActionResult> ToggleRingingTone(long userId, [FromBody] RingingToneUpdateDto ringingToneUpdateDto)
    {
        
        var service = await _context.VoiceService.FindAsync(userId);
        if (service != null)
        {
            service.RingingToneName = ringingToneUpdateDto.ringingToneName;
        }
        await _context.SaveChangesAsync();
        return Ok(new { Status = "Success" });
    }
    
    // 9. Enable or Disable Voice Roaming for a user
    // task of a user
    [HttpGet("roaming/voice/toggle/{userId}")]
    [Authorize]
    public async Task<ActionResult> ToggleVoiceRoaming(long userId)
    {
        var service = await _context.VoiceService.FindAsync(userId);
        if (service == null)
        {
            var voiceService = new VoiceService
            {
                UserId = userId,
                IsRingingTone = 0,
                RingingToneName = "Alone - Alan Walker",
                RingingToneCharge = 100,
                IsVoiceRoaming = 1,
                VoiceRoamingCharge = 100
            };
            _context.VoiceService.Add(voiceService);
        }
        else
        {
            service.IsVoiceRoaming = 1 - service.IsVoiceRoaming;
        }
        await _context.SaveChangesAsync();
        return Ok(new { Status = "Success" });
    }
    
    // 10. Enable or Disable Data Roaming for a user
    // task of a user
    [HttpGet("roaming/data/toggle/{userId}")]
    [Authorize]
    public async Task<ActionResult> ToggleDataRoaming(long userId)
    {
        var service = await _context.DataService.FindAsync(userId);
        if (service == null)
        {
            var dataService = new DataService
            {
                UserId = userId,
                IsDataRoaming = 1,
                DataRoamingCharge = 150
            };
            _context.DataService.Add(dataService);
        }
        else
        {
            service.IsDataRoaming = 1 - service.IsDataRoaming;
        }
        await _context.SaveChangesAsync();
        return Ok(new { Status = "Success" });
    }
    
    // 11. Get other services status (data roaming, voice roaming and ringingtone)
    // task of a user
    [HttpGet("other/status/{userId}")]
    [Authorize]
    public async Task<ActionResult> OtherServicesStatus(long userId)
    {
        var voiceService = _context.VoiceService.Where(vs => vs.UserId == userId).FirstOrDefault();
        var dataService = _context.DataService.Where(ds => ds.UserId == userId).FirstOrDefault();

        var otherServicesStatusDto = new OtherServicesStatusDTO
        {
            DataRoamingStatus = 0,
            VoiceRoamingStatus = 0,
            RingingToneStatus = 0,
            RingingTone = "Alone - Alan Walker"
        };
        if(voiceService != null)
        {
            otherServicesStatusDto.VoiceRoamingStatus = voiceService.IsVoiceRoaming;
            otherServicesStatusDto.RingingToneStatus = voiceService.IsRingingTone;
            otherServicesStatusDto.RingingTone = voiceService.RingingToneName;
        }

        if (dataService != null)
        {
            otherServicesStatusDto.DataRoamingStatus = dataService.IsDataRoaming;
        }

        return Ok(otherServicesStatusDto);
    }
    
    
    // supporting services
    private bool ServiceExists(long id)
    {
        return _context.Service.Any(e => e.Id == id);
    }
    
}