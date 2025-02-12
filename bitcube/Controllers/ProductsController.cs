using bitcube.ActionFilters;
using bitcube.Model;
using bitcube.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProductsController : Controller
    {

        private readonly BitcubeContext dbContext;

        /* 
         *   On construction setup the dbContext 
         */
        public ProductsController()
        {
            this.dbContext = new BitcubeContext();
        }

        /*
        *   Create a new product using this endpoint 
        *   POST (JSON) 
        *      id : string 
        *      name : string 
        *      price : double
        *      quantity : int 
        *   Requirements:  
        *      - authorization is required
        */
        [HttpPost]
        [Route("add-product")]
        [AuthorizationRequired]
        public async Task<IActionResult> addProduct(ProductViewModel product)
        {
            // Get the user 
            var user = (User)HttpContext.Items["user"];


            return Ok("hello there");
        }
    }
}
