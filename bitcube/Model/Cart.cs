using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Model
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string reference { get; set; }

        [Required]
        public User owner { get; set; }
        
        [Required]
        public bool open { get; set; }

        [Required]
        public DateTime last_updated { get; set; }
        
        [Required]
        public DateTime created { get; set; }

        /*
         *  Constructors 
         */
        public Cart() { }

        public Cart(string reference, User owner, bool open = true)
        {
            this.reference = reference;
            this.owner = owner;
            this.open = open;
            this.created = DateTime.Now;
            this.last_updated = DateTime.Now;
        }

        /*
         *  Returns the cart given the username of the owner
         */
        public async static Task<Cart> getCartUsingUsername(string username,BitcubeContext dbContext)
        {
            // Get all the carts 
            var cart = await dbContext.cart
                .Where(dbCart => dbCart.owner.username == username)
                .Where(dbCart => dbCart.open == true)
                .FirstOrDefaultAsync();

            return cart;
        }
    }
}
