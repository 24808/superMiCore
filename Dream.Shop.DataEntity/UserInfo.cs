using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.DataEntity
{
    public class UserInfo :BaseMod
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

    }
}
