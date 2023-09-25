using Microsoft.AspNetCore.Mvc;
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
    
    // 2. Remove an existing Package
    // done using postman (no front end for user) 
    // task of a admin
    
    // 3. Update an existing Package in the database
    // done using postman (no front end for user) 
    // task of a admin
    
    // 4. Get all the Packages in the database
    // done using postman (no front end for user) 
    // task of a admin
    
    // 5. Activate a package for a user
    // hint: get the package id and user id from front-end.
    // task of a user
    
    // 6. Upgrade or Downgrade a package for a user
    // task of a user
    
    // 7. Deactivate a package for a user
    // task of a user
}