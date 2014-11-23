using mdPhone.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.ViewModel
{
    public class UserInfoViewModel
    {
        //用户
        public static void UserDetail(HttpWeb.callbackResult callback)
        {
            // 构建URL内容
            string uriStr = MDApi.UserDetail+"&format=json"; 
            //http请求
            HttpWeb.CreatePostHttpResponse(uriStr, null, callback);
        } 
    }
}
