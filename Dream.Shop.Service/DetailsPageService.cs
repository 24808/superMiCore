
using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dream.Shop.Service
{
    public class DetailsPageService : IDetailsPageService
    {

        private readonly ILogger<HomePageService> _logger;
        private readonly IPersonalService _per;
        private readonly Dream_ShopContext EF;


        public DetailsPageService(ILogger<HomePageService> logger, IPersonalService per, Dream_ShopContext dbContext)
        //: base(dbContext)
        {
            _logger = logger;
            _per = per;
            EF = dbContext;

        }
        /// <summary>
        /// 商品页
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public DetailsOutput GetGood(string goodid, string gsid)
        {


            var data = (from a in EF.Goods
                        join c in EF.GoodCategories on a.CategoryID equals c.CategoryID
                        join d in EF.Good_Specifications on a.GoodId equals d.GoodId
                        where a.GoodId == goodid  && a.IsDelect == true && c.IsDelect == true && d.IsDelect == true
                        select new DetailsOutput
                        {
                            GoodId = a.GoodId,
                            GoodName = a.GoodName,
                            Title = a.Title,
                            Content = a.Content,
                            CategoryId = c.CategoryID,
                            CategoryName = c.CategoryName,
                            ImagesUrl = GetGoodImages(goodid),
                            ChlidClass = GetChild(goodid, ""),
                        }).FirstOrDefault();
            EF.Goods.FirstOrDefault(a => a.GoodId == goodid).VisitCount += 1;
            EF.SaveChanges();
            return data;
        }
        /// <summary>
        /// 获取商品价格
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public decimal GetGoodPrice(string goodid, string gsid)
        {
            if (gsid != null)
            {
                return Convert.ToDecimal(EF.Good_Specifications.FirstOrDefault(a => a.GSId == gsid && a.IsDelect == true).Price);
            }
            return Convert.ToDecimal(EF.Good_Specifications.OrderBy(a=> a.Price).FirstOrDefault(a => a.GoodId == goodid && a.Price != null && a.IsDelect == true).Price);
        }
        /// <summary>
        /// 获取商品轮播图片
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="gsid"></param>
        /// <returns></returns>
        public List<Images> GetGoodImages(string goodid)
        {
            var imglist = new List<Images>();
            var data = EF.Good_Specifications.Where(a => a.GoodId == goodid && a.IsDelect == true).ToList();

            foreach (var item in data)
            {
                var url = EF.Images.FirstOrDefault(a => a.Number == item.GSId && a.ImgUrl != "");
                if (url != null)
                {
                    var mod = new Images()
                    {
                        ImgUrl = "https://localhost:44363/file/image/200/" + url.ImgUrl,
                    };
                    imglist.Add(mod);
                }

            }

            imglist = imglist.Take(5).ToList();
            return imglist;
        }


        /// <summary>
        /// 获取商品规格
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<SpecificationOutput> GetChild(string goodid, string pid)
        {
            var chlidlist = new List<SpecificationOutput>();
            var data = EF.GoodSpecifications.Where(a => a.IsDelect == true).ToList();
            var sss = EF.Good_Specifications.Where(a => a.ParentId == pid).ToList();
            if (sss.Count == 0)
            {
                return new List<SpecificationOutput>();
            }
            foreach (var item in data)
            {
                var SpList = new List<GoodSpNum>();
                var spdata = EF.Good_Specifications.Where(a => a.GoodId == goodid && a.SpecificationId == item.SpecificationId && a.ParentId == pid).ToList();
                if (spdata.Count != 0)
                {
                    foreach (var o in spdata)
                    {
                        var Spmod = new GoodSpNum()
                        {
                            GoodId = o.GoodId,
                            Num = o.SpecificationNum,
                            GSId = o.GSId,
                            ParentId = o.ParentId,
                            Price = Convert.ToDecimal(EF.Good_Specifications.FirstOrDefault(a => a.GSId == o.GSId).Price),
                            Chlid = GetChild(goodid, o.GSId),
                        };
                        SpList.Add(Spmod);
                    }
                }
                var mod = new SpecificationOutput()
                {
                    SpecificationId = item.SpecificationId,
                    SpecificationName = item.SpecificationName,
                    SpecificationNum = SpList,
                };
                if (mod.SpecificationNum.Count != 0)
                {
                    chlidlist.Add(mod);
                }
            }
            return chlidlist;
        }
        /// <summary>
        /// 查看所有商品评论
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<CommentOutput> GetGoodComment(string goodid, int page, int limte, string pid )
        {
            var list = new List<CommentOutput>();
        

            var data = EF.Comments.Where(a => a.Number == goodid && a.ParentId == pid && a.IsDelect == true).OrderBy(a => a.CreateTime).ToList();
            if (data.Count == 0)
            {
                return null;
            }
            foreach (var item in data)
            {
                var mod = new CommentOutput()
                {
                    CommentId = item.CommentId,
                    Content = item.Content,
                    CreateTime = item.CreateTime,
                    Number = item.Number,
                    UserId = item.UserId,
                    UserName = EF.UserInfos.FirstOrDefault(a=> a.UserID == item.UserId).UserName,
                    ParentId = item.ParentId,
                    SupportCount = item.SupportCount,
                    OpposeCount = item.OpposeCount,
                    ChlidCom = GetGoodComment(goodid, page, limte, item.CommentId)
                };
                list.Add(mod);
            }
            return list;
        }
        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodid"></param>
        /// <param name="commentid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public List<CommentOutput> ReplyComment(string userid, string goodid, string commentid, string content)
        {
            var data = EF.Comments.OrderByDescending(a => a.Id).FirstOrDefault().Id;
            var mod = new Comment()
            {
                IsDelect = true,
                CreateTime = DateTime.Now,
                CommentId = "Co1000" + data,
                Number = goodid,
                Content = content,
                OpposeCount = 0,
                SupportCount = 0,
                UserId = userid,
                ParentId = commentid,
            };
            EF.Add(mod);
            if (EF.SaveChanges() == 0)
            {
                return null;
            }
            var data1 = GetGoodComment(goodid, 1, 10, null);

            return data1;
        }
        /// <summary>
        /// 点赞or踩
        /// </summary>
        /// <param name="goodid"></param>
        /// <param name="commentid"></param>
        /// <param name="jundge"></param>
        /// <returns></returns>
        public List<CommentOutput> SupportOrOppose(string goodid, string commentid, bool jundge)
        {
            var data = EF.Comments.FirstOrDefault(a => a.CommentId == commentid && a.IsDelect == true);
            if (jundge == true)
            {
                data.SupportCount += 1;
            }
            else
            {
                data.OpposeCount += 1;
            }
            if (EF.SaveChanges() == 0)
            {
                return null;
            }
            return GetGoodComment(goodid, 1, 10, "");
        }

        /// <summary>
        /// 获取商品概述
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public List<Images> GetOutLine(string goodid)
        {
            var outlineid = EF.Goods.FirstOrDefault(a => a.GoodId == goodid).OutLineId;
            var imglist = new List<Images>();
            var data = EF.Good_Specifications.Where(a => a.GoodId == outlineid && a.IsDelect == true).ToList();

            foreach (var item in data)
            {
                var url = EF.Images.FirstOrDefault(a => a.Number == item.GSId && a.ImgUrl != "");
                if (url != null)
                {
                    var mod = new Images()
                    {
                        ImgUrl = "https://localhost:44363/file/image/200/" + url.ImgUrl,
                    };
                    imglist.Add(mod);
                }

            }

            imglist = imglist.ToList();
            return imglist;
        }
        /// <summary>
        /// 获取商品参数
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public List<Images> GetParameter(string parameterid)
        {
            return EF.Images.OrderBy(a => a.Rank).Where(a => a.Number == parameterid && a.IsDelect == true).ToList();

        }

        /// <summary>
        /// 加入收藏商品
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public List<GoodListOutput> IntoCollect(string userid, string goodid)
        {
            var mod = new GoodCollect()
            {
                IsDelect = true,
                CreateTime = DateTime.Now,
                GoodId = goodid,
                UserId = userid,
            };
            EF.GoodCollects.Add(mod);
            if (EF.SaveChanges() > 0)
            {
                return _per.GetCollect(userid);
            }
            return null;

        }
    }
}
