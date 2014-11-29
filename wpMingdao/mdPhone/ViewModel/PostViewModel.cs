using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdPhone.Helper;
using mdPhone.Model;
using mdPhone.Model.post;

namespace mdPhone.ViewModel
{
   public class PostViewModel
    {
        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="postEnum">类型</param>
        /// <param name="parm">参数</param>
        /// <param name="callback">回调</param>
       public static void GetUlity(PostEnum postEnum, Dictionary<string, string> parm, HttpWeb.callbackResult callback)
        {
            string uriStr = string.Empty;
            if (postEnum==PostEnum.PostAll)
            {
                uriStr = MDApi.PostAll;
            }
            else if (postEnum==PostEnum.Atme2)
            {
                 uriStr = MDApi.Atme2;
            } else if (postEnum==PostEnum.Replyme)
            {
                uriStr = MDApi.Replyme;
            }
            HttpWeb.CreateGetHttpResponse(uriStr, parm, callback);
        }


        


       /// <summary>
       /// 发布
       /// </summary>
       /// <param name="postDic"></param>
       /// <param name="callback"></param>
        public static void PostUpdate(Dictionary<string,string> postDic,  HttpWeb.callbackResult callback)
        {
            HttpWeb.CreatePostHttpResponse(MDApi.Post_update, postDic, callback); 
        }


        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="postDic"></param>
        /// <param name="callback"></param>
        public static void Add_reply(Dictionary<string, string> postDic, HttpWeb.callbackResult callback)
        {
            HttpWeb.CreatePostHttpResponse(MDApi.Add_reply, postDic, callback);
        } 

    }
}
