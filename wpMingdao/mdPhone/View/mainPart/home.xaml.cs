using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using mdPhone.Model;
using mdPhone.Helper;
using System.Windows.Media.Imaging;

namespace mdPhone.View.mainPart
{
    public partial class home : UserControl
    {
        public home()
        {
          
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UserInfo user = UserDataManager.LoadUserSettings();
            userName.Text = user.username;
            companyName.Text = user.CompanyName;
            Dispatcher.BeginInvoke(() =>
            {
                uerImg.Source = new BitmapImage(new Uri(user.userimage));
            });
            //  uerImg.Source =new BitmapImage(new Uri(user.avatar)); //new ExtendedImage() { UriSource = new Uri("/Images/loading.gif", UriKind.Relative) }; //user.avatar;
             
        }

        /// <summary>
        /// 动态更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void post_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/post/Post.xaml", UriKind.RelativeOrAbsolute));

        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/apps/taskcenter/main.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/apps/calendar/main.xaml", UriKind.RelativeOrAbsolute));
            
        }

        private void uerImg_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/user/main.xaml", UriKind.RelativeOrAbsolute));
        }
 
      


    }
}
