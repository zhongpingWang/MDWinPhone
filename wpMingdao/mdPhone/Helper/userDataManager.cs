using mdPhone.Model;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Helper
{
    public class UserDataManager
    {
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        public static void SaveUserInformation(UserInfo user)
        {
            var localSettings = IsolatedStorageSettings.ApplicationSettings; 
            localSettings["user"] = user;
            localSettings.Save();
        }

        /// <summary>
        /// 本地读取用户数据
        /// </summary>
        /// <returns></returns>
        public static UserInfo LoadUserSettings()
        {
            var localSettings = IsolatedStorageSettings.ApplicationSettings;
            var result = localSettings.Where(a => a.Key == "user");
            if (result.Any())
            {
                UserInfo user = localSettings["user"] as UserInfo;
                return user;
            }
            return null;
        }

        public static void RemoveUserSettings()
        { 
            IsolatedStorageSettings.ApplicationSettings.Remove("user");
            IsolatedStorageSettings.ApplicationSettings.Save();
        }




    }
}
