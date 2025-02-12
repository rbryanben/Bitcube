using bitcube.ViewModel;
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

        [Required]
        public string apiKey { get; set; } // Since we are generating only one on user creation


        // Constructor 
        public User(string username,string firstname, string lastname,string apiKey)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.last_updated = DateTime.Now;
            this.created = DateTime.Now;
            this.apiKey = apiKey;
        }
    }
}
