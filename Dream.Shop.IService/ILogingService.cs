using Dream.Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.IService
{
    public interface ILogingService
    {
        public UserDTO GetLogin(string userId, string pwd);
    }
}
