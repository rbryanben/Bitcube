using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Model
{
    public class CartProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public Cart cart {get; set;}

        [Required]
        public long quantity { get; set; }

        [Required]
        public Product product { get; set; }

        [Required]
        public DateTime last_updated { get; set; }

        [Required]
        public DateTime created { get; set; }

        public CartProduct() { }

        public CartProduct(Cart cart,long quantity ,Product product)
        {
            this.cart = cart;
            this.quantity = quantity;
            this.product = product;
            this.last_updated = DateTime.Now;
            this.created = DateTime.Now;
        }


    }
}
