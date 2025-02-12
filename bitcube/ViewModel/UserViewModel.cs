using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.ViewModel
{
    public class UserViewModel
    {
        [Required]
        [MinLength(3,ErrorMessage = "username is too short")]
        public string username { get; set;  }

        [Required]
        [MinLength(3,ErrorMessage = "password is too short")]
        public string firstname { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="lastname is too short")]
        public string lastname { get; set; }
    }
}
