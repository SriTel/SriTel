using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SriTel.DTO;
using SriTel.Models;

namespace SriTel.Controllers;

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
    [HttpPost("{uid}/{aoid}")]  // userID, addOnId
    public async Task<ActionResult<AddOnActivationDTO>> ActivateAddOn(long uid, long aoid)
    {
        AddOnActivationDTO addOnActivation = new AddOnActivationDTO();
        addOnActivation.DataServiceId = 12;     // what is this
        // there are only two services in the service table
        // one is internet service (comes under dataservice)
        // other one is voice service (comes under voice service)
        // so there is only a single entry in dataservice table
        // get that service's id and take it as the addOnActivation.DataServiceId
        
        addOnActivation.UserId = uid;
        addOnActivation.AddOnId = aoid;
        addOnActivation.ActivatedDateTime = (DateTime)DateTime();
        addOnActivation.DataUsage = 0;

        var add_on_activation = addOnActivation.ToAddOnActivation();
        _context.AddOnActivation.Add(add_on_activation);
        await _context.SaveChangesAsync();

        // var addOn = addOnDTO.ToAddOn();
        // _context.AddOn.Add(addOn);
        // await _context.SaveChangesAsync();
        return Ok("AddOn Activated Successfully");
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