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
    public class CartController : Controller
    {

        // Variables 
        private readonly BitcubeContext dbContext;

        // Constructor 
        //  - inject 
        public CartController()
        {
            this.dbContext = new BitcubeContext();
        }


        /*
         *  Add product to a cart; will create a new cart if there is no open cart 
         *  POST:
         *      products : list[AddToCartViewModel] 
         *  Requirements:
         *      - authenticated user 
         *  Returns:
         *      - list of the products and the status 
         */
        [HttpPost]
        [Route("add-to-cart")]
        [AuthorizationRequired]
         public async Task<IActionResult> addToCart([FromBody] AddToCartViewModel addToCartViewModel)
        {
            // Get the user 
            var userData = (User)HttpContext.Items["userData"];
            var user = await dbContext.users.FirstOrDefaultAsync(dbUser => dbUser.username == userData.username);

            // Get the cart associated with the user that is open
            var cart = await Cart.getCartUsingUsername(userData.username, dbContext);

            // If cart is null then create one 
            if (cart == null)
            {
                // Set the cart 
                cart = new Cart(
                        Utils.collection.generateKey(true),
                        user,
                        true
                );

                // Save the cart  
                await dbContext.cart.AddAsync(cart);
                await dbContext.SaveChangesAsync();

            }

            // Resultant list 
            var res = new List<object>();

            // Add each product
            foreach (AddToCartViewModel.ProductRefViewModel productToAdd in addToCartViewModel.products)
            {
                // Check if the product exists
                var dbProduct = dbContext.products.FirstOrDefault(dbProduct => dbProduct.productId == productToAdd.product_id);
                
                // If null then add the result to the resultant list  
                if (dbProduct == null)
                {
                    res.Add(new
                    {
                        product_id = productToAdd.product_id,
                        status = Utils.collection.CommonResponses.PRODUCT_NOT_FOUND.ToString(),
                        item_price = 0,
                        total_price = 0,
                        quantity_in_cart = 0
                    });
                    
                    // continue with the list
                    continue;
                }

                // Check if the quantity is sufficient 
                if (dbProduct.quantity < productToAdd.quantity)
                {
                    res.Add(new
                    {
                        product_id = productToAdd.product_id,
                        status = Utils.collection.CommonResponses.NOT_ENOUGH_STOCK.ToString(),
                        item_price = 0,
                        total_price = 0,
                        quantity_in_cart = 0
                    });

                    // continue with the list
                    continue;
                }

                // Check if the the product is already in the cart then increment 
                if (dbContext.cartProducts
                        .Where(dbCartProduct => dbCartProduct.product == dbProduct)
                        .Where(dbCartProduct => dbCartProduct.cart == cart)
                        .Any())
                {
                    var dbCartProduct = await dbContext.cartProducts
                        .Where(dbCartProduct => dbCartProduct.product == dbProduct)
                        .Where(dbCartProduct => dbCartProduct.cart == cart)
                        .FirstOrDefaultAsync();
                    

                    // Remove the previous record
                    dbContext.Remove(dbCartProduct);
                }

                // CartProduct to add 
                var cartProduct = new CartProduct(
                        cart,
                        productToAdd.quantity,
                        dbProduct
                    );

                // Add product to the cart 
                await dbContext.cartProducts.AddAsync(cartProduct);

                // Add the result to the resultant list 
                res.Add(new
                {
                    product_id = productToAdd.product_id,
                    status = Utils.collection.CommonResponses.SUCCESS.ToString(),
                    item_price = dbProduct.productPrice,
                    total_price = dbProduct.productPrice * cartProduct.quantity,
                    quantity_in_cart = cartProduct.quantity
                });
            }

            // Commit to the database 
            await dbContext.SaveChangesAsync();

            // Get the user  
            return Ok(res);
        }

        /*
         *   
         * 
         */
    }
}
