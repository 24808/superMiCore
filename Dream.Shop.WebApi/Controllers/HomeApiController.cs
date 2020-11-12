using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dream.Shop.IService;
using Dream.Shop.WebApi.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dream.Shop.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly IHomePageService home;
        public HomeApiController(IHomePageService _home) : base()
        {
            this.home = _home;
        }
        [HttpGet]
        public ResData CagegoryClicks(string number, string gsid)
        {
            var list = home.CagegoryClick(number, gsid);
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData CagegoryHome()
        {

            var list = home.GetCagegoryHomes();
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData GetHomeGood(int page = 1, int limte = 8)
        {

            var list = home.GetHomeGood(page, limte);
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData FuzzyGood(string str, int page = 1, int limte = 10)
        {

            var list = home.GetFuzzyGood(str, page, limte);
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData HomeTopList()
        {
            var list = home.HomeTopList();
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData GetBuyingLists(int page=1, int limte=8)
        {
            var list = home.GetBuyingList(page, limte);
            return ResData.Succ(list);
        }
            [HttpGet]
        public ResData GetBuyingGoodList(int page=1, int limte=8)
        {
            var list = home.GetBuyingGood(page,limte);
            return ResData.Succ(list);
        }

    }
}
 