using Microsoft.AspNetCore.Mvc;
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
    
    // 2. Remove an existing Add-on
    // done using postman (no front end for user) 
    // task of a admin
    
    // 3. Update an existing Add-on in the database
    // done using postman (no front end for user) 
    // task of a admin
    
    // 4. Get all the Add-ons in the database
    // done using postman (no front end for user) 
    // task of a admin
    
    // 6. Add a new addon to a user
    // hint: you should get the user id and addon id from front-end
    // task of a user
}