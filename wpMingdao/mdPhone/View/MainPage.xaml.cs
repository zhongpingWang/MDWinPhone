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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Coding4Fun.Phone.Controls;


namespace mdPhone.View
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        //初始化
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UserInfo user = UserDataManager.LoadUserSettings();
            userName.Text = user.username;
            companyName.Text = user.CompanyName;
            Dispatcher.BeginInvoke(() =>
            {
                uerImg.Source = new BitmapImage(new Uri(user.userimage));
            }); 
            //  uerImg.Source =new BitmapImage(new Uri(user.avatar)); //new ExtendedImage() { UriSource = new Uri("/Images/loading.gif", UriKind.Relative) }; //user.avatar;
            base.OnNavigatedTo(e);
        }

        DateTime backTime=DateTime.Now;

        //在按一次推出
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if ((DateTime.Now-backTime).TotalSeconds>=2)
            {
                
                e.Cancel = true;

                Coding4FunForMsg("在按一次退出", "", 1000);  

                //MessageBox.Show("在按一次退出");
                backTime = DateTime.Now;
            }
            else
            {
                Application.Current.Terminate();
            }
           
            
            base.OnBackKeyPress(e);
        }


        /// <summary>  
        /// 信息提示  
        /// </summary>  
        /// <param name="content">提示的信息内容</param>  
        /// <param name="title">提示的标题</param>  
        /// <param name="timeout">提示消息的显示过期时间。单位毫秒</param>  
        public void Coding4FunForMsg(string content, string title, int timeout)
        {
            SolidColorBrush White = new SolidColorBrush(Colors.White);
            SolidColorBrush Red = new SolidColorBrush(Colors.Red);
            ToastPrompt toast = new ToastPrompt
            {
                Background = Red,
                IsTimerEnabled = true,
                IsAppBarVisible = true,
                MillisecondsUntilHidden = timeout,
                Foreground = White,
            };
           // toast.Title = title;
            toast.Message = content;
            toast.VerticalAlignment = VerticalAlignment.Bottom;
            //toast.Margin = new Thickness(0, 0, 0, 0);
            toast.TextOrientation = System.Windows.Controls.Orientation.Horizontal;
            toast.Show();
        }  



        private void Exit()
        {
            while (NavigationService.BackStack.Any())
                NavigationService.RemoveBackEntry();
            this.IsHitTestVisible = this.IsEnabled = false;
            if (this.ApplicationBar != null)
            {
                foreach (var item in this.ApplicationBar.Buttons.OfType<ApplicationBarIconButton>())
                {
                    item.IsEnabled = false;
                }
            }
        }

        //动态更新
        private void post_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/post/Post.xaml", UriKind.Relative));
        }
        //个人中心
        private void uerImg_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/user/main.xaml", UriKind.Relative));
           //(Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/user/main.xaml", UriKind.RelativeOrAbsolute));
        }

       
 

    }
    

}