using Dream.Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.IService
{
    public interface IAccountService
    {
        /// <summary>
        /// 通过账号密码登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        UserOutput Login(string userId, string pwd);
    }
}
