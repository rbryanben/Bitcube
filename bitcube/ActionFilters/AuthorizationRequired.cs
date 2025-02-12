using bitcube.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.ActionFilters
{
    public class AuthorizationRequired : ActionFilterAttribute
    {
        // Variables 
        private const string AUTHORIZATION_HEADER = "x-api-key";
        private BitcubeContext dbContext;
        
        // Constructor 
        public AuthorizationRequired()
        {
            this.dbContext = new BitcubeContext();
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the header is present in the request
            if (!context.HttpContext.Request.Headers.ContainsKey(AUTHORIZATION_HEADER))
            {
                // If the header is missing, return a BadRequest
                context.Result = new BadRequestObjectResult(Utils.collection.errorResponse(400,$"{AUTHORIZATION_HEADER} header is required"));
            }

            // Get the authorization header 
            string authorization = context.HttpContext.Request.Headers[AUTHORIZATION_HEADER];

            // Get the user associated with the authorization
            var user = dbContext.users.FirstOrDefault(dbUser => dbUser.apiKey == authorization);

            // Check if the authorization header is valid 
            if (user == null)
            {
                // If the header is missing, return a BadRequest
                context.Result = new UnauthorizedObjectResult(Utils.collection.errorResponse(401, $"invalid {AUTHORIZATION_HEADER}"));
            }

            // Attach the user to the request 
            context.HttpContext.Items["userData"] = user;

            // Check if the key is valid 
            base.OnActionExecuting(context);
        }
    }
}
