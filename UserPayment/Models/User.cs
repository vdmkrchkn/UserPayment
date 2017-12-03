using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserPayment.Models
{
    public class User
    {
        // entity primary key
        [Key]
        public int Id { get; set; }
        // phone number
        public string Login { get; set; }
        // password
        public string Password { get; set; }
    }
}
