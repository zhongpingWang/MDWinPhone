﻿using mdPhone.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.ViewModel
{
   public   class TaskViewModel
    {
        //获取token
        public static void GetMyJoin(string token, HttpWeb.callbackResult callback)
        {
            string uriStr = MDApi.Task_my_joined  + "&format=json";
            HttpWeb.CreateGetHttpResponse(uriStr, null, callback);
        } 
    }
}
