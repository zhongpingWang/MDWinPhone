using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace mdPhone.Helper
{
   public class DataManager
    {

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        public static void SaveUserInformation(string post,string key)
        {
            var postSettings = IsolatedStorageSettings.ApplicationSettings;
            postSettings[key] = post;
        }

        /// <summary>
        /// 本地读取用户数据
        /// </summary>
        /// <returns></returns>
        public static string LoadPostSettings(string key)
        {
            var localSettings = IsolatedStorageSettings.ApplicationSettings;
            var result = localSettings.Where(a => a.Key == key);
            if (result.Any())
            {
                string post = localSettings[key].ToString();
                return post;
            }
            return null;
        } 


    }
}
