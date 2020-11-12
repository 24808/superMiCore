using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dream.Shop.WebApi.Handler
{
    public class ResData
    {
        public int status { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public object data { get; set; }

        public static ResData Succ(object _data = null, string _msg = "succ", int _count = 0)
        {
            return new ResData()
            {
                count = _count,
                data = _data,
                msg = _msg,
                status = 0
            };
        }
        public static ResData Fail(string _msg = "fail")
        {
            return new ResData()
            {
                count = 0,
                data = null,
                msg = _msg,
                status = 1
            };
        }
       
    }
}
