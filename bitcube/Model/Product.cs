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
    }
}
