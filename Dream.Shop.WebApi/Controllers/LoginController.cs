using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Dream.Shop.WebApi.Handler;
using Dream.Shop.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Dream.Shop.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogingService logingService;
        private readonly IMemoryCache cache;

        public LoginController(ILogingService logingService, IRegisterService _service, IMemoryCache _cache)
        {
            this.logingService = logingService;
            cache = _cache;
        }
        [HttpPost]
        public ResData Login([FromForm] string userId, [FromForm] string pwd)
        {
            var li = logingService.GetLogin(userId, pwd);
            if (li != null)
            {
                HttpContext.Response.Cookies.Append("userid", userId, options: new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(0.5)
                });
           
            }
            return ResData.Succ(li);
        }
        [HttpPost]
        public ResData OutLogin()
        {
            HttpContext.Response.Cookies.Delete("userid");
            return ResData.Succ(null);
        }
    }
}
