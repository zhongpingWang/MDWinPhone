using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.JPush
{
    [DataContract]
    public class UserSetting
    {
        public static readonly string isoKey = "push_md_wp_inbox";

        private static UserSetting _userSetting;
        public UserSetting()
        {
            isOpenNotification = false;
            isFirstLoad = true;
        }
        [DataMember]
        public bool isOpenNotification { get; set; }
        [DataMember]
        public bool isFirstLoad { get; set; }

        public string registrationID { get; set; }

        public static UserSetting shareUserDefualt()
        {
            lock (isoKey)
            {
                if (_userSetting == null)
                {
                    try
                    {
                        var setting = IsolatedStorageSettings.ApplicationSettings;
                        if (setting != null)
                        {
                            if (setting.Contains(isoKey))
                            {
                                _userSetting = (UserSetting)setting[isoKey];
                                return _userSetting;
                            }
                        }
                        _userSetting = new UserSetting();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        _userSetting = new UserSetting();
                    }
                }
                return _userSetting;
            }
        }
        public void Synchronized()
        {
            try
            {
                lock (isoKey)
                {
                    var isosettings = IsolatedStorageSettings.ApplicationSettings;
                    if (isosettings != null)
                    {
                        if (isosettings.Contains(isoKey))
                        {
                            isosettings[isoKey] = this;
                        }
                        else
                        {
                            isosettings.Add(isoKey, this);
                        }
                    }
                    isosettings.Save();
                }
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
        }
    }
}
