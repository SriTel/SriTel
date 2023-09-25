using Microsoft.AspNetCore.Mvc;
using SriTel.Models;

namespace SriTel.Controllers;

public class TelcoServiceController : Controller
{
    private readonly SriTelContext _context;

    public TelcoServiceController(SriTelContext context)
    {
        _context = context;
    }
    // 1. Add a new Telco Service to the database
    // done using postman (no front end for user) 
    // task of a admin
    
    // 2. Remove an existing Telco Service
    // done using postman (no front end for user) 
    // task of a admin
    
    // 3. Update an existing Telco Service in the database
    // done using postman (no front end for user) 
    // task of a admin
    
    // 4. Get all the Telco Services in the database
    // done using postman (no front end for user) 
    // task of a admin
    
    // 5. Enable a Telco Service for a user
    // eg: enable internet service, voice service, roaming service or ringing tone service
    // task of a user
    
    // 6. Disable a Telco Service for a user
    // task of a user
}