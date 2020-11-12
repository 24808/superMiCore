using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.Models
{
    public class User_AddressDTO
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsDelect { get; set; }
    }
}
