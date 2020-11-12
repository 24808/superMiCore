using Dream.Shop.DataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.IService
{
    public interface IRegisterService
    {
        bool Regis(UserInfo user, string yz);
    }
}
