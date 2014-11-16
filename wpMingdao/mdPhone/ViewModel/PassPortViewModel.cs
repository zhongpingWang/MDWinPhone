using mdPhone.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.ViewModel
{
   public class PassPortViewModel
    {

        //获取token
       public static void Unreadcount(HttpWeb.callbackResult callback)
        {
            string uriStr = MDApi.Unreadcount + "&format=json";
            HttpWeb.CreateGetHttpResponse(uriStr, null, callback);
        }

    }
}
