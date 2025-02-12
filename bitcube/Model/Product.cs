using bitcube.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string productId { get; set; }
        
        [Required]
        public string productName { get; set;  }

        [Required]
        public double productPrice { get; set; }

        [Required]
        public long quantity { get; set; }

        [Required]
        public User createdBy { get; set; } = null!;

        [Required]
        public DateTime created { get; set; } 

        [Required]
        public DateTime last_updated { get; set; }

        /*
         *  Constructors 
         */
        public Product(string productId,string productName,double productPrice,long quantity, User creator)
        {
            this.productId = productId;
            this.productName = productName;
            this.productPrice = productPrice;
            this.quantity = quantity;
            this.createdBy = creator;
            this.created = DateTime.Now;
            this.last_updated = DateTime.Now;
        }
        public Product() { }

        /*
         *  This will set any changed field from the given UserViewModel 
         *  It will return a clone of the old object, after applying the new changes 
         */
        public Product applyChangesFromUserViewModel(ProductViewModel modelWithChanges)
        {
            // Previous state
            var prevObject = (Product) this.MemberwiseClone();

            // Apply changes
            this.productName = modelWithChanges.product_name;
            this.productPrice = modelWithChanges.product_price;
            this.quantity = modelWithChanges.quantity;
            this.last_updated = DateTime.Now;

            return prevObject;
        }

        /*
         *  Filter object to remove all sensetive information
         */
        public object getFilteredObject()
        {
            return new
            {
                productId = productId,
                productName = productName,
                productPrice = productPrice,
                quantity = quantity,
                createdBy = this.createdBy.username,
                created = DateTime.Now,
                last_updated = DateTime.Now
            };
        }

    }
}
