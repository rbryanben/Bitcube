using bitcube.Model;
using bitcube.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace bitcube.Controllers
{
    
    [ApiController]
    [Route("api/v1")]
    public class Authorization : Controller
    {
        private readonly BitcubeContext dbContext; 

        /* 
         *   On construction setup the dbContext 
         */
        public Authorization()
        {
            this.dbContext = new BitcubeContext();
        }


        /*
         *   Create a new user and receive and API key using this endpoint 
         *   POST (JSON) 
         *      firstname : string 
         *      lastname : string 
         *      username : string
         *   Requirements:  
         *      - username to be unique 
         */
        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> createUser([FromBody] UserViewModel user)
        {
            // Check if the username is not taken 
            if (await dbContext.users.AnyAsync(dbUser => dbUser.username == user.username))
            {
                return Conflict(Utils.collection.errorResponse(409,"username taken"));
            }

            // Generate an api key and check if the key does not exist 
            string apiKey = null;

            while (apiKey == null || await dbContext.users.AnyAsync(dbUser => dbUser.apiKey == apiKey))
            {
                apiKey = Utils.collection.generateKey();
            }


            // Create the user 
            var newUser = new Model.User(
                user.username, 
                user.firstname, 
                user.lastname, 
                apiKey);

            // Add and commit the database changes
            await dbContext.users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();
        
            // Return the api key 
            return Ok(new
            {
                username = newUser.username,
                apiKey = newUser.apiKey
            });   
        }
    }
}
