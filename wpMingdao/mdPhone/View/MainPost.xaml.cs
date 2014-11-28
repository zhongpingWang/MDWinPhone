using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Coding4Fun.Phone.Controls;
using mdPhone.Model;
using mdPhone.Helper;
using System.Windows.Media.Imaging;
using mdPhone.ViewModel;
using mdPhone.Model.passport;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using ImageTools;

namespace mdPhone.View
{
    public partial class MainPost : PhoneApplicationPage
    {
        public MainPost()
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
            //消息提醒
            getPost.Visibility = Visibility.Visible;
            PassPortViewModel.Unreadcount(ResultNewPost);
            base.OnNavigatedTo(e);
        } 

        private void breakPost_mouseup(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var tag = (sender as Image).Tag;
            NavigationService.Navigate(new Uri("/View/post/Post.xaml?type="+tag.ToString(), UriKind.Relative));

        }


        public void ResultNewPost(string resutlt)
        {
            //错误信息 
            if (resutlt.IndexOf("error_code") > -1)
            {
                Dispatcher.BeginInvoke(() =>
               {
                   NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
               });
            }
            else
            {
                Unreadcount unreadCount = new Unreadcount();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(unreadCount.GetType());
                unreadCount = ser.ReadObject(ms) as Unreadcount;
                ms.Close();
                ms.Dispose(); 

                Dispatcher.BeginInvoke(() =>
                {
                    int msgRight = 50;
                    int mRight = 0;
                    if (unreadCount.updated != "0")
                    {
                        //全部消息
                        mRight = msgRight - Convert.ToInt32(unreadCount.updated.Length) * 5;
                        allMsg.Margin = new Thickness(0, 14, mRight, 0);
                        allMsgImg.Source = new BitmapImage(new Uri("/Images/post/allMsgTip.png", UriKind.Relative));
                        allMsg.Text = unreadCount.updated;
                        allMsg.Visibility = Visibility.Visible;


                    }
                    else
                    {
                        allMsgImg.Source = new BitmapImage(new Uri("/Images/post/allMsg.png", UriKind.Relative));
                        allMsg.Visibility = Visibility.Collapsed;
                    }

                    //系统消息
                    if (unreadCount.sysmsg != "0")
                    {
                        //系统消息
                        mRight = msgRight - Convert.ToInt32(unreadCount.sysmsg.Length) * 5;
                        sysMsgImg.Source = new BitmapImage(new Uri("/Images/post/sysMsgTip.png", UriKind.Relative));
                        sysMsg.Margin = new Thickness(0, 14, mRight, 0);
                        sysMsg.Text = unreadCount.sysmsg;
                        sysMsg.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        sysMsgImg.Source = new BitmapImage(new Uri("/Images/post/sysMsg.png", UriKind.Relative));
                        sysMsg.Visibility = Visibility.Collapsed;
                    }

                    //提到我的
                    if (unreadCount.atme != "0")
                    {
                        //系统消息
                        mRight = msgRight - Convert.ToInt32(unreadCount.atme.Length) * 5;
                        atMeImg.Source = new BitmapImage(new Uri("/Images/post/atMeTip.png", UriKind.Relative));
                        atMe.Margin = new Thickness(0, 14, mRight, 0);
                        atMe.Text = unreadCount.atme;
                        atMe.Visibility = Visibility.Visible; 
                    }
                    else
                    {
                        atMeImg.Source = new BitmapImage(new Uri("/Images/post/atMe.png", UriKind.Relative));
                        atMe.Visibility = Visibility.Collapsed;
                    } 


                    //回复我的
                    if (unreadCount.replyme != "0")
                    {
                        //回复我的
                        mRight = msgRight - Convert.ToInt32(unreadCount.replyme.Length) * 5;
                        replyMeImg.Source = new BitmapImage(new Uri("/Images/post/replyMeTip.png", UriKind.Relative));
                        replyMe.Margin = new Thickness(0, 14, mRight, 0);
                        replyMe.Text = unreadCount.replyme;
                        replyMe.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        replyMeImg.Source = new BitmapImage(new Uri("/Images/post/replyMe.png", UriKind.Relative));
                        replyMe.Visibility = Visibility.Collapsed;
                    }

                    getPost.Visibility = Visibility.Collapsed;
                });
            }
        }

        DateTime backTime = DateTime.Now; 

        //在按一次推出
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if ((DateTime.Now - backTime).TotalSeconds >= 2)
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
            toast.VerticalAlignment = VerticalAlignment.Top;
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

        //刷新消息提醒
        private void refreshPassPort_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //消息提醒
            getPost.Visibility = Visibility.Visible;
            PassPortViewModel.Unreadcount(ResultNewPost);
        }

        private void uerImg_MouseLeftButtonUp_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/user/userInfo.xaml", UriKind.Relative));
        } 

        /// <summary>
        ///账号设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginOutSettings_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/user/AccountSetting.xaml", UriKind.Relative));
        }

    }
}