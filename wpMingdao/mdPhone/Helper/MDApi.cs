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

      //获取动态更新
       public static string newPost = "https://api2.mingdao.com/post/followed".QueryToken();

       //获取当前登录用户参与的任务列表
       public static string task_my_joined = "https://api2.mingdao.com/task/my_joined".QueryToken();

       //获取当前登录用户待办日程列表
       public static string calendar_todo = "https://api2.mingdao.com/calendar/todo".QueryToken();

        //根据日程编号获取单条日程内容
       public static string calendar_detail = "https://api2.mingdao.com/calendar/detail".QueryToken();

        //发表动态
       public static string post_update = "https://api.mingdao.com/post/update".QueryToken();

    }
}
