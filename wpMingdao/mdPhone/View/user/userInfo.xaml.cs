using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using mdPhone.JPush;
using mdPhone.ViewModel;
using mdPhone.Model.user;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Windows.Media.Imaging;

namespace mdPhone.View.user
{
    public partial class userInfo : PhoneApplicationPage
    {
        public userInfo()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            UserInfoViewModel.UserDetail(getUserDetail);

            UserSetting _setting = UserSetting.shareUserDefualt();
            if (_setting.isOpenNotification)
            {
                togglePush.IsChecked = true;
                togglePush.Content = "On";
                // NotificationBox.Content = "开启";
            }
            else
            {
                togglePush.IsChecked = true;
                togglePush.Content = "Off";

            }
           
        }

        private void getUserDetail(string userDetailJson) 
        {
            userDetail userDetail = new userDetail();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(userDetailJson));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(userDetail.GetType());
            userDetail = ser.ReadObject(ms) as userDetail;
         
            ms.Close();
            ms.Dispose();
          
            Dispatcher.BeginInvoke(() =>
            {
                uerImg.Source = new BitmapImage(new Uri(userDetail.user.avatar));
                userCompanyName.Text = userDetail.user.company;
                userName.Text = userDetail.user.name;
                userJob.Text =userDetail.user.department+" - "+userDetail.user.job;
                userEmail.Text = userDetail.user.email;
                userGrade.Text = userDetail.user.grade;
                userMark.Text = userDetail.user.mark;
                uerAddress.Text = userDetail.user.city;
                userJoin.Text = userDetail.user.create_time;
                userPhone.Text = userDetail.user.mobile_phone;
                userBox.Visibility = Visibility.Visible;
                loading.Visibility = Visibility.Collapsed;
            });
           
        }




        private void togglePush_Click(object sender, RoutedEventArgs e)
        {


            if (togglePush.IsChecked == true)
            {
                togglePush.Content = "On";
                JPushSDK.JServer.RegisterNotification();
                UserSetting.shareUserDefualt().isOpenNotification = true;

            }
            else
            {
                JPushSDK.JServer.CloseNotification();
                togglePush.Content = "Off";
                UserSetting.shareUserDefualt().isOpenNotification = true;

            }
        }
 
    }
}