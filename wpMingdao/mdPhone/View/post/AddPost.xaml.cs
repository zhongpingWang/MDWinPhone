using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using mdPhone.ViewModel;
using System.Windows.Input;

namespace mdPhone.View.post
{
    public partial class AddPost : PhoneApplicationPage
    {
        public AddPost()
        {
            InitializeComponent();
        }
        
        //发表动态
        private void add_post_Click(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                fristLoad.Visibility = Visibility.Visible;
            });
             
            string s_type = "3";
            if (allUser.IsChecked == true)
            {
                s_type = "0";
            }
            Dictionary<string, string> postDic = new Dictionary<string, string>() {

               {"g_id",string.Empty},
               {"p_msg",postText.Text.Trim()},
               {"p_type","0"},
               {"s_type",s_type},
               {"format","json"}
            
            };
           
            PostViewModel.PostUpdate(postDic, ResultNewPost); 


        }

    

        //回调
        public void ResultNewPost(string resutlt)
        {
            //错误信息 
            if (resutlt.IndexOf("error_code") > -1)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("登录失效");
                    NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
                }); 
                
            }
            else
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("发表成功");
                    NavigationService.Navigate(new Uri("/View/post/Post.xaml?type=PostAll", UriKind.Relative));
                }); 
               
            }
        }

        /// <summary>
        /// 文本默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (postText.Text=="请输入动态内容…")
            {
                postText.Text = "";
            }
        }
        /// <summary>
        /// 文本默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void postText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(postText.Text))
            {
               postText.Text = "请输入动态内容…";
            }
        }

        /// <summary>
        /// 滑出右侧菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPanel_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {

            double mLeft = ContentPanel.Margin.Left + e.DeltaManipulation.Translation.X;
            if (mLeft < -85)
            {
                mLeft = -85;
            }
            else if (mLeft > 0)
            {
                mLeft = 0;
            }

            ContentPanel.Margin = new Thickness(mLeft, 60, 0, 0);
        }
        /// <summary>
        /// 滑出右侧菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRoot_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

            double mLeft = ContentPanel.Margin.Left;
            if (mLeft < -40)
            {
                mLeft = -85;
            }
            else
            {
                mLeft = 0;
            }

            ContentPanel.Margin = new Thickness(mLeft, 60, 0, 0);
        }

    

        


    }
}