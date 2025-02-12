using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Model
{
    public class User
    {
        [Key]
        [Required]
        public string username { get; set; }
        [Required]
        public string firstname { get; set;  }
        [Required]
        public string lastname { get; set; }

        [Required]
        public DateTime last_updated { get; set;  }

        [Required]
        public DateTime created { get; set;  }
    }
}
