using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Dream.Shop.Utility;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dream.Shop.Service
{
    public class HomePageService: IHomePageService
    {
        private readonly ILogger<HomePageService> _logger;
        private readonly Dream_ShopContext EF;
        public HomePageService(ILogger<HomePageService> logger, Dream_ShopContext dbContext)
        //: base(dbContext)
        {
            _logger = logger;
            EF = dbContext;

        }
        public List<HomeTopOutput> HomeTopList()
        {
            var list = new List<HomeTopOutput>();
            var data1 = EF.Goods.Where(a => a.GoodName.Contains("小米") && a.CategoryID == "C1001").ToList();
            var goodlist1 = new List<HomeGoodOutput>();
            foreach (var item in data1)
            {
                var mod = new HomeGoodOutput()
                {
                    GoodId = item.GoodId,
                    GoodName = item.GoodName,
                    ImgUrl = "https://localhost:44363/file/image/200/" + item.ThumbnailImg,
                    CreateTime = item.CreateTime,
                    FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderByDescending(a => a.Price).FirstOrDefault(a => a.GoodId == item.GoodId).Price),
                };
                goodlist1.Add(mod);
            }
            var mod1 = new HomeTopOutput()
            {
                Name = "小米手机",
                GetGoodList = goodlist1.OrderBy(a => a.CreateTime).ToList(),
            };
            list.Add(mod1);

            var data2 = EF.Goods.Where(a => a.GoodName.Contains("Redmi") && a.CategoryID == "C1001").ToList();
            var goodlist2 = new List<HomeGoodOutput>();
            foreach (var item in data2)
            {
                var mod = new HomeGoodOutput()
                {
                    GoodId = item.GoodId,
                    GoodName = item.GoodName,
                    ImgUrl = "https://localhost:44363/file/image/200/" + item.ThumbnailImg,
                    CreateTime = item.CreateTime,
                    FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderByDescending(a => a.Price).FirstOrDefault(a => a.GoodId == item.GoodId).Price),
                };
                goodlist2.Add(mod);
            }
            var mod2 = new HomeTopOutput()
            {
                Name = "红米手机",
                GetGoodList = goodlist2.OrderBy(a => a.CreateTime).ToList(),
            };
            list.Add(mod2);

            var data3 = EF.Goods.Where(a => a.GoodName.Contains("电视") && a.CategoryID == "C1002").ToList();
            var goodlist3 = new List<HomeGoodOutput>();
            foreach (var item in data1)
            {
                var mod = new HomeGoodOutput()
                {
                    GoodId = item.GoodId,
                    GoodName = item.GoodName,
                    ImgUrl = "https://localhost:44363/file/image/200/" + item.ThumbnailImg,
                    CreateTime = item.CreateTime,
                    FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderByDescending(a => a.Price).FirstOrDefault(a => a.GoodId == item.GoodId).Price),
                };
                goodlist3.Add(mod);
            }
            var mod3 = new HomeTopOutput()
            {
                Name = "电视",
                GetGoodList = goodlist3.OrderBy(a => a.CreateTime).ToList(),
            };
            list.Add(mod3);

            return list;
        }
        /// <summary>
        /// 点击顶部分类栏
        /// </summary>
        /// <param name="number"></param>
        /// <param name="gsid"></param>
        /// <returns></returns>
        public List<DetailsOutput> CagegoryClick(string number, string gsid)
        {
            var catePid = EF.GoodCategories.FirstOrDefault(a => a.CategoryID == number && a.IsDelect == true).ParentId;
            var list = EF.Goods.Where(a => a.CategoryID == number && a.IsDelect == true).ToList();
            var goodlist = new List<DetailsOutput>();
            if (catePid == "")
            {
                goodlist = (from a in EF.Goods
                            join c in EF.GoodCategories on a.CategoryID equals c.CategoryID
                            join d in EF.Good_Specifications on a.CategoryID equals d.GoodId
                            where a.GoodId == number && d.ParentId == "" && a.IsDelect == true && c.IsDelect == true && d.IsDelect == true
                            select new DetailsOutput
                            {
                                GoodId = a.GoodId,
                                GoodName = a.GoodName,
                                Title = a.Title,
                                Content = a.Content,
                                CategoryId = a.CategoryID,
                                CategoryName = c.CategoryName,
                                ImagesUrl = GetGoodImages(a.GoodId),
                                ChlidClass = GetChild(number, ""),
                            }).ToList();
                foreach(var item in EF.GoodCategories.Where(a=> a.ParentId == number))
                {
                    var categoodlist = EF.Goods.Where(a => a.CategoryID == item.CategoryID).ToList();
                    list.AddRange(categoodlist);
                }
            }
            else if(list.Count == 1)
            {
                goodlist = (from a in EF.Goods
                            join c in EF.GoodCategories on a.CategoryID equals c.CategoryID
                            join d in EF.Good_Specifications on a.CategoryID equals d.GoodId
                            where a.CategoryID == number && d.ParentId == "" && a.IsDelect == true && c.IsDelect == true && d.IsDelect == true
                            select new DetailsOutput
                            {
                                GoodId = a.GoodId,
                                GoodName = a.GoodName,
                                Title = a.Title,
                                Content = a.Content,
                                CategoryId = c.CategoryID,
                                CategoryName = c.CategoryName,
                                ImagesUrl = GetGoodImages(a.GoodId),
                                ChlidClass = GetChild(number, ""),
                            }).ToList();
                EF.Goods.FirstOrDefault(a => a.GoodId == goodlist.FirstOrDefault().GoodId).VisitCount += 1;
                EF.SaveChanges();
            }
            else if(list.Count == 0)
            {
                var buying = EF.GoodPromotions.Where(a => a.PromotionId == number).ToList();
                if (buying.Count == 0)
                {
                    goodlist = (from a in EF.Goods
                                join c in EF.GoodCategories on a.CategoryID equals c.CategoryID
                                join d in EF.Good_Specifications on a.CategoryID equals d.GoodId
                                where a.GoodId == number && d.ParentId == "" && a.IsDelect == true && c.IsDelect == true
                                select new DetailsOutput
                                {
                                    GoodId = a.GoodId,
                                    GoodName = a.GoodName,
                                    Title = a.Title,
                                    Content = a.Content,
                                    CategoryId = c.CategoryID,
                                    CategoryName = c.CategoryName,
                                    ImagesUrl = GetGoodImages(a.GoodId),
                                    ChlidClass = GetChild(number, ""),
                                    
                                }).ToList();
                    EF.Goods.FirstOrDefault(a => a.GoodId == goodlist.FirstOrDefault().GoodId).VisitCount += 1;
                    EF.SaveChanges();
                }
                else
                {
                    goodlist = (from a in EF.Goods
                                join b in EF.Good_Promotions on a.GoodId equals b.GoodId
                                join c in EF.GoodPromotions on b.PromotionId equals c.PromotionId
                                join d in EF.Good_Specifications on a.CategoryID equals d.GoodId
                                where a.GoodId == number && d.ParentId == "" && a.IsDelect == true && c.IsDelect == true && d.IsDelect == true
                                select new DetailsOutput
                                {
                                    GoodId = a.GoodId,
                                    GoodName = a.GoodName,
                                    Title = a.Title,
                                    Content = a.Content,
                                    CategoryId = a.CategoryID,
                                    CategoryName = EF.GoodCategories.FirstOrDefault(x=> x.CategoryID == x.CategoryID).CategoryName,
                                    ImagesUrl = GetGoodImages(a.GoodId),
                                    ChlidClass = GetChild(number, ""),
                                    
                                }).ToList();
                }
            }

            return goodlist;


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
        /// 获取商品价格
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public decimal GetGoodPrice(string goodid, string gsid)
        {
            if (gsid != "")
            {
                return Convert.ToDecimal(EF.Good_Specifications.OrderBy(a=> a.Price).FirstOrDefault(a => a.GSId == gsid && a.IsDelect == true).Price);
            }
            var price = (from a in EF.Good_Specifications
                         join c in EF.GoodSpecifications on a.SpecificationId equals c.SpecificationId
                         where a.GoodId == goodid && a.IsDelect == true && c.IsDelect == true
                         select new Good_Specification
                         {
                             Price = EF.Good_Specifications.FirstOrDefault(x => x.GSId == gsid && x.IsDelect == true).Price
                         }).FirstOrDefault().Price;
            return Convert.ToDecimal(price);
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
        /// 获取闪电抢购列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        public List<BuyingOutput> GetBuyingList(int page, int limte)
        {
            var buylist = new List<BuyingOutput>();

            var data = EF.GoodPromotions.OrderBy(a => a.StartTiem).Where(a => a.PromotionName.Contains("闪电抢购") && a.EndTime > DateTime.Now && a.IsDelect == true).Take(4).ToList();
            foreach (var i in data)
            {
                var buymod = new BuyingOutput();
                var list = new List<HomeGoodOutput>();


                var promotionlist = EF.Good_Promotions.Where(a => a.PromotionId == i.PromotionId).ToList();
                foreach (var item in promotionlist)
                {
                    var mod = new HomeGoodOutput()
                    {
                        GoodId = item.GoodId,
                        GoodName = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).GoodName,
                        HardId = item.PromotionId,
                        HardName = EF.GoodPromotions.FirstOrDefault(a => a.PromotionId == item.PromotionId).PromotionName,
                        ImgUrl = "https://localhost:44363/file/image/200/" + EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).ThumbnailImg,
                        CreateTime = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).CreateTime,
                        FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderByDescending(a => a.Price).FirstOrDefault(a => a.GoodId == item.GoodId && a.Price != 0 && a.IsDelect == true).Price),
                        Discounts = item.Discounts,
                    };
                    list.Add(mod);

                }
                buymod.ChlidGood = list.Skip((page - 1) * limte).Take(limte).ToList();
                buymod.EndTime = EF.GoodPromotions.FirstOrDefault(a => a.PromotionId == i.PromotionId).EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                buymod.StartTiem = EF.GoodPromotions.FirstOrDefault(a => a.PromotionId == i.PromotionId).StartTiem.ToString("yyyy-MM-dd HH:mm:ss"); 
                buylist.Add(buymod);
            }


            return buylist;
        }

        /// <summary>
        /// 获取闪电抢购商品列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        public BuyingOutput GetBuyingGood(int page, int limte)
        {
            var buymod = new BuyingOutput();
            var list = new List<HomeGoodOutput>();

            var data = EF.GoodPromotions.OrderBy(a => a.StartTiem).FirstOrDefault(a => a.PromotionName.Contains("闪电抢购") && a.EndTime > DateTime.Now && a.IsDelect == true);

            var promotionlist = EF.Good_Promotions.Where(a => a.PromotionId == data.PromotionId).ToList();
            foreach (var item in promotionlist)
            {
                var mod = new HomeGoodOutput()
                {
                    GoodId = item.GoodId,
                    GoodName = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).GoodName,
                    HardId = item.PromotionId,
                    HardName = EF.GoodPromotions.FirstOrDefault(a => a.PromotionId == item.PromotionId).PromotionName,
                    ImgUrl = "https://localhost:44363/file/image/200/" + EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).ThumbnailImg,
                    CreateTime = EF.Goods.FirstOrDefault(a => a.GoodId == item.GoodId).CreateTime,
                    FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderByDescending(a => a.Price).FirstOrDefault(a => a.GoodId == item.GoodId && a.Price != 0 && a.IsDelect == true).Price),
                    Discounts = item.Discounts,
                };
                list.Add(mod);
            }
            buymod.ChlidGood = list.Skip((page - 1) * limte).Take(limte).ToList();
            buymod.EndTime = EF.GoodPromotions.FirstOrDefault(a => a.PromotionId == data.PromotionId).EndTime.ToString("yyyy-MM-dd HH:mm:ss");
            buymod.StartTiem = EF.GoodPromotions.FirstOrDefault(a => a.PromotionId == data.PromotionId).StartTiem.ToString("yyyy-MM-dd HH:mm:ss");

            return buymod;
        }
        /// <summary>
        /// 查询首页分类商品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>
        public List<HomeCateOutput> GetHomeGood(int page, int limte)
        {
            var homecatelist = new List<HomeCateOutput>();
            var data = EF.GoodCategories.Where(a => a.IsDelect == true && a.ParentId == "").ToList();
            foreach (var item in data)
            {
                var list = new List<HomeGoodOutput>();
                var homecatemod = new HomeCateOutput();
                var goodlist = EF.Goods.Where(a => a.CategoryID == item.CategoryID).Take(limte).ToList();
                foreach (var i in goodlist)
                {
                    var mod = new HomeGoodOutput()
                    {
                        HardId = i.CategoryID,
                        HardName = EF.GoodCategories.FirstOrDefault(a => a.CategoryID == i.CategoryID).CategoryName,
                        GoodId = i.GoodId,
                        GoodName = i.GoodName,
                        ImgUrl = "https://localhost:44363/file/image/200/" + i.ThumbnailImg,
                        CreateTime = i.CreateTime,
                        FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderBy(a => a.Price).FirstOrDefault(a => a.GoodId == i.GoodId && a.Price != null && a.IsDelect == true).Price),
                    };
                    list.Add(mod);
                }
                goodlist = EF.Goods.Where(a => a.CategoryID == EF.GoodCategories.FirstOrDefault(b => b.ParentId == item.CategoryID).CategoryID).Take(8 - goodlist.Count).ToList();

                foreach (var i in goodlist)
                {
                    var mod = new HomeGoodOutput()
                    {
                        HardId = i.CategoryID,
                        HardName = EF.GoodCategories.FirstOrDefault(a => a.CategoryID == i.CategoryID).CategoryName,
                        GoodId = i.GoodId,
                        GoodName = i.GoodName,
                        ImgUrl = "https://localhost:44363/file/image/200/" + i.ThumbnailImg,
                        CreateTime = i.CreateTime,
                        FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderBy(a => a.Price).FirstOrDefault(a => a.GoodId == i.GoodId && a.Price != null && a.IsDelect == true).Price),
                    };
                    list.Add(mod);
                }
                homecatemod.GetHomeGood = list;
                homecatelist.Add(homecatemod);
            }

            return homecatelist;
        }
        /// <summary>
        /// 查询首页顶部商品分类
        /// </summary>
        /// <returns></returns>
        public List<Object> GetCagegoryHomes()
        {
            var data = EF.GoodCategories.Where(a=> a.IsDelect == true).ToList();
            var img = EF.Images.Where(a=> a.IsDelect == true).ToList();
            var list = new List<Object>();
            foreach (var item in data.Where(a => a.ParentId == ""))
            {
                var url = img.FirstOrDefault(a => a.Number == item.CategoryID);
                list.Add(new
                {
                    Id = item.Id,
                    Num = item.CategoryID,
                    Name = item.CategoryName,
                    ImgUrl = url != null ? "https://localhost:44363/file/image/200/" + url.ImgUrl : null,
                    Child = GetCagegoryChild(data, item.CategoryID)
                });
            }
            return list;

        }
        /// <summary>
        /// 获取主分类子类
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public List<CateOutput> GetCagegoryChild(List<GoodCategory> mod, string number)
        {
            var catedata = mod.Where(a => a.ParentId == number).Take(24).ToList();
            var img = EF.Images.Where(a => a.IsDelect == true).ToList();
            var catelist = new List<CateOutput>();
            var list = new List<HomeOutput>();
            foreach (var item in catedata)
            {
                var url = img.FirstOrDefault(a => a.Number == item.CategoryID);
                var catemod = new HomeOutput()
                {
                    Id = item.Id,
                    Num = item.CategoryID,
                    Name = item.CategoryName,
                    ImgUrl = url != null ? "https://localhost:44363/file/image/200/" + url.ImgUrl : null,
                };
                list.Add(catemod);
                list.Take(45).ToList();
            }
            if (list.Count != 24)
            {
                var gooddata = EF.Goods.Where(a => a.CategoryID == number && a.IsDelect == true).Take(24 - catedata.Count).ToList();
                foreach (var o in gooddata)
                {
                    var goodmod = new HomeOutput()
                    {
                        Id = o.Id,
                        Num = o.GoodId,
                        Name = o.GoodName,
                        ImgUrl = "https://localhost:44363/file/image/200/" + o.ThumbnailImg,
                    };
                    list.Add(goodmod);
                }

            }
            foreach (var item in catedata)
            {
                var url = img.FirstOrDefault(a => a.Number == item.CategoryID);
                var catemod = new HomeOutput()
                {
                    Id = item.Id,
                    Num = item.CategoryID,
                    Name = item.CategoryName,
                    ImgUrl = url != null ? "https://localhost:44363/file/image/200/" + url.ImgUrl : null,
                };
                list.Add(catemod);
            }
            var cou = list.Count;
            if (cou % 4 != 0)
            {
                cou = (cou / 4) + 1;
            }
            for (var q = 1; q <= cou; q++)
            {
                var clist = new List<HomeOutput>();
                clist.AddRange(list.Skip((q - 1) * 4).Take(4).ToList());
                var dlist = new CateOutput()
                {
                    GoodList = clist,
                };
                catelist.Add(dlist);

            }


            return catelist;
        }






        /// 查询商品or类别
        /// </summary>
        /// <param name="str"></param>
        /// <param name="page"></param>
        /// <param name="limte"></param>
        /// <returns></returns>

        public List<HomeGoodOutput> GetFuzzyGood(string str, int page, int limte)
        {
            var list = new List<HomeGoodOutput>();
            var data = EF.Goods.ToList();
            if (str == "")
            {
                foreach (var item in data)
                {
                    var mod = new HomeGoodOutput()
                    {
                        CreateTime = item.CreateTime,
                        GoodId = item.GoodId,
                        GoodName = item.GoodName,
                        FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderByDescending(a => a.Price).FirstOrDefault(a => a.GoodId == a.GoodId && a.Price != 0 && a.IsDelect == true).Price),
                        ImgUrl = "https://localhost:44363/file/image/200/" + item.ThumbnailImg,
                    };
                    list.Add(mod);
                }
                return list;
            };
            list = (from x in data
                    join b in EF.GoodCategories on x.CategoryID equals b.CategoryID
                    where b.CategoryName.Contains(str) || x.GoodName.Contains(str) && x.IsDelect == true && b.IsDelect == true
                    select new HomeGoodOutput
                    {

                        CreateTime = x.CreateTime,
                        GoodId = x.GoodId,
                        GoodName = x.GoodName,
                        FloorPrice = Convert.ToDecimal(EF.Good_Specifications.OrderByDescending(a => a.Price).FirstOrDefault(a => a.GoodId == x.GoodId && a.Price != 0 && a.IsDelect == true).Price),
                        ImgUrl = "https://localhost:44363/file/image/200/" + x.ThumbnailImg,
                    }).ToList();



            return list.Skip(limte * (page - 1)).Take(limte).ToList();
        }
    }
}
