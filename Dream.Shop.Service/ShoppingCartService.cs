using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dream.Shop.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ILogger<HomePageService> _logger;
        private readonly Dream_ShopContext EF;
        public ShoppingCartService(ILogger<HomePageService> logger, Dream_ShopContext dbContext)
        //: base(dbContext)
        {
            _logger = logger;
            EF = dbContext;
        }
        /// <summary>
        /// 获取购物车页面
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<ShoppingCarOutput> GetShoppingCarList(string userid)

        {
            var cardata = EF.ShoppingCars.Where(a => a.UserId == userid).ToList();
            var list = new List<ShoppingCarOutput>();
            foreach (var item in cardata)
            {
                var mod = new ShoppingCarOutput()
                {
                    Amount = item.Amount,
                    Price = item.Price,
                    Title = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).Title,
                    Stock = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).Stock,
                    GoodId = item.GoodId,
                    GoodName = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).GoodName,
                    ImgUrl = "https://localhost:44363/file/image/200/" + EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).ThumbnailImg,
                    SpecificationName = item.SpecificationName,
                    ischeck=item.IsDelect

                };
                list.Add(mod);
            }
            return list;
        }

        /// <summary>
        /// 获取选取规格值
        /// </summary>
        /// <param name="specificationarr"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string GetSpecificationName(string ArrSpecification)
        {
            string[] arr = ArrSpecification.Split(',');
            var name = "";
            
            foreach (var item in arr)
            {
                if (item != "")
                {
                    name += " " + EF.Good_Specifications.FirstOrDefault(a => a.GSId == item && a.IsDelect == true).SpecificationNum;
                }
            }
            return name;
        }
        public List<ShoppingCarOutput> fanShoppingCar(string goodid, string userid)
        {
            var data = EF.ShoppingCars.FirstOrDefault(a => a.GoodId == goodid && a.UserId == userid);
            if (data != null)
            {
                var dui = !data.IsDelect;
                //data.IsDelect = dui;
                data.IsDelect = dui;
            }
            if (EF.SaveChanges() > 0)
            {
                return GetShoppingCarList(userid);
                //return null;
            }
            return null;
        }

        public List<ShoppingCarOutput> jianShoppingCar(string goodid, string userid,int type)
        {
            var data = EF.ShoppingCars.FirstOrDefault(a => a.GoodId == goodid && a.UserId == userid );
            if (data != null)
            {
                if (type == 0)
                {
                    data.Amount -= 1;

                }
                else
                {
                    data.Amount += 1;
                }
            }
            if (EF.SaveChanges() > 0)
            {
                return GetShoppingCarList(userid);
                //return null;
            }
            return null;
        }
            /// <summary>
            /// 加入购物车
            /// </summary>
            /// <param name="goodid"></param>
            /// <param name="userid"></param>
            /// <param name="specificationarr"></param>
            /// <returns></returns>

            public List<ShoppingCarOutput> IntoShoppingCar(string goodid, string userid, string specificationIdArr,decimal price)
        {
         
            var data = EF.ShoppingCars.Where(a => a.GoodId == goodid && a.UserId == userid).ToList();
            if (data.Count != 0)
            {
                data.FirstOrDefault().Amount += 1;
            }
            else
            {
                var mod = new ShoppingCar
                {
                    Amount = 1,
                    Price = price,
                    CreateTime = DateTime.Now,
                    GoodId = goodid,
                    IsDelect = true,
                    UserId = userid,
                    SpecificationName = GetSpecificationName(specificationIdArr) ,
                };
                EF.ShoppingCars.Add(mod);
            }
            if (EF.SaveChanges() > 0)
            {
                  return GetShoppingCarList(userid);
                //return null;
            }
            return null;
        }
        /// <summary>
        /// 结算页展示
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="carlist"></param>
        /// <returns></returns>
        public SettlementOutput GetSettlementsList(string userid, string orderid)
        {
            var data = EF.Good_Orders.Where(a => a.OrderId == orderid).ToList();
            var goodlist = new List<GoodListOutput>();
            var cartlist = new List<ShoppingCar>();

            if (data.Count == 0)
            {
                return null;
            }
            foreach (var item in data)
            {
                var goodmod = new GoodListOutput()
                {
                    UserId = userid,
                    Amount = item.Quantity,
                    GoodId = item.GoodId,
                    GoodName = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).GoodName,
                    Price = item.Price,
                    ThumbnailImg = "https://localhost:44363/file/image/200/" + EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).ThumbnailImg,
                    SpecificationName = item.SpecificationName,
                };
                goodlist.Add(goodmod);
                var cartmod = new ShoppingCar()
                {
                    Amount = item.Quantity,
                    CreateTime = item.CreateTime,
                    GoodId = item.GoodId,
                    IsDelect = item.IsDelect,
                    Price = item.Price,
                    UserId = userid,
                    SpecificationName = item.SpecificationName,
                };
                cartlist.Add(cartmod);
            }

            var list = new SettlementOutput()
            {
                GetGoodLists = goodlist,
                GetGoodCoupons = GetCouponList(userid, cartlist),
            };
            return list;
        }

        /// <summary>
        /// 获取优惠券列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodlist"></param>
        /// <returns></returns>
        public List<CouponOutput> GetCouponList(string userid, List<ShoppingCar> goodlist)
        {
            var list = new List<CouponOutput>();

            if (goodlist == null)
            {
                var data = EF.GoodCoupons.Where(a => a.UserId == userid && a.IsDelect == true).ToList();
                foreach (var item in data)
                {
                    var mod = new CouponOutput()
                    {
                        CouponId = item.CouponId,
                        CouponName = item.CouponName,
                        Meet = item.Meet,
                        DiscountedPrice = item.DiscountedPrice,
                        StartTiem = item.StartTiem,
                        EndTime = item.EndTime,
                        State = item.State,
                    };

                    list.Add(mod);
                };
                return list;
            };
            foreach (var item in goodlist)
            {
                var mod = (from a in EF.GoodCoupons
                           join b in EF.Good_Coupons on a.CouponId equals b.CouponId
                           where b.CouponId == a.CouponId && b.GoodId == item.GoodId && a.UserId == item.UserId && a.IsDelect == true && b.IsDelect == true
                           select new CouponOutput
                           {
                               CouponId = a.CouponId,
                               CouponName = a.CouponName,
                               DiscountedPrice = a.DiscountedPrice,
                               StartTiem = a.StartTiem,
                               EndTime = a.EndTime,
                               Meet = a.Meet,
                               State = a.State,
                               JudgeMeet = false,
                           }).Distinct().ToList();
                list.AddRange(mod);
            };
            var coulist = new List<CouponOutput>();
            foreach (var item in list)
            {
                if (coulist.Where(a => a.CouponId == item.CouponId).ToList().Count == 0)
                {
                    var coumod = item;
                    coulist.Add(coumod);
                }
            }
            foreach (var item in coulist)
            {
                var goodcoupon = EF.Good_Coupons.Where(a => a.CouponId == item.CouponId && a.IsDelect == true).ToList();
                decimal meetPrice = 0;

                foreach (var x in goodcoupon)
                {
                    foreach (var i in goodlist)
                    {
                        if (x.GoodId == i.GoodId)
                        {
                            meetPrice += i.Price;
                        }
                    }
                }
                if (meetPrice > item.Meet)
                {
                    item.JudgeMeet = true;
                    EF.SaveChanges();
                }
            }
            return coulist;
        }
        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="number"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<ShoppingCarOutput> ShoppingCarDelect(string goodid, string userid)
        {
            var data = EF.ShoppingCars.FirstOrDefault(a => a.GoodId == goodid && a.IsDelect == true);
            if (data == null)
            {
                return null;
            }
            EF.Remove(data);
            if (EF.SaveChanges() > 0)
            {
                return GetShoppingCarList(userid);
            };
            return null;
        }
        /// <summary>
        /// 获取优惠券
        /// </summary>
        /// <param name="couponid"></param>
        /// <returns></returns>
        public CouponOutput GetCoupon(string couponid)
        {
            var data = EF.GoodCoupons.FirstOrDefault(a => a.CouponId == couponid && a.IsDelect == true);
            return new CouponOutput()
            {
                CouponId = data.CouponId,
                CouponName = data.CouponName,
                Meet = data.Meet,
                DiscountedPrice = data.DiscountedPrice,
                StartTiem = data.StartTiem,
                EndTime = data.EndTime,
                State = data.State,
            };
        }



        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        public Order CreateOrder(string userid, string shoppingcartlist)
        {
            var goodlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShoppingCar>>(shoppingcartlist);


            var data = EF.Orders.OrderByDescending(a => a.Id).ToList();
            var mod = new Order()
            {

                CreateTime = DateTime.Now,
                IsDelect = true,
                OrderId = data.Count != 0 ? "Or100" + data.FirstOrDefault().Id : "Or1001",
                OrderType = 1,
                UserID = userid,

            };
            EF.Orders.Add(mod);
            foreach (var item in goodlist)
            {
                var order = new Good_Order()
                {
                    CreateTime = DateTime.Now,
                    GoodId = item.GoodId,
                    IsDelect = true,
                    Price = item.Price,
                    Quantity = item.Amount,
                    OrderId = mod.OrderId,
                    SpecificationName = item.SpecificationName,
                };
                EF.Good_Orders.Add(order);
                ShoppingCarDelect(item.GoodId, userid);
            }
           
                return mod;
            
        }
        /// <summary>
        /// 点击支付
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public Order SettleClick(string orderid, User_AddressDTO addressmod, string couponid,decimal? totalPrice)
        {
            //var addressmod = Newtonsoft.Json.JsonConvert.DeserializeObject<User_AddressDTO>(address);
            var data = EF.Orders.FirstOrDefault(a => a.OrderId == orderid);
            var mod = EF.Orders.FirstOrDefault(a => a.OrderId == orderid);
            mod.CouponId = couponid;
            mod.Address = addressmod.Address;
            mod.Phone = addressmod.Phone;
            mod.Name = addressmod.Name;
            mod.TotalPrice = totalPrice;
            if (EF.SaveChanges() > 0)
            {
                var mod1 = EF.Orders.FirstOrDefault(a => a.OrderId == orderid);
                return mod1;
            }
            return null;
        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public Order ChangeOrder(string orderid)
        {
            var data = EF.Orders.FirstOrDefault(a => a.OrderId == orderid);
            data.OrderType += 1;
            if (EF.SaveChanges() > 0)
            {
                return EF.Orders.FirstOrDefault(a => a.OrderId == orderid);
            }
            return null;
        }


    }
}
