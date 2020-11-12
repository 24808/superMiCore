using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Dream.Shop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dream.Shop.Service
{
    public class LoginService: ILogingService
    {
        private readonly Dream_ShopContext EF;
        public LoginService(Dream_ShopContext _ef)
        {
            EF = _ef;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserDTO GetLogin(string userId,string pwd)
        {
            //var Pwd = MD5Helper.EncryptString(pwd);
            var li = EF.UserInfos.FirstOrDefault(a=>a.UserID==userId&&a.PassWord== pwd);
            if (li != null)
            {
                UserDTO moddto = new UserDTO()
                {
                    UserId=li.UserID,
                    UserName=li.UserName,
                    Password=li.PassWord,
                    PhoneNum=li.PhoneNum,
                    Email=li.Email
                };

                return moddto;
            }
            else
            {
                return null;
            }
        }
    }
}
