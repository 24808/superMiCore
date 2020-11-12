using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class UserDTO
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string PhoneNum { get; set; }

        public string Email { get; set; }
    }
}
