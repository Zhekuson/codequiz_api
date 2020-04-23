using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Users
{
    public class GoogleUser
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
