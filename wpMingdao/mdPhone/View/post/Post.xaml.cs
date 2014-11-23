using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using mdPhone.Model.post;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using mdPhone.Helper;
using mdPhone.ViewModel;
using mdPhone.Model;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace mdPhone.View
{
    public partial class Post : PhoneApplicationPage
    { 
        public Post()
        {
            InitializeComponent();
        }
         
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            //string post = DataManager.LoadPostSettings("post");
            //if (!string.IsNullOrEmpty(post))
            //{
            //    ResultNewPost(post);
            //    //已有数据 取消中间加载
            //    fristLoad.Visibility = Visibility.Collapsed;
            //}

            //第一次取消头部 刷新
            getNew.Visibility = Visibility.Collapsed;
            //获取参数
            string type = "PostAll";
            if (NavigationContext.QueryString.ContainsKey("type"))
            {
                type = NavigationContext.QueryString["type"];
            }
            getPostByType(type);
            base.OnNavigatedTo(e);
        }
        PostEnum postenum = new PostEnum();
        /// <summary>
        /// 获取动态更新 根据类型
        /// </summary>
        private void getPostByType(string type)
        {
            if (type == "PostAll")
            {
                postenum = PostEnum.PostAll;
            }
            else if (type == "Atme2")
            {
                postenum = PostEnum.Atme2;
            }
            else if (type == "Replybyme")
            {
                postenum = PostEnum.Replybyme;
            }
            else
            {
                postenum = PostEnum.PostAll;
            }
            PostViewModel.GetNewPost(postenum, ResultNewPost);

        }


        public void ResultNewPost(string resutlt)
        {
            //错误信息 
            if (resutlt.IndexOf("error_code")>-1)
            {
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
            else
            {
                DataManager.SaveUserInformation(resutlt,"post");
                //resutlt 字符串中的 大小写 及名称和类中的一致
                Posts post = new Posts();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(post.GetType());
                post = ser.ReadObject(ms) as Posts;
                ms.Close();
                ms.Dispose();


                Dispatcher.BeginInvoke(() =>
                {
                    this.postListBox.ItemsSource = post.posts;
                    fristLoad.Visibility = Visibility.Collapsed;
                    getNew.Visibility = Visibility.Collapsed;
               
                });
            } 
        } 





        //回复
        private void replyPost_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a reply");
        }

        //新增动态
        private void add_post_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/post/AddPost.xaml", UriKind.Relative));
            
        }


        private void breakPost_mouseup(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 滑出右侧菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPanel_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            
           double mLeft=ContentPanel.Margin.Left+ e.DeltaManipulation.Translation.X;
           if (mLeft<-85)
           {
               mLeft = -85;
           }
           else if (mLeft>0)
           {
               mLeft = 0;
           }
           ContentPanel.Margin = new Thickness(mLeft,0,0,0);
                
               
           
        }


        private void PhoneApplicationPage_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {

            double x = e.DeltaManipulation.Translation.X;
            double x2 = e.CumulativeManipulation.Translation.X;
            // this.ContentPanel.X = e.CumulativeManipulation.Translation.X ;


        }


        private void PhoneApplicationPage_ManipulationCompleted(object sender, ManipulationDeltaEventArgs e)
        {
            double x = e.DeltaManipulation.Translation.X;
            double x2 = e.CumulativeManipulation.Translation.X;
            // this.ContentPanel.X = e.CumulativeManipulation.Translation.X ;


        }
        


    }
}