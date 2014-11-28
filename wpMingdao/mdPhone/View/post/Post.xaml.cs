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
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Collections;
using System.Windows.Media.Imaging;

namespace mdPhone.View
{
    public partial class Post : PhoneApplicationPage
    {
        private string since_id = "";
        private int pageSize = 35;
        private string max_id = "";
        private bool isMore = true;
        private ObservableCollection<posts> PostAllData;
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
            PostAllData = new ObservableCollection<posts>();
            //第一次取消头部 刷新
            getNew.Visibility = Visibility.Collapsed;
            //获取参数
            string type = "PostAll";
            if (NavigationContext.QueryString.ContainsKey("type"))
            {
                type = NavigationContext.QueryString["type"];
            }
            // 获取动态更新 根据类型
            getPostByType(type);
            base.OnNavigatedTo(e);
        }
        PostEnum postenum = new PostEnum();
        /// <summary>
        /// 获取动态更新 根据类型
        /// </summary>
        private void getPostByType(string type)
        {
            Dictionary<string, string> parm = new Dictionary<string, string>();
            if (type == "PostAll")
            {
                if (!string.IsNullOrEmpty(max_id))
                {
                    //parm.Add("since_id",since_id);
                    parm.Add("max_id", max_id);
                    parm.Add("pagesize", pageSize.ToString());
                }
                else
                {
                    parm.Add("pagesize", pageSize.ToString());
                }
                postenum = PostEnum.PostAll;
                PostViewModel.GetUlity(postenum, parm, ResultNewPost);
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
                PostViewModel.GetUlity(postenum, parm, ResultNewPost);
            }
            SelectImage(postenum);
        }

        private void SelectImage(PostEnum selTab) {
            if (selTab==PostEnum.PostAll)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    allMsgImg.Source = new BitmapImage(new Uri("/Images/post/allMsgSel.png", UriKind.Relative));
                    atMeImg.Source = new BitmapImage(new Uri("/Images/post/atMe.png", UriKind.Relative));
                    replyMeImg.Source = new BitmapImage(new Uri("/Images/post/replyMe.png", UriKind.Relative));
                });
                
            }
            else if (selTab == PostEnum.Replybyme)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    allMsgImg.Source = new BitmapImage(new Uri("/Images/post/allMsg.png", UriKind.Relative));
                    atMeImg.Source = new BitmapImage(new Uri("/Images/post/atMe.png", UriKind.Relative));
                    replyMeImg.Source = new BitmapImage(new Uri("/Images/post/replyMeSel.png", UriKind.Relative));
                });
              
            }
            else if (selTab == PostEnum.Atme2)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    allMsgImg.Source = new BitmapImage(new Uri("/Images/post/allMsg.png", UriKind.Relative));
                    atMeImg.Source = new BitmapImage(new Uri("/Images/post/atMeSel.png", UriKind.Relative));
                    replyMeImg.Source = new BitmapImage(new Uri("/Images/post/replyMe.png", UriKind.Relative));
                });
                
            } 
            
        }


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
                DataManager.SaveUserInformation(resutlt, "post");
                //resutlt 字符串中的 大小写 及名称和类中的一致
                Posts post = new Posts();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(post.GetType());
                post = ser.ReadObject(ms) as Posts;
                since_id = post.sincepostid;
                max_id = post.sincepostid; 
                isMore= Convert.ToBoolean(Convert.ToInt32(post.more));
               // max_id=post.posts[post.posts.Count-1].id; 
               
                ms.Close();
                ms.Dispose();

                Dispatcher.BeginInvoke(() =>
                {
                    foreach (var item in post.posts)
                    {
                        PostAllData.Add(item);
                    } 
                    this.postListBox.ItemsSource = PostAllData; //post.posts; 

                });

                ResultRefreshImage();
            }
        }

        private void ResultRefreshImage() {
            Dispatcher.BeginInvoke(() =>
            {
                loadMoreIcon.Source = new BitmapImage(new Uri("/Images/post/loadMoreNew.png", UriKind.Relative));
                refresh.Source = new BitmapImage(new Uri("/Images/post/refresh.png", UriKind.Relative));
                fristLoad.Visibility = Visibility.Collapsed;
                getNew.Visibility = Visibility.Collapsed;
            });
           
        }


      

        //回复
        private void replyPost_Click(object sender, RoutedEventArgs e)
        { 
            HyperlinkButton hb=sender as HyperlinkButton;  
            NavigationService.Navigate(new Uri("/View/post/ReplyPost.xaml?id="+hb.Tag.ToString(), UriKind.Relative)); 
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

            double mLeft = ContentPanel.Margin.Left + e.DeltaManipulation.Translation.X;
            if (mLeft < -85)
            {
                mLeft = -85;
            }
            else if (mLeft > 0)
            {
                mLeft = 0;
            }

            ContentPanel.Margin = new Thickness(mLeft, 0, 0, 0); 
        }

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

            ContentPanel.Margin = new Thickness(mLeft, 0, 0, 0);
        }

        /// <summary>
        /// 加载更多
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadMoreIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (getNew.Visibility == Visibility.Visible)
            {
                return;
            }
            if (isMore)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    getNew.Visibility = Visibility.Visible; 
                    loadMoreIcon.Source = new BitmapImage(new Uri("/Images/post/loadMoreNewSel.png", UriKind.Relative));
                });


                if (postenum == PostEnum.PostAll)
                {
                    getPostByType("PostAll");
                }
            }
          
           
        }

        private void refresh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (getNew.Visibility==Visibility.Visible)
            {
                return;
            }

            Dispatcher.BeginInvoke(() =>
            {
                getNew.Visibility = Visibility.Visible;
                refresh.Source = new BitmapImage(new Uri("/Images/post/refreshSel.png", UriKind.Relative)); 
            });
            
            since_id = "";
            max_id = "";
            PostAllData = new ObservableCollection<posts>();
            if (postenum == PostEnum.PostAll)
            {
                getPostByType("PostAll");
            }
        }

       

     


    }
}