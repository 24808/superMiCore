using System;
using System.Collections.Generic;
using System.Text;
using Dream.Shop.Models;
using Dream.Shop.DataEntity;


namespace Dream.Shop.IService
{
    public interface IShoppingCartService
    {

        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<ShoppingCarOutput> GetShoppingCarList(string userid);
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<ShoppingCarOutput> IntoShoppingCar(string goodid,string userid,string specificationIdArr,decimal price);
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<ShoppingCarOutput> jianShoppingCar(string goodid, string userid,int type);
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<ShoppingCarOutput> fanShoppingCar(string goodid, string userid);
        
        /// <summary>
        /// 获取订单页
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="carlist"></param>
        /// <returns></returns>
        SettlementOutput GetSettlementsList(string userid,string orderid);
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        List<ShoppingCarOutput> ShoppingCarDelect(string goodid, string userid);
        /// <summary>
        /// 获取优惠券列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodlist"></param>
        /// <returns></returns>
        List<CouponOutput> GetCouponList(string userid, List<ShoppingCar> goodlist);
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sett"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Order CreateOrder(string userid, string sett);
        /// <summary>
        /// 点击支付
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="address"></param>
        /// <param name="couponid"></param>
        /// <returns></returns>
        Order SettleClick(string orderid, User_AddressDTO address, string couponid, decimal? totalPrice);
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        Order ChangeOrder(string orderid);
        /// <summary>
        /// 优惠券详情
        /// </summary>
        /// <param name="couponid"></param>
        /// <returns></returns>
        CouponOutput GetCoupon(string couponid);

    }
}
