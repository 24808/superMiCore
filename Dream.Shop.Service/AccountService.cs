using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Models;
using Dream.Shop.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dream.Shop.Service
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly DbContext _dbContext;
        public AccountService(ILogger<AccountService> logger, DbContext dbContext)
        //: base(dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }
        public UserOutput Login(string userId, string pwd)
        {
            _logger.LogWarning("开始登陆！");
            string MD5Pwd = MD5Helper.EncryptString(pwd);
            return (from a in _dbContext.Set<UserInfo>().Where(u => u.UserID == userId && u.PassWord == MD5Pwd && u.IsDelect == false)
                    select new UserOutput
                    {
                        UserId = a.UserID,
                        UserName = a.UserName,
                    }).FirstOrDefault();
        }
    }
}
