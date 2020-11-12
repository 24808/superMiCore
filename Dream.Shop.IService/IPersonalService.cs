using Dream.Shop.DataEntity;
using Dream.Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.IService
{
    /// <summary>
    /// 个人中心接口
    /// </summary>
   public interface IPersonalService
    {
        /// <summary>
        /// 获取用户的订单
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        List<User_GoodsOder> GetGood_OrdersList(int? Type, int page, int limte, string userId);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        UserInfo UpdataUser(string pwd, string userId);
        /// <summary>
        /// 查找用户所有的地址
        /// </summary>
        /// <returns></returns>
        List<ShoppingAddress> GetShoppingAddress(string userId);
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="mod">接收用户填写的地址</param>
        /// <returns></returns
        ShoppingAddress AddAddress(User_AddressDTO mod, string userId);
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <returns></returns>
        ShoppingAddress UpAddress(User_AddressDTO addressmod, string userId);
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id">接收地址id</param>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        ShoppingAddress DeleteAddress(int id,string userId);
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="id">接收订单id</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        List<User_GoodsOder> DeleteGoodOrder(int? Type, int page, int limte, int id,string userId);
        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<GoodListOutput> GetCollect(string userid);

        public UserInfo GetUser(string userId);
        public User_AddressDTO GetAddress(string userId);
    }
}
