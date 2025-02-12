using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.ViewModel
{
    public class AddToCartViewModel
    {

        // Inner class for an individual entry 
        public class ProductRefViewModel
        {
            [Required]
            public string product_id { get; set; }

            [Required]
            public long quantity { get; set; }
        }


        [Required]
        public IEnumerable<AddToCartViewModel.ProductRefViewModel> products { get; set; }
    }
}
