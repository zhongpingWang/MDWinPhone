using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Helper
{
    public static class MDApi
    {
        public static string app_key = "68B09E2BA977";
        public static string app_secret = "614F752D97F0B667C9B0E52AD06752";
        //登陆
       public static string loginUrl = "https://api.mingdao.com/ajaxpage/authorizeAjax.aspx?type=GetRequestCode";
        
        //获取token
       public static string tokenUrl = "https://api.mingdao.com/auth2/access_token?";

       private static string _newPost = "https://api2.mingdao.com/post/followed";
        /// <summary>
       /// 获取动态更新
        /// </summary>
       public static string NewPost
       {
           get { return AddToken(MDApi._newPost); }
           set { MDApi._newPost = value; }
       }

       private static string _task_my_joined = "https://api2.mingdao.com/task/my_joined";
        /// <summary>
       /// 获取当前登录用户参与的任务列表
        /// </summary>
       public static string Task_my_joined
       {
           get { return AddToken(MDApi._task_my_joined); }
           set { MDApi._task_my_joined = value; }
       }

       private static string _calendar_todo = "https://api2.mingdao.com/calendar/todo";
        /// <summary>
       /// 获取当前登录用户待办日程列表
        /// </summary>
       public static string Calendar_todo
       {
           get { return AddToken(MDApi._calendar_todo); }
           set { MDApi._calendar_todo = value; }
       }

       private static string _calendar_detail = "https://api2.mingdao.com/calendar/detail";
        /// <summary>
       /// 根据日程编号获取单条日程内容
        /// </summary>
       public static string Calendar_detail
       {
           get { return AddToken(MDApi._calendar_detail); }
           set { MDApi._calendar_detail = value; }
       }


       private static string _post_update = "https://api.mingdao.com/post/update";
        /// <summary>
       /// 发表动态
        /// </summary>
       public static string Post_update
       {
           get { return AddToken(MDApi._post_update); }
           set { MDApi._post_update = value; }
       } 
      
        /// <summary>
        /// 加上token
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
       private static string AddToken(string url)
       {
           return url += "?access_token="+App.Token;;
       }


    }
}
