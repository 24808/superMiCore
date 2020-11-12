using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Dream.Shop.Utility;
using Dream.Shop.WebApi.Handler;
using Dream.Shop.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Dream.Shop.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonalController : RegisterApiController
    {
        private readonly IMemoryCache cache;
        private readonly Dream_ShopContext EF;
        private readonly IPersonalService personalI;
        private readonly IRegisterService service;
        private readonly IOptions<EmailMod> options;

        public PersonalController(IMemoryCache _cache, Dream_ShopContext ef, IPersonalService personalI, IOptions<EmailMod> options, IRegisterService service) 
            : base(_cache, options, service)
        {
            cache = _cache;
            EF=ef;
            this.personalI = personalI;
            this.options = options;
            this.service = service;
        }

   
        #region 订单
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        [HttpGet]
        public ResData GetPersonal(int? Type, int page = 1, int limte = 4)
        {
            var user = "";
             user = GetCookies("userid");
            if (user !=  "" )
            {

                var li = personalI.GetGood_OrdersList(Type, page, limte, user);
            if (li.Count != 0)
                return ResData.Succ(li);
            else
                return ResData.Succ(_msg: "暂时没有订单");
            }
            else
            {
                return ResData.Fail("未登录");
            }

        }
        #endregion
        #region 获取地址
        [HttpGet]
        public ResData GetShoppingAddress()
        {
            var user = "";
            user = GetCookies("userid");
            if (user != "")
            {


                var list = personalI.GetShoppingAddress(user);
            return ResData.Succ(list);
            }
            else
            {
                return ResData.Fail("未登录");
            }
        }
        #endregion

        #region 添加
        [HttpPost]
        public ResData AddADDress([FromForm] string Name, [FromForm] string Phone, [FromForm] string Address)
        {
            var user = "";
            user = GetCookies("userid");
            if (user != "")
            {

                var dto = new User_AddressDTO() { 
                Address=Address,
                IsDelect=true,
                Name=Name,
                Phone=Phone,
                UserID= user
                };
            
            var li = personalI.AddAddress(dto, user);
            return GetShoppingAddress();
            }
            else
            {
                return ResData.Fail("未登录");
            }
        }
        #endregion

        /// <summary>
        /// 拿到用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResData GetUser()
        {
            var user = "";
            user = GetCookies("userid");
            if (user != "")
            {


                var li = personalI.GetUser(user);
            return ResData.Succ(li);
            }
            else
            {
                return ResData.Fail("未登录");
            }
        }
        #region 修改
        [HttpPost]
        public ResData UpUserInfo([FromForm] string pwd, [FromForm] string pwdOld, [FromForm] string Code)
        {
            var user = "";
            user = GetCookies("userid");
            if (user != "")
            {
                var li = EF.UserInfos.FirstOrDefault(a => a.UserID == user);
                //var Pwd = MD5Helper.EncryptString(pwd);

                //var PwdOld = MD5Helper.EncryptString(pwdOld);

                if (li.PassWord == pwdOld)
                {

                    if (Code == GetCookies("yz"))
                    {
                        var up = personalI.UpdataUser(pwd, user);
                        return GetUser();
                    }
                    else
                    {
                        return ResData.Succ(_msg: "验证码错误");
                    }
                }
                else
                {
                    return ResData.Succ(_msg: "密码错误");
                }

            } else
            {
                return ResData.Fail();
            }
           
              
         

        }
        #endregion
        #region 删除
        [HttpPost]
        public ResData DeleteGoodOrder([FromForm] int id, [FromForm] int? Type, [FromForm] int page = 1, [FromForm] int limte = 10)
        {
            var user = "";
            user = GetCookies("userid");
            if (user != "")
            {
                var li = personalI.DeleteGoodOrder(Type, limte, page, id, user);
                return GetPersonal(Type, page, limte);
            }
            else
            {
                return ResData.Fail("未登录");
            }
              
        }
        [HttpPost]
        public ResData Delete([FromForm] int id)
        {
            var user = "";
            user = GetCookies("userid");
            if (user != "")
            {
                var li = personalI.DeleteAddress(id, user);
                return GetShoppingAddress();
            }
            else
            {

                return ResData.Fail("未登录");


            }
        }
        #endregion

    }
}
