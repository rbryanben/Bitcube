using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.ViewModel
{
    public class ProductViewModel
    {
        [Required]
        [MinLength(4,ErrorMessage ="product id should be at least 4 chars long i.e. A001")]
        public string product_id { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="product name should be at least 3 chars long")]
        public string product_name { get; set; }

        [Required]
        public double product_price { get; set; }

        [Required]
        public long quantity { get; set; }
    }
}
