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
            var userData = (User)HttpContext.Items["userData"];
            var user = await dbContext.users.FirstOrDefaultAsync(dbUser => dbUser.username == userData.username);

            // Check if the product id exists 
            if (await dbContext.products.AnyAsync(dbProduct => dbProduct.productId == product.product_id))
            {
                return Conflict(Utils.collection.errorResponse(409, $"product with id {product.product_id} already exists"));
            }

            // Create the product 
            var newProduct = new Product(
                product.product_id,
                product.product_name,
                product.product_price,
                product.quantity,
                user
            );

            // Add and commit 
            await dbContext.products.AddAsync(newProduct);
            await dbContext.SaveChangesAsync();

            // Return the product id and owner of the product 
            return Ok(new
            {
                product_id = newProduct.productId,
                owner = user.username
            });
        }


        /*
        *   Get all products in the order of their creation 
        *   GET: 
        *      count : default 200, max 500
        *      index : -1 
        *   Requirements:  
        *      - authorization is required
        */
        [HttpGet]
        [Route("get-products")]
        [AuthorizationRequired]
        public async Task<IActionResult> getProducts(int count = 200, int index = 0)
        {
            // Cap count 
            count = Math.Min(count, 500);

            // Fetch all products 
            var products = await dbContext.products
                .OrderBy(dbProduct => dbProduct.id)
                .Take(count)
                .Select(p => new
                {
                    id = p.id,
                    product_id = p.productId,
                    product_name = p.productName,
                    price = p.productPrice,
                    owner = p.createdBy.username,
                    quantity = p.quantity,
                    created = p.created,
                    last_update = p.last_updated
                })
                .Where(p => p.id >= index).ToListAsync();

            // Reverse the list
            products.Reverse();

            // Return the products
            return Ok(products);
        }

        /*
        *   Delete a product given a product_id 
        *   DELETE: 
        *      product_id : string 
        *   Requirements:  
        *      - authorization is required
        *      - user has to be the owner of the product
        */
        [HttpDelete]
        [Route("delete-product")]
        [AuthorizationRequired]
        public async Task<IActionResult> deleteProduct(string product_id = null)
        {

            // Check if product_id is null 
            if (String.IsNullOrEmpty(product_id))
            {
                return BadRequest(Utils.collection.errorResponse(400, "product_id cannot be null or empty"));
            }

            // Get the user 
            var userData = (User)HttpContext.Items["userData"];

            // Get the product 
            var product = await dbContext.products.Include(dbProduct => dbProduct.createdBy).FirstOrDefaultAsync(dbProduct => dbProduct.productId == product_id);

            // Check if the product does not exists 
            if (product == null)
            {
                return NotFound(Utils.collection.errorResponse(404, $"product with id {product_id} was not found"));
            }

            // Check if the user is the owner of the product 
            if (product.createdBy.username != userData.username)
            {
                HttpContext.Response.StatusCode = 403;
                return Content($"user not owner of product with id {product_id}");
            }

            // Delete the product 
            dbContext.products.Remove(product);
            await dbContext.SaveChangesAsync();

            return Ok(product_id);
        }

        /*
         *  Modify a product 
         *  PUT:
         *      product_id : string
         *      product_name : string
         *      product_price : double 
         *      quantity : long
         *  Requirements
         *      - authenticated user
         *      - user should be the owner of the product
         */
        [HttpPut]
        [Route("modify-product")]
        [AuthorizationRequired]
        public async Task<IActionResult> modifyProduct([FromBody] ProductViewModel product)
        {
            // Get the user 
            var userData = (User)HttpContext.Items["userData"];

            // fetch the product from the database 
            var dbProduct = await dbContext.products.Include(dbProduct => dbProduct.createdBy).FirstOrDefaultAsync(dbProduct => dbProduct.productId == product.product_id);

            // Check if the product does not exists 
            if (dbProduct == null)
            {
                return NotFound(Utils.collection.errorResponse(404, $"product with id {product.product_id} was not found"));
            }

            // Check if the user is the owner of the product 
            if (dbProduct.createdBy.username != userData.username)
            {
                HttpContext.Response.StatusCode = 403;
                return Content($"user not owner of product with id {dbProduct.productId}");
            }

            // apply the change and save the changes
            var prevState = dbProduct.applyChangesFromUserViewModel(product);
            dbContext.SaveChanges();

            // Return the old object and the new one
            return Ok(new
            {
                new_object = dbProduct.getFilteredObject(),
                old_object = prevState.getFilteredObject()
            });
        }
    }
}
