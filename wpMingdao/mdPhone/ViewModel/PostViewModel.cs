using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdPhone.Helper;
using mdPhone.Model;

namespace mdPhone.ViewModel
{
   public class PostViewModel
    {
        //获取token
        public static void GetNewPost(HttpWeb.callbackResult callback)
        {
            string uriStr = MDApi.NewPost + "&format=json";  
            HttpWeb.CreateGetHttpResponse(uriStr, null, callback);
        }

        public static void PostUpdate(Dictionary<string,string> postDic,  HttpWeb.callbackResult callback)
        {
            HttpWeb.CreatePostHttpResponse(MDApi.Post_update, postDic, callback); 
        } 
    }
}
