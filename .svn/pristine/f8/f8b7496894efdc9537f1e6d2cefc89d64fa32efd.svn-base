using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Dream.Shop.Service;
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
    public class AccountApiController : BaseApiController
    {
        private readonly IMemoryCache cache;
        private readonly IOptions<EmailMod> options;
        private readonly IAccountService accountService;
        public AccountApiController(IMemoryCache _cache, IAccountService _accountService,IOptions<EmailMod> options) : base(_cache)
        {
            accountService = _accountService;
            cache = _cache;
            this.options = options;
        }
        
        [HttpPost]
        public object Login([FromForm] string name, [FromForm] string pwd)
        {
            
            UserOutput entity = accountService.Login(name, pwd);
            if (entity != null)
            {
                var token = Guid.NewGuid().ToString("N");
                SetUser(token, entity);
                return ResData.Succ(token);
            }
            else
            {
                return ResData.Fail("账号或密码错误");
            }
        }

        [HttpPost]
        public object Test([FromForm] string token)
        {
            if (LoginUser == null) return ResData.Fail();
            else return ResData.Succ();
        }
       
    }
}
