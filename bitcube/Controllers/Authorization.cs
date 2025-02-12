using bitcube.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Controllers
{
    
    [ApiController]
    [Route("api/v1")]
    public class Authorization : Controller
    {
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
        public IActionResult createUser([FromBody] UserViewModel user)
        {
            return Ok("We are okay");   
        }
    }
}
