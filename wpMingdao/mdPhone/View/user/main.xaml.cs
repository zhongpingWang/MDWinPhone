using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using mdPhone.Helper;
using mdPhone.Model;

namespace mdPhone.View.user
{
    public partial class main : PhoneApplicationPage
    {
        public main()
        {
            InitializeComponent();
        }

        //退出登录
        private void exitLogin_Click(object sender, RoutedEventArgs e)
        {
            UserInfo user = UserDataManager.LoadUserSettings();
            user.Token = "";
            UserDataManager.RemoveUserSettings();
            Application.Current.Terminate();
        }

        //切换账号
        private void cjangeLogin_Click(object sender, RoutedEventArgs e)
        {
            UserInfo user = UserDataManager.LoadUserSettings();
            user.RememberPwd = false;
            UserDataManager.SaveUserInformation(user);
             NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative)); 
        }
    }
}