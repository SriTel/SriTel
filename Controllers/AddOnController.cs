using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;
[Route("api/[controller]")]
[ApiController]

public class AddOnController : Controller
{
    private readonly SriTelContext _context;

    public AddOnController(SriTelContext context)
    {
        _context = context;
    }
    
    // 1. Add a new Add-on to the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpPost]
    public async Task<ActionResult<AddOnDTO>> CreateAddOn(AddOnDTO addOnDTO)
    {
        var addOn = addOnDTO.ToAddOn();
        _context.AddOn.Add(addOn);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAddOn), new { id = addOn.Id }, AddOnDTO.FromAddOn(addOn));
    }
    
    // 2. Remove an existing Add-on
    // done using postman (no front end for user) 
    // task of a admin
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAddOn(long id)
    {
        var addOn = await _context.AddOn.FindAsync(id);
        if (addOn == null)
        {
            return NotFound();
        }

        _context.AddOn.Remove(addOn);
        await _context.SaveChangesAsync();

        return Ok(new { Status = "Success" });
    }
    
    // 3. Update an existing Add-on in the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<AddOnDTO>> UpdateAddOn(long id, AddOnDTO addOnDto)
    {
        if (id != addOnDto.Id)
        {
            return BadRequest();
        }
        var addOn = await _context.AddOn.FindAsync(id);
        if (addOn == null)
        {
            return NotFound();
        }
        if(addOnDto.Name != string.Empty) addOn.Name = addOnDto.Name;
        if(addOnDto.Image != string.Empty) addOn.Image = addOnDto.Image;
        if(addOnDto.Description != string.Empty) addOn.Description = addOnDto.Description;
        if(addOnDto.ValidNoOfDays != 0) addOn.ValidNoOfDays = addOnDto.ValidNoOfDays;   //check
        if(addOnDto.ChargePerGb != 0) addOn.ChargePerGb = addOnDto.ChargePerGb;         //check
        if(addOnDto.DataAmount != 0) addOn.DataAmount = addOnDto.DataAmount;         //check
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!AddOnExists(id))
        {
            return NotFound();
        }
        return AddOnDTO.FromAddOn(addOn);
    }
    
    // 4. Get all the Add-ons in the database
    // done using postman (no front end for user) 
    // task of a admin
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddOnDTO>>> GetAddOns()
    {
        var addOns = await _context.AddOn
            .Select(u => AddOnDTO.FromAddOn(u))
            .ToListAsync();
        return addOns;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<AddOnDTO>> GetAddOn(long id)
    {
        var addOn = await _context.AddOn.FindAsync(id);
        if (addOn == null)
        {
            return NotFound();
        }
        return AddOnDTO.FromAddOn(addOn);
    }
    
    // 6. Add a new addon to a user
    // hint: you should get the user id and addon id from front-end
    // task of a user
    [HttpGet("{uid}/{aoid}")]  // userID, addOnId
    [Authorize]
    public async Task<ActionResult> ActivateAddOn(long uid, long aoid)
    {
        // get the package type first
        var addOn = await _context.AddOn.FindAsync(aoid);
        if (addOn == null) return NotFound();
        var serviceId = (await _context.Service.Where(s => s.Type == ServiceType.Data).FirstAsync()).Id;
        
        // check if user already have that addOn.
        var currentAddOn =
            await _context.AddOnActivation.Where(ad => ad.UserId == uid && ad.AddOnId == aoid).FirstOrDefaultAsync();
        
        // if not create a new one
        if (currentAddOn == null)
        {
            var newAddOnActivation = new AddOnActivation
            {
                DataServiceId = serviceId,
                UserId = uid,
                AddOnId = aoid,
                Type = addOn.Type,
                ActivatedDateTime = System.DateTime.Now,
                ExpireDateTime = System.DateTime.Now.AddDays(addOn.ValidNoOfDays),
                DataUsage = 0,
                TotalData = addOn.DataAmount
            };
            _context.AddOnActivation.Add(newAddOnActivation);
        }
        // if yes update it
        else
        {
            // first check if the current one is expired
            // if not update it
            if (currentAddOn.ExpireDateTime > System.DateTime.Now)
            {
                currentAddOn.ActivatedDateTime = System.DateTime.Now;
                currentAddOn.ExpireDateTime =
                    currentAddOn.ExpireDateTime > System.DateTime.Now.AddDays(addOn.ValidNoOfDays)
                        ? currentAddOn.ExpireDateTime
                        : System.DateTime.Now.AddDays(addOn.ValidNoOfDays);
                currentAddOn.TotalData += addOn.DataAmount;
            }
            // if yes restore that one
            else
            {
                currentAddOn.ActivatedDateTime = System.DateTime.Now;
                currentAddOn.ExpireDateTime = System.DateTime.Now.AddDays(addOn.ValidNoOfDays);
                currentAddOn.DataUsage = 0;
                currentAddOn.TotalData = addOn.DataAmount;
            }
            
        }
        
        // if the addOn is a extraGB addon
        // extend the activationTime for similar extra gb addons of the given user
        if (addOn.Type == AddOnType.ExtraGb)
        {
            var similarAddOns = await _context.AddOnActivation
                .Where(ad =>
                    ad.UserId == uid && ad.Type == AddOnType.ExtraGb &&
                    ad.ExpireDateTime > System.DateTime.Now &&
                    ad.DataUsage < ad.TotalData)
                .ToListAsync();

            foreach (var similarAddOn in similarAddOns)
            {
                similarAddOn.ActivatedDateTime = System.DateTime.Now;
                similarAddOn.ExpireDateTime = System.DateTime.Now.AddDays(addOn.ValidNoOfDays);
            }

        }
        await _context.SaveChangesAsync();
        return Ok();
    }

    private object DateTime()
    {
        throw new NotImplementedException();
    }

    private bool AddOnExists(long id)
    {
        return _context.AddOn.Any(e => e.Id == id);
    }
}