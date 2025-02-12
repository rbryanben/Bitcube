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
            var userData = (User) HttpContext.Items["userData"];
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
    }
}
