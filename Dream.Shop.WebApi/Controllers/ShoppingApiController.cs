using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Dream.Shop.WebApi.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Dream.Shop.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingApiController : ControllerBase
    {
        private readonly IShoppingCartService shopping;
        public ShoppingApiController(IShoppingCartService _shopping) : base()
        {
            this.shopping = _shopping;
        }
        [HttpGet]
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
        [HttpGet]
        public ResData ShiooingCarList()
        {
            var userid = "";
            if(GetCookies("userid") != "")
            {
                userid = GetCookies("userid");
            }
            else
            {
                return ResData.Fail("未登录");
            }
            var list = shopping.GetShoppingCarList(userid);
            return ResData.Succ(list);
        }
        [HttpPost]
        public ResData jianShoppingCar([FromForm] int type, [FromForm] string goodid )
        {
            var userid = "";
            if (GetCookies("userid") != "")
            {
                userid = GetCookies("userid");
            }
            else
            {
                return ResData.Fail("未登录");
            }
            var list = shopping.jianShoppingCar(goodid, userid,type);
            return ResData.Succ(list);
        }
        [HttpPost]
        public ResData fanShoppingCar([FromForm]  string goodid = "G1001")
        {
            var userid = "";
            if (GetCookies("userid") != "")
            {
                userid = GetCookies("userid");
            }
            else
            {
                return ResData.Fail("未登录");
            }
            var list = shopping.fanShoppingCar(goodid, userid);
            return ResData.Succ(list);
        }
        [HttpPost]
        public ResData IntoShoppingCar([FromForm] string goodid="G1001", [FromForm]string specificationarr= "GS1004,GS1001", [FromForm] decimal price=5999)
        {
            var userid = "";
            if (GetCookies("userid") != "")
            {
                userid = GetCookies("userid");
            }
            else
            {
                return ResData.Fail("未登录");
            }
            var list = shopping.IntoShoppingCar(goodid,userid,specificationarr,price);
            return ResData.Succ(list);                                        
        }
        [HttpPost]
        public ResData ShiooingCarDelect([FromForm] string goodid)
        {
            var userid = "";
            if (GetCookies("userid") != "")
            {
                userid = GetCookies("userid");
            }
            else
            {
                return ResData.Fail("未登录");
            }
            var list = shopping.ShoppingCarDelect(goodid, userid);
            return ResData.Succ(list);
        }
        [HttpPost]
        public ResData meiyoude([FromForm] string list)
        {
            //Newtonsoft.Json

            var a4= Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShoppingCar>>(list);



            return ResData.Succ(list);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="sett"></param>
        /// <returns></returns>
        [HttpPost]
        public ResData CreateOrderList([FromForm] string sett)
        {
            var userid = "";
            if (GetCookies("userid") != "")
            {
                userid = GetCookies("userid");
            }
            else
            {
                return ResData.Fail("未登录");
            }
            var list = shopping.CreateOrder(userid, sett);
            return ResData.Succ(list);
        }
        /// <summary>
        /// 支付按钮点击
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="addressmod"></param>
        /// <param name="couponid"></param>
        /// <param name="totalPrice"></param>
        /// <returns></returns>
        [HttpPost]
        public ResData SettleBtnClick([FromForm] string orderid, [FromForm] User_AddressDTO addressmod, [FromForm] string couponid,[FromForm] decimal? totalPrice)
        {
            var list = shopping.SettleClick(orderid, addressmod, couponid, totalPrice);
            return ResData.Succ(list);
        }
        /// <summary>
        /// 更改订单状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpPost]
        public ResData ChangeOrderType([FromForm]string orderid)
        {
            var list = shopping.ChangeOrder(orderid);
            return ResData.Succ(list);
        }
        /// <summary>
        /// 优惠券信息
        /// </summary>
        /// <param name="couponid"></param>
        /// <returns></returns>
        [HttpGet]
        public ResData GetCoupons(string couponid)
        {
            var list = shopping.GetCoupon(couponid);
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData GetSettlements(string orderid)
        {
            var userid = "";
            if (GetCookies("userid") != "")
            {
                userid = GetCookies("userid");
            }
            else
            {
                return ResData.Fail("未登录");
            }
            var list = shopping.GetSettlementsList(userid, orderid);
            return ResData.Succ(list);
        }
    }
}
