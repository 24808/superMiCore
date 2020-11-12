using Dream.Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dream.Shop.DataEntity;

namespace Dream.Shop.IService
{
    public interface IDetailsPageService
    {
        /// <summary>
        /// 展示商品
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        DetailsOutput GetGood(string goodid,string gsid);

        /// <summary>
        /// 获取商品评论
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        List<CommentOutput> GetGoodComment(string goodid, int page, int limte, string pid = "");
        /// <summary>
        /// 获取商品概述
        /// </summary>
        /// <param name="outlineid"></param>
        /// <returns></returns>
        List<Images> GetOutLine(string goodid);
        /// <summary>
        /// 获取商品参数
        /// </summary>
        /// <param name="parameterid"></param>
        /// <returns></returns>

        List<Images> GetParameter(string parameterid);

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodid"></param>
        /// <param name="commentid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        List<CommentOutput> ReplyComment(string userid, string goodid, string commentid, string content);
        /// <summary>
        /// 商品评论点赞or踩
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodid"></param>
        /// <param name="commentid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        List<CommentOutput> SupportOrOppose(string goodid, string commentid, bool jundge);
        /// <summary>
        /// 加入收藏商品
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodid"></param>
        /// <returns></returns>
        List<GoodListOutput> IntoCollect(string userid,string goodid);




    }
}
