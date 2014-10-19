using mdPhone.Helper;
using mdPhone.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.ViewModel
{
    public class LoginViewModel
    {
          
        //用户登录
        public static void UserLogin(string userEmail, string pwd,string projectId, HttpWeb.callbackResult callback)
        {
            // 构建URL内容
            string uriStr = MDApi.loginUrl;//"https://api.mingdao.com/ajaxpage/authorizeAjax.aspx?type=GetRequestCode";//

            Dictionary<string, string> dic = new Dictionary<string, string>() { 
               {"UserEmail",userEmail},
               {"UserPsw",pwd},
               {"app_key",MDApi.app_key},//BE5472E1492BEFD650D6B4C71F101DCD
               {"redirect_uri","http://www.baidu.com"},//E771069CB1159D197C9733ABF7D5F75
               {"ProjectID",projectId},//fe288386-3d26-4eab-b5d2-51eeab82a7f9
               {"appID",""},
               {"chkCode",""},
               {"state",""} 
            };
            //http请求
            HttpWeb.CreatePostHttpResponse(uriStr, dic, callback);
        }

        //获取token
        public static void GetUserToken(string code, HttpWeb.callbackResult callback)
        {
            string uriStr = MDApi.tokenUrl;// "https://api.mingdao.com/auth2/access_token?";

            Dictionary<string, string> dic = new Dictionary<string, string>() { 
                   {"app_Key",MDApi.app_key},
                   {"app_secret",MDApi.app_secret},
                   {"grant_type","authorization_code"},
                   {"code",code},
                   {"redirect_uri","http://localhost:12136/authorize_callback"},
                   {"format","json"}
                };

            HttpWeb.CreateGetHttpResponse(uriStr, dic, callback); 
        } 
    }
}
