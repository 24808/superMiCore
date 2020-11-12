using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dream.Shop.IService;
using Dream.Shop.WebApi.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dream.Shop.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailsPageService  details;
        public DetailsController(IDetailsPageService _details) : base()
        {
            this.details = _details;
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
        public ResData GetGood(string goodid,string gsid)
        {
            var list = details.GetGood(goodid, gsid);
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData GetGoodComment(string goodid)
        {
            var list = details.GetGoodComment(goodid, 1, 10, null);
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData GetOutLine(string outlineid)
        {
            var list = details.GetOutLine(outlineid);
            return ResData.Succ(list);
        }
        [HttpGet]
        public ResData GetParameter(string parameterid)
        {
            var list = details.GetParameter(parameterid);
            return ResData.Succ(list);
        }
        [HttpPost]
        public ResData ReplyComments([FromForm] string goodid, [FromForm]string commentid, [FromForm]string content)
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
            var list = details.ReplyComment(userid, goodid, commentid, content);
            return ResData.Succ(list);
        }
        /// <summary>
        /// 商品概述
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        [HttpGet]
        public ResData GetOutLineList(string goodid)
        {
            var list = details.GetOutLine(goodid);
            return ResData.Succ(list);
        }
    }
}
