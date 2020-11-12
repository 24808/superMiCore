using Dream.Shop.DataEntity;
using Dream.Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.IService
{
    public interface IHomePageService
    {
        /// <summary>
        /// 查询首页顶部商品分类
        /// </summary>
        /// <returns></returns>
        List<object> GetCagegoryHomes();
        /// <summary>
        /// 查询首页分类商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        List<HomeCateOutput> GetHomeGood(int page, int limte);
        /// <summary>
        /// 查询商品or类别
        /// </summary>
        /// <param name="str"></param>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        List<HomeGoodOutput> GetFuzzyGood(string str,int page,int limte);
        /// <summary>
        /// 点击顶商品分类栏
        /// </summary>
        /// <param name="number"></param>
        /// <param name="gsid"></param>
        /// <returns></returns>
        List<DetailsOutput> CagegoryClick(string number, string gsid);
        /// <summary>
        /// 获取闪电抢购列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        List<BuyingOutput> GetBuyingList(int page, int limte);
        /// <summary>
        /// 获取闪电抢购商品列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        BuyingOutput GetBuyingGood(int page, int limte);
        /// <summary>
        /// 首页顶部商品
        /// </summary>
        /// <returns></returns>
        List<HomeTopOutput> HomeTopList();
    }
}
