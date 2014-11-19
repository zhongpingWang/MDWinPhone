using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.util
{
    class Base64
    {
        public static String getBase64Encode(String str)
        { 
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes).Replace("=", "");
        }
    }
}
