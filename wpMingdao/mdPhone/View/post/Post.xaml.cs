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
using Coding4Fun.Phone.Controls;

namespace mdPhone.View
{
    public partial class Post : PhoneApplicationPage
    {
        private string since_id = "";
        private int pageSize = 35;
        private string max_id = "";
        private bool isMore = true;
        private int pageindex = 1;
        private ObservableCollection<posts> PostAllData;
        private ObservableCollection<replyment> ReplymentsData;
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
            ReplymentsData = new ObservableCollection<replyment>();
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
                chargeList(0);
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
                title.Text = "动态更新";
                PostViewModel.GetUlity(postenum, parm, ResultNewPost);
            }
            else if (type == "Atme2")
            {
                chargeList(0);
                postenum = PostEnum.Atme2;
                parm.Add("pageindex", pageindex.ToString());
                parm.Add("pagesize", pageSize.ToString());
                title.Text = "提到我的";
                PostViewModel.GetUlity(postenum, parm, ResultNewPost);
            }
            else if (type == "Replyme")
            {
                chargeList(1);
                postenum = PostEnum.Replyme;
                parm.Add("max_id", max_id);
                parm.Add("pagesize", pageSize.ToString());
                title.Text = "回复我的";
                PostViewModel.GetUlity(postenum, parm, ResultReplybyme);

            }
            else
            {
                chargeList(0);
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
            SelectImage(postenum);
        }

        /// <summary>
        /// 切换textboxlist
        /// </summary>
        /// <param name="type"></param>
        private void chargeList(int type)
        {
            if (type == 1)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    this.replyListBox.Visibility = Visibility.Visible;
                    this.postListBox.Visibility = Visibility.Collapsed;
                });
            }
            else
            {
                this.replyListBox.Visibility = Visibility.Collapsed;
                this.postListBox.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// 回复我的
        /// </summary>
        /// <param name="resutlt"></param>
        public void ResultReplybyme(string resutlt)
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
                resutlt = resutlt.Replace("ref", "ref2");
                DataManager.SaveUserInformation(resutlt, "ResultReplyme");
                //resutlt 字符串中的 大小写 及名称和类中的一致
                Replyments reply = new Replyments();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(reply.GetType());
                reply = ser.ReadObject(ms) as Replyments;
                max_id = reply.sincepostid;
                isMore = Convert.ToBoolean(Convert.ToInt32(reply.more));

                ms.Close();
                ms.Dispose();

                Dispatcher.BeginInvoke(() =>
                {
                    foreach (var item in reply.replyments)
                    {
                        ReplymentsData.Add(item);
                    }
                    this.replyListBox.ItemsSource = ReplymentsData; //post.posts; 
                    Coding4FunForMsg("加载完成", "", 1000);
                });

                ResultRefreshImage();
            }
        }



        /// <summary>
        /// 提到我的
        /// </summary>
        /// <param name="resutlt"></param>
        public void ResultAtme2(string resutlt)
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
                DataManager.SaveUserInformation(resutlt, "atme2");
                //resutlt 字符串中的 大小写 及名称和类中的一致
                Posts post = new Posts();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(post.GetType());
                post = ser.ReadObject(ms) as Posts;
                since_id = post.sincepostid;
                max_id = post.sincepostid;
                isMore = Convert.ToBoolean(Convert.ToInt32(post.more));
                if (post.posts.Count < 35)
                {
                    isMore = false;
                }
                ms.Close();
                ms.Dispose();

                Dispatcher.BeginInvoke(() =>
                {
                    foreach (var item in post.posts)
                    {
                        PostAllData.Add(item);
                    }
                    this.postListBox.ItemsSource = PostAllData; //post.posts; 
                    Coding4FunForMsg("加载完成", "", 1000);
                });

                ResultRefreshImage();
            }
        }



        /// <summary>
        /// 选中tab
        /// </summary>
        /// <param name="selTab"></param>
        private void SelectImage(PostEnum selTab)
        {
            if (selTab == PostEnum.PostAll)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    allMsgImg.Source = new BitmapImage(new Uri("/Images/post/allMsgSel.png", UriKind.Relative));
                    atMeImg.Source = new BitmapImage(new Uri("/Images/post/atMe.png", UriKind.Relative));
                    replyMeImg.Source = new BitmapImage(new Uri("/Images/post/replyMe.png", UriKind.Relative));
                });

            }
            else if (selTab == PostEnum.Replyme)
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

        /// <summary>
        /// 全部动态
        /// </summary>
        /// <param name="resutlt"></param>
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
                isMore = Convert.ToBoolean(Convert.ToInt32(post.more));
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
                    Coding4FunForMsg("加载完成", "", 1000);
                });

                ResultRefreshImage();
            }
        }

        /// <summary>
        /// 清除图片效果
        /// </summary>
        private void ResultRefreshImage()
        {
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
            HyperlinkButton hb = sender as HyperlinkButton;
            NavigationService.Navigate(new Uri("/View/post/ReplyPost.xaml?id=" + hb.Tag.ToString(), UriKind.Relative));
        }

        //新增动态
        private void add_post_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/post/AddPost.xaml", UriKind.Relative));

        }

        /// <summary>
        /// 切换tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void breakPost_mouseup(object sender, EventArgs e)
        { 
            Image image = sender as Image;
            string type = image.Tag.ToString(); 
            ClearData();
            getPostByType(type);

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
                Coding4FunForMsg("数据加载中……", "", 1000);
            }
            else
            {
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
                    else if (postenum == PostEnum.Atme2)
                    {
                        pageindex++;
                        getPostByType("Atme2");
                    }
                    else if (postenum == PostEnum.Replyme)
                    {
                        getPostByType("Replyme");
                    }
                }
                else
                {
                    Coding4FunForMsg("没有更多数据了……", "", 1000); 
                }

            }



        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refresh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (getNew.Visibility == Visibility.Visible)
            {
                Coding4FunForMsg("数据加载中……", "", 1000);
                return;
            }

            Dispatcher.BeginInvoke(() =>
            {
                getNew.Visibility = Visibility.Visible;
                refresh.Source = new BitmapImage(new Uri("/Images/post/refreshSel.png", UriKind.Relative));
            });

            ClearData();
            if (postenum == PostEnum.PostAll)
            {
                getPostByType("PostAll");
            }
            else if (postenum == PostEnum.Atme2)
            {
                getPostByType("Atme2");
            }
            else if (postenum == PostEnum.Replyme)
            {
                getPostByType("Replyme");
            }
        }


        /// <summary>
        /// 清除 数据
        /// </summary>
        private void ClearData() {
            since_id = "";
            max_id = "";
            pageindex = 1;
            isMore = true;
            PostAllData = new ObservableCollection<posts>();
            ReplymentsData = new ObservableCollection<replyment>();
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


    }
}