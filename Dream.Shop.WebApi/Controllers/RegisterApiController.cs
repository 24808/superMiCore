using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Dream.Shop.DataEntity;
using Dream.Shop.IService;
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
    public class RegisterApiController : ControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly IOptions<EmailMod> options;
        private readonly IRegisterService service;
        public RegisterApiController(IMemoryCache _cache, IOptions<EmailMod> options, IRegisterService _service)
        {
            cache = _cache;
            this.options = options;
            this.service = _service;
        }


        [HttpGet]
        public object Yz(string email)
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
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string intString = random.Next(1000, 9999).ToString();
            HttpContext.Response.Cookies.Append("yz", intString, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(3)
            });
            // 确定smtp服务器地址 实例化一个Smtp客户端
            SmtpClient client = new SmtpClient();
            client.Host = options.Value.Host;
            // 确定发件地址与收件地址
            MailAddress sendAddress = new MailAddress(options.Value.Address, options.Value.Name, System.Text.Encoding.Default);
            MailAddress receiveAddress = new MailAddress(email);
            // 构造一个Email的Message对象 内容信息
            MailMessage mailMessage = new MailMessage(sendAddress, receiveAddress);
            mailMessage.Subject = "登陆验证码";
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "嗨喽，终于等到您啦！<br/><br/>欢迎修改宁的邮箱密码，您的验证码为：<span style=font-weight:bold;font-size:18px;'>" + intString + "</span><br/>(5分钟内有效)<br/><br/>工作人员不会向您索要密码、验证码等信息。如非本人操作，请联系我们或忽略本条信息。<br/><br/>";
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = MailPriority.Low;//优先级
            // 邮件发送方式  通过网络发送到smtp服务器
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // 如果服务器支持安全连接，则将安全连接设为true
            client.EnableSsl = true;
            try
            {
                // 是否使用默认凭据，若为false，则使用自定义的证书，就是下面的networkCredential实例对象
                client.UseDefaultCredentials = false;
                // 指定邮箱账号和密码,需要注意的是，这个密码是你在QQ邮箱设置里开启服务的时候给你的那个授权码
                NetworkCredential networkCredential = new NetworkCredential(options.Value.Address, options.Value.Password);
                client.Credentials = networkCredential;
                // 发送邮件
                client.Send(mailMessage);

                return ResData.Succ("发送成功");
            }
            catch
            {
                return ResData.Fail("发送失败");
            }
        }

        [HttpGet]
        public ResData Regi([FromForm] UserInfo user, [FromForm] string yz)
        {
            var sds = GetCookies("yz");
            if (yz != sds)
            {
                return ResData.Succ();

            }
            return ResData.Succ();

        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        [HttpGet]
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
