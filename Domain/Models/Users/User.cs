using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public bool isConnectedToGoogle { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public GoogleUser GoogleUser { get; set; }
        public User() {}
    
    }
}
