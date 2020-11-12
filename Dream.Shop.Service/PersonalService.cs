using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Dream.Shop.Service
{
    /// <summary>
    /// 个人中心实现类
    /// </summary>
    public class PersonalService : IPersonalService
    {
        private readonly Dream_ShopContext EF;
        public PersonalService(Dream_ShopContext _ef)
        {
            EF = _ef;
        }
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public ShoppingAddress AddAddress(User_AddressDTO mod,string userId)
        {

            var li = EF.UserInfos.FirstOrDefault(a=>a.UserID==userId);
            ShoppingAddress amod = new ShoppingAddress()
            {
                UserId = mod.UserID,
                Name = mod.Name,
                Phone = mod.Phone,
                Address = mod.Address,
                IsDelect=true
            };
            EF.ShoppingAddresses.Add(amod);
            if (EF.SaveChanges()>0)
            {
                return amod;
            }
            return null;

        }

        public List<User_GoodsOder> GetUser_Goods()
        {
            List<GoodOrderedDTO> dto = new List<GoodOrderedDTO>();
            dto = (from a in EF.Goods
                   join b in EF.Good_Orders on a.GoodId equals b.GoodId
                   join c in EF.Orders on b.OrderId equals c.OrderId
                   //join d in EF.Good_Specifications on a.GoodId equals d.GoodId
                   //join e in EF.GoodSpecifications on d.SpecificationId equals e.SpecificationId
                   select new GoodOrderedDTO()
                   {
                       GoodId = a.GoodId,
                       OrderId = b.OrderId,
                       GoodName = a.GoodName,
                       SpecificationName = b.SpecificationName,
                       Quantity = b.Quantity,
                       ImgUrl = "https://localhost:44363/file/image/200/" + a.ThumbnailImg,
                       Price = b.Price
                   }
                 ).ToList();


            List<User_GoodsOder> list = new List<User_GoodsOder>();
            //var data = dto.Where(x => x.OrderId == "Or1006").ToList();
            list = (from o in EF.Orders
                    join u in EF.UserInfos on o.UserID equals u.UserID
                    join go in EF.Good_Orders on o.OrderId equals go.OrderId
                    join g in EF.Goods on go.GoodId equals g.GoodId
                    //join gs in EF.Good_Specifications on g.GoodId equals gs.GoodId
                    //join godsp in EF.GoodSpecifications on gs.SpecificationId equals godsp.SpecificationId
                    select new User_GoodsOder()
                    {
                        OrderId = o.OrderId,
                        UserId = u.UserID,
                        UserName = u.UserName,
                        Price = o.TotalPrice,
                        Address = o.Address,
                        OrderType = o.OrderType,
                        //ChlidClass = dto.Where(a => a.OrderId == o.OrderId).ToList(),
                        CreateTime = o.CreateTime,
                        IsDelect = o.IsDelect
                    }).ToList();

            foreach (var item in list)
            {
                item.ChlidClass = dto.Where(a => a.OrderId == item.OrderId).ToList();
            }

            return list;
        }

        /// <summary>
        /// 获取用户的所有订单
        /// </summary>
        /// <param name="Type">订单类型</param>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        public List<User_GoodsOder> GetGood_OrdersList(int? Type, int page, int limte, string userId)
        {
            if (Type.HasValue)
            {
                var li = GetUser_Goods().Where(a => a.OrderType == Type && a.UserId == userId && a.IsDelect == true).OrderByDescending(a => a.CreateTime).Skip((page - 1) * limte).Take(limte).ToList();
                return li;
            }
            else
            {
                var li = GetUser_Goods().Where(a => a.IsDelect == true && a.UserId == userId).OrderByDescending(a => a.CreateTime).Skip((page - 1) * limte).Take(limte).ToList();
                return li;
            }
        }
        /// <summary>
        /// 获取用户的所有地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ShoppingAddress> GetShoppingAddress(string userId)
        {
            var li = EF.ShoppingAddresses.Where(a => a.UserId == userId&a.IsDelect==true).ToList();
            return li;
        }

        #region 删除
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ShoppingAddress DeleteAddress(int id, string userId)
        {
            var addressmod = EF.ShoppingAddresses.FirstOrDefault(a => a.Id == id && a.UserId == userId);
            ////删除地址
            //EF.ShoppingAddresses.Remove(addressmod);
            addressmod.IsDelect = false;
            if (EF.SaveChanges() > 0)
            {
                return addressmod;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<User_GoodsOder> DeleteGoodOrder(int? Type, int page, int limte, int id, string userId)
        {
            var odermod = EF.Orders.FirstOrDefault(a => a.Id == id && a.UserID == userId);
            //EF.Orders.Remove(odermod);
            odermod.IsDelect = false;
            if (EF.SaveChanges() > 0)
            {
                return GetGood_OrdersList(Type, page, limte, userId);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetUser(string userId)
        {
            var li = EF.UserInfos.FirstOrDefault(a => a.UserID == userId);
            return li;
        }
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User_AddressDTO GetAddress(string userId)
        {
            var li = EF.ShoppingAddresses.FirstOrDefault(a => a.UserId == userId && a.IsDelect == true);
            
            return null;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="usermod"></param>
        /// <returns></returns>
        public UserInfo UpdataUser(string pwd, string userId)
        {
            var li = GetUser(userId);
                li.PassWord = pwd;
            if (EF.SaveChanges() > 0)
            {
                return GetUser(userId);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="addressmod"></param>s
        /// <returns></returns>
        public ShoppingAddress UpAddress(User_AddressDTO addressmod, string userId)
        {
            var li = GetAddress(userId);
            var mod = EF.ShoppingAddresses.FirstOrDefault(a => a.Id == addressmod.Id && a.UserId == userId);
            mod.UserId = userId;
            mod.Name = addressmod.Name;
            mod.Phone = addressmod.Phone;
            mod.Address = addressmod.Address;
            mod.IsDelect = true;
            if (EF.SaveChanges() > 0)
            {
                return mod;
            }
            else
            {
                return null;
            }
        }

        public List<GoodListOutput> GetCollect(string userid)
        {
            throw new NotImplementedException();
        }


        #endregion
        ///// <summary>
        ///// 获取用户收藏列表
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <returns></returns>
        //public List<GoodListOutput> GetCollect(string userid)
        //{
        //    var goodlist = new List<GoodListOutput>();
        //    var collectList = EF.GoodCollects.Where(a => a.UserId == userid).ToList();
        //    foreach (var item in collectList)
        //    {
        //        var goodmod = new GoodListOutput()
        //        {
        //            UserId = userid,
        //            GoodId = item.GoodId,
        //            GoodName = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).GoodName,
        //            Price = GetGoodPrice(item.GoodId, ""),
        //            ThumbnailImg = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).ThumbnailImg,
        //            SpecificationList = GetChild(item.GoodId, ""),

        //        };
        //        goodlist.Add(goodmod);
        //    }
        //    return goodlist;
        //}
        ///// <summary>
        ///// 获取商品规格
        ///// </summary>
        ///// <param name="goodid"></param>
        ///// <param name="pid"></param>
        ///// <returns></returns>
        //public List<SpecificationOutput> GetChild(string goodid, string pid)
        //{
        //    var data = EF.Good_Specifications.Where(a => a.ParentId == pid && a.IsDelect == true).ToList();
        //    if (data.Count == 0)
        //    {
        //        return null;
        //    }
        //    var chlidlist = (from a in EF.GoodSpecifications
        //                     join b in data on a.SpecificationId equals b.SpecificationId
        //                     where b.GoodId == goodid && b.ParentId == pid && a.IsDelect == true && b.IsDelect == true
        //                     select new SpecificationOutput
        //                     {
        //                         SpecificationId = a.SpecificationId,
        //                         ParentId = b.ParentId,
        //                         SpecificationName = a.SpecificationName,
        //                         SpecificationNum = EF.Good_Specifications.Where(a => a.SpecificationId == a.SpecificationId && a.IsDelect == true).ToList(),
        //                         Chlid = GetChild(goodid, a.SpecificationId),
        //                     }).Distinct().ToList();


        //    return chlidlist;
        //}
        ///// <summary>
        ///// 获取商品价格
        ///// </summary>
        ///// <param name="goodid"></param>
        ///// <returns></returns>
        //public decimal GetGoodPrice(string goodid, string gsid)
        //{
        //    if (gsid != "")
        //    {
        //        return Convert.ToDecimal(EF.Good_Specifications.OrderBy(a => a.Price).FirstOrDefault(a => a.GSId == gsid && a.IsDelect == true).Price);
        //    }
        //    return Convert.ToDecimal((from a in EF.Good_Specifications
        //                              join c in EF.GoodSpecifications on a.SpecificationId equals c.SpecificationId
        //                              where a.GoodId == goodid && a.IsDelect == true && c.IsDelect == true
        //                              select new Good_Specification
        //                              {
        //                                  Price = EF.Good_Specifications.FirstOrDefault(a => a.SpecificationId == c.SpecificationId && a.IsDelect == true).Price
        //                              }).FirstOrDefault().Price);
        //}



    }
}
