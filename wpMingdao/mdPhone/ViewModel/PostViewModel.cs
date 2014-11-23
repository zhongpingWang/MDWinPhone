﻿using System;
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
        //获取token
        public static void GetNewPost(PostEnum postEnum, HttpWeb.callbackResult callback)
        {
            string uriStr = string.Empty;
            if (postEnum==PostEnum.PostAll)
            {
                uriStr = MDApi.PostAll;
            }
            else if (postEnum==PostEnum.Atme2)
            {
                 uriStr = MDApi.Atme2;
            } else if (postEnum==PostEnum.Replybyme)
            {
                uriStr = MDApi.Replybyme;
            }
            HttpWeb.CreateGetHttpResponse(uriStr, null, callback);
        }

        public static void PostUpdate(Dictionary<string,string> postDic,  HttpWeb.callbackResult callback)
        {
            HttpWeb.CreatePostHttpResponse(MDApi.Post_update, postDic, callback); 
        } 
    }
}
