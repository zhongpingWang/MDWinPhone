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

namespace mdPhone.View
{
    public partial class Post : PhoneApplicationPage
    {
        private int pageIndex = 1;
        private int pageSize = 35;
        private ObservableCollection<List<posts>> PostAllData;
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
            PostAllData = new ObservableCollection<List<posts>>();
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
            PostViewModel.GetNewPost(postenum, pageSize.ToString(), "", ResultNewPost);

        }


        public void ResultNewPost(string resutlt)
        {
            //错误信息 
            if (resutlt.IndexOf("error_code") > -1)
            {
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
            else
            {
                DataManager.SaveUserInformation(resutlt, "post");
                //resutlt 字符串中的 大小写 及名称和类中的一致
                Posts post = new Posts();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(post.GetType());
                post = ser.ReadObject(ms) as Posts;
                PostAllData.Add(post.posts);
                ms.Close();
                ms.Dispose();

                Dispatcher.BeginInvoke(() =>
                {
                    this.postListBox.ItemsSource = PostAllData[0]; //post.posts;
                    fristLoad.Visibility = Visibility.Collapsed;
                    getNew.Visibility = Visibility.Collapsed;

                });
            }
        }

        private void ResultLoadPostAll(string resultJson)
        {
            //DataManager.SaveUserInformation(resultJson, "post");
            //resutlt 字符串中的 大小写 及名称和类中的一致
            Posts post = new Posts();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultJson));
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


        private void postListBox_MouseMove(object sender, MouseEventArgs e)
        {
            //获取listbox的子类型ScrollViewer 
            //  ScrollViewer scrollViewer = FindChildOfType<ScrollViewer>(postListBox);//ScrollViewer  scrollBar
            if (scrollViewer == null)
            {
                throw new InvalidOperationException("erro");
            }
            else
            {
               // title.Text = scrollViewer.VerticalOffset.ToString();
                //判断当前滚动的高度是否大于或者等于scrollViewer实际可滚动高度，如果等于或者大于就证明到底了
                //if (scrollViewer.VerticalOffset + 2 >= scrollViewer.ScrollableHeight)
                //{
                //    //处理listbox滚动到底的事情
                //    loadMore.Visibility = Visibility.Visible;
                //}
                //else {
                //    loadMore.Visibility = Visibility.Collapsed;
                //}
            }
        }


        private void postListBox_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

