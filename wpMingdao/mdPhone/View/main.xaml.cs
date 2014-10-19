using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using mdPhone.View.mainPart;
using System.Windows.Input;
using System.Windows.Media;
using Coding4Fun.Phone.Controls;

namespace mdPhone.View
{
    
    public partial class main : PhoneApplicationPage
    {
          /// <summary>
        /// Ordered list of the panorama pages
        /// </summary>
        protected List<UserControl> PageList { get; set; }
        /// <summary>
        /// Size of the pages
        /// </summary>
        public const int PageWidth = 480;
        /// <summary>
        /// Index of the page currently displayed
        /// </summary>
        protected int CurrentPageIndex { get; set; }
        public main()
        {
           

            //创建三个页面列表
            this.PageList = new List<UserControl>() 
                { 
                    new home() { IsEnabled = false }, 
                    new msg() { IsEnabled = false }, 
                    new search() { IsEnabled = false },
                    new create() { IsEnabled = false } 
                };

            this.CurrentPageIndex = 0;

            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.Portrait;

        }

        DateTime backTime = DateTime.Now;
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

        #region 页面切花效果


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //var frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            //frame.Width = PageWidth * 4;

            //载入panoramictitle控件
            // var title = new PanoramicTitle();

            UserControl title = new panoramicTile();
            this.TitlePanel.Children.Add(title);

            this.LoadPages();
        }

        private void LoadPages()
        { 
            //滑倒第一页再向左滑动
            if (CurrentPageIndex == -1)
            {
                CurrentPageIndex = 3;
            } 
            //滑倒第三页再向右滑动
            if (CurrentPageIndex == 4)
            { 
                CurrentPageIndex = 0;
            }
            this.PanoramicGrid.Children.Clear();
            var currentPage = this.PageList[this.CurrentPageIndex];
            currentPage.IsEnabled = true;

            this.PanoramicGrid.Children.Add(currentPage);

            Grid.SetColumn(currentPage, 1);
            Grid.SetRow(currentPage, 0);

            if (this.PageList.Count > this.CurrentPageIndex + 1)
            {
                var nextPage = this.PageList[this.CurrentPageIndex + 1];
                nextPage.IsEnabled = false;

                this.PanoramicGrid.Children.Add(nextPage);

                Grid.SetColumn(nextPage, 2);
                Grid.SetRow(nextPage, 0);
            }
            else {

                var nextPage = this.PageList[0];
                nextPage.IsEnabled = false;

                this.PanoramicGrid.Children.Add(nextPage);

                Grid.SetColumn(nextPage, 2);
                Grid.SetRow(nextPage, 0);
            
            }

            if (this.CurrentPageIndex > 0)
            {
                var previousPage = this.PageList[this.CurrentPageIndex - 1];
                previousPage.IsEnabled = false;

                this.PanoramicGrid.Children.Add(previousPage);

                Grid.SetColumn(previousPage, 0);
                Grid.SetRow(previousPage, 0);
            }
            else {
                var previousPage = this.PageList[this.PageList.Count-1];
                previousPage.IsEnabled = false;

                this.PanoramicGrid.Children.Add(previousPage);

                Grid.SetColumn(previousPage, 0);
                Grid.SetRow(previousPage, 0);
            
            }
        }

        private void PhoneApplicationPage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            //if (e.OriginalSource is Panel)
            //{
            if (e.TotalManipulation.Translation.X < 0)
            {
                if (e.TotalManipulation.Translation.X > -100 /*|| this.CurrentPageIndex >= this.PageList.Count - 1*/)
                {
                    this.SlideTitleDoubleAnimation.To = this.CurrentPageIndex * PageWidth / 2 * -1;
                    this.PageChangeAnimation.Begin();
                }
                else
                {
                    this.ChangePage(1);
                }
            }
            else if (e.TotalManipulation.Translation.X > 0)
            {
                if (e.TotalManipulation.Translation.X < 100)
                {
                    this.SlideTitleDoubleAnimation.To = this.CurrentPageIndex * PageWidth / 2 * -1;
                    this.PageChangeAnimation.Begin();
                }
                else
                {
                    this.ChangePage(-1);
                }
            }
            // }
        }

        private void ChangePage(int step)
        {
            this.CurrentPageIndex += step;

            this.LoadPages();

            SelectPage elPage = (SelectPage)this.CurrentPageIndex;
            pageSelectBackground(elPage);

            this.PanoramaContentTranslate.X += PageWidth * step;

            this.SlideTitleDoubleAnimation.To = this.CurrentPageIndex * PageWidth / 2 * -1;

            this.PageChangeAnimation.Begin();
        }

        private void PhoneApplicationPage_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            //if (e.OriginalSource is Panel)
            //{ 
                this.PanoramaContentTranslate.X = e.CumulativeManipulation.Translation.X - PageWidth;

                this.TitleTranslate.X = e.CumulativeManipulation.Translation.X / 2 - (this.CurrentPageIndex * PageWidth / 2);  
           
            // }
        }

        #endregion
        
        /// <summary>
        /// 主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        { 
            pageSelectBackground(SelectPage.Home);
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessage_Click(object sender, RoutedEventArgs e)
        {
            pageSelectBackground(SelectPage.Msg);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            pageSelectBackground(SelectPage.Search);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            pageSelectBackground(SelectPage.Create);
        }

        /// <summary>
        /// 选中页面
        /// </summary>
        private void pageSelectBackground(SelectPage elPage)  
        {
            btnHome.Background = new SolidColorBrush(Colors.Black);
            btnMessage.Background = new SolidColorBrush(Colors.Black);
            btnSearch.Background = new SolidColorBrush(Colors.Black);
            btnCreate.Background = new SolidColorBrush(Colors.Black); 
            switch (elPage)
            {
                case SelectPage.Home:
                    CurrentPageIndex = 0;
                    btnHome.Background = new SolidColorBrush(Colors.Red);
                    break;
                case SelectPage.Msg:
                    CurrentPageIndex = 1;
                    btnMessage.Background = new SolidColorBrush(Colors.Red);
                    break;
                case SelectPage.Search:
                    CurrentPageIndex = 2;
                    btnSearch.Background = new SolidColorBrush(Colors.Red);
                    break;
                case SelectPage.Create:
                    CurrentPageIndex = 3;
                    btnCreate.Background = new SolidColorBrush(Colors.Red);
                    break;
                default:
                    break;
            }
            this.LoadPages();
        }


     


    }

       public enum SelectPage 
        {
           Home=0,
           Msg=1,
           Search=2,
           Create=3 
        };

}