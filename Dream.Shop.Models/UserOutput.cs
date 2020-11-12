using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class UserOutput
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PhoneNum { get; set; }

        public string Email { get; set; }

        public DateTime AddTime { get; set; }

        public int RoleId { get; set; }
    }
}