            //判断当前滚动的高度是否大于或者等于scrollViewer实际可滚动高度，如果等于或者大于就证明到底了
            if (scrollViewer.VerticalOffset + 2 >= scrollViewer.ScrollableHeight)
            {
                //处理listbox滚动到底的事情
                loadMore.Visibility = Visibility.Visible;
            }
            else
            {
                loadMore.Visibility = Visibility.Collapsed;
            }
        }


        ScrollViewer scrollViewer;
        private TextBlock _activeTxb = null;
        private ScrollBar sb = null;
        private ScrollViewer sv = null;
        private bool _isBouncy = false;
        private bool alreadyHookedScrollEvents = false;
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            if (alreadyHookedScrollEvents)
                return;

            alreadyHookedScrollEvents = true;
            //postListBox.AddHandler(ListBox.ManipulationCompletedEvent, (EventHandler<ManipulationCompletedEventArgs>)LB_ManipulationCompleted, true);
            sb = (ScrollBar)FindElementRecursive(postListBox, typeof(ScrollBar));
            sv = (ScrollViewer)FindElementRecursive(postListBox, typeof(ScrollViewer));

            if (sv != null)
            {
                // Visual States are always on the first child of the control template 
                FrameworkElement element = VisualTreeHelper.GetChild(sv, 0) as FrameworkElement;
                if (element != null)
                {
                    VisualStateGroup group = FindVisualState(element, "ScrollStates");
                    if (group != null)
                    {
                        group.CurrentStateChanging += new EventHandler<VisualStateChangedEventArgs>(group_CurrentStateChanging);
                    }
                    VisualStateGroup vgroup = FindVisualState(element, "VerticalCompression");
                    VisualStateGroup hgroup = FindVisualState(element, "HorizontalCompression");
                    if (vgroup != null)
                    {
                        vgroup.CurrentStateChanging += new EventHandler<VisualStateChangedEventArgs>(vgroup_CurrentStateChanging);
                    }
                    if (hgroup != null)
                    {
                        hgroup.CurrentStateChanging += new EventHandler<VisualStateChangedEventArgs>(hgroup_CurrentStateChanging);
                    }
                }
            }

            ////获取listbox的子类型ScrollViewer
            //scrollViewer = FindChildOfType<ScrollViewer>(postListBox);//ScrollViewer  scrollBar
            //if (scrollViewer == null)
            //{
            //    throw new InvalidOperationException("erro");
            //}
            //else
            //{
            //    scrollViewer.MouseMove += postListBox_MouseMove;
            //    // scrollViewer.ManipulationCompleted += postListBox_ManipulationCompleted;
            //}
        }


        private void hgroup_CurrentStateChanging(object sender, VisualStateChangedEventArgs e)
        {
            if (e.NewState.Name == "CompressionLeft")
            {
                //NoHCompression
                //HCompressionTxb.Foreground = new SolidColorBrush(Colors.White);
                //LEFT
                // LeftTxb.Foreground = new SolidColorBrush(Colors.Green);
            }

            if (e.NewState.Name == "CompressionRight")
            {
                //NoHCompression
                //HCompressionTxb.Foreground = new SolidColorBrush(Colors.White);
                //right
                //RightTxb.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (e.NewState.Name == "NoHorizontalCompression")
            {
                //left
                //LeftTxb.Foreground = new SolidColorBrush(Colors.White);
                //right
                // RightTxb.Foreground = new SolidColorBrush(Colors.White);
                //NoHCompression
                //  HCompressionTxb.Foreground = new SolidColorBrush(Colors.Green);
            }
        }
        private void vgroup_CurrentStateChanging(object sender, VisualStateChangedEventArgs e)
        {
            if (e.NewState.Name == "CompressionTop")
            {
                //NoVCompression
                // VCompressionTxb.Foreground = new SolidColorBrush(Colors.White);
                //top
                //TopTxb.Foreground = new SolidColorBrush(Colors.Green);
            }

            if (e.NewState.Name == "CompressionBottom")
            {
                loadMore.Visibility = Visibility.Visible;

                // VCompressionTxb.Foreground = new SolidColorBrush(Colors.White);
                // BottomTxb.Foreground = new SolidColorBrush(Colors.Green);

                /*
                 * As an example, this is where you can add code to load new items, have the progress bar animation and so on
                 *                  
                 */
            }
            if (e.NewState.Name == "NoVerticalCompression")
            {
                //loadMore.Visibility = Visibility.Collapsed;
                //TopTxb.Foreground = new SolidColorBrush(Colors.White);
                //BottomTxb.Foreground = new SolidColorBrush(Colors.White);
                // VCompressionTxb.Foreground = new SolidColorBrush(Colors.Green);
            }
        }
        private void group_CurrentStateChanging(object sender, VisualStateChangedEventArgs e)
        {
            if (e.NewState.Name == "Scrolling")
            {
                //string s;
                // isScrollingTxb.Foreground = new SolidColorBrush(Colors.Green);
                //AnimateText(scrollStartedTxb);
            }
            else
            {
                //string v;
                // isScrollingTxb.Foreground = new SolidColorBrush(Colors.White);
                // AnimateText(scrollCompletedTxb);
            }
        }
        //private void AnimateText(TextBlock target)
        //{
        //    _activeTxb = target;
        //    AnimateTextSB.Stop();
        //    Storyboard.SetTargetName(FontAnimation, target.Name);
        //    Storyboard.SetTargetName(FontColorAnimation, target.Name);
        //    FontAnimation.AutoReverse = true;
        //    AnimateTextSB.Begin();
        //}



        //ManipulationCompletedEvent
        private void LB_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            //isScrollingTxb.Foreground = new SolidColorBrush(Colors.White);
            //VCompressionTxb.Foreground = new SolidColorBrush(Colors.White);
            // HCompressionTxb.Foreground = new SolidColorBrush(Colors.White);
        }




        private UIElement FindElementRecursive(FrameworkElement parent, Type targetType)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            UIElement returnElement = null;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Object element = VisualTreeHelper.GetChild(parent, i);
                    if (element.GetType() == targetType)
                    {
                        return element as UIElement;
                    }
                    else
                    {
                        returnElement = FindElementRecursive(VisualTreeHelper.GetChild(parent, i) as FrameworkElement, targetType);
                    }
                }
            }
            return returnElement;
        }
        private VisualStateGroup FindVisualState(FrameworkElement element, string name)
        {
            if (element == null)
                return null;

            IList groups = VisualStateManager.GetVisualStateGroups(element);
            foreach (VisualStateGroup group in groups)
                if (group.Name == name)
                    return group;

            return null;
        }

        //获取子类型
        static T FindChildOfType<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }


        private void AnimateTextSB_Completed(object sender, EventArgs e)
        {
            _activeTxb.FontSize = 30;
            _activeTxb.Foreground = new SolidColorBrush(Colors.White);
        }


    }
}