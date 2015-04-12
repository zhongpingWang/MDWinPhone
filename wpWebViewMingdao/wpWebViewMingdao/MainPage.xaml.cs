using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.Serialization.Json;
using System.Text;
using wpWebViewMingdao.Helper;
using Windows.UI.Core;

// “WebView 应用程序”模板在 http://go.microsoft.com/fwlink/?LinkID=391641 上有介绍

namespace wpWebViewMingdao
{
    public sealed partial class MainPage : Page
    {
        // TODO: 在此处替换为您的 URL。
        private static readonly Uri HomeUri = new Uri("ms-appx-web:///Html/login.html", UriKind.Absolute);

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            WebViewControl.Navigate(HomeUri);

            HardwareButtons.BackPressed += this.MainPage_BackPressed;
        }

        /// <summary>
        /// 在离开此页时调用。
        /// </summary>
        /// <param name="e">描述如何导航此页的事件数据。</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= this.MainPage_BackPressed;
        }

        /// <summary>
        /// 重写后退按钮按压事件以在 WebView (而不是应用程序)的返回栈中导航。
        /// </summary>
        private void MainPage_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (WebViewControl.CanGoBack)
            {
                WebViewControl.GoBack();
                e.Handled = true;
            }
        }

        private void Browser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!args.IsSuccess)
            {
                Debug.WriteLine("Navigation to this page failed, check your internet connection.");
            }
        }

        /// <summary>
        /// 在 WebView 的历史记录中向前导航。
        /// </summary>
        private async void ForwardAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (WebViewControl.CanGoForward)
            {
                WebViewControl.GoForward();
            }
            string test = "dsfsdafdsaf";
            await WebViewControl.InvokeScriptAsync("Login.loginCallback", new string[] { test });
        }

        /// <summary>
        /// 导航到初始主页。
        /// </summary>
        private void HomeAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            WebViewControl.Navigate(HomeUri);
        }

        private void WebViewControl_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var data = e.Value;
            UserInfo login = new UserInfo();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(login.GetType());
            login = ser.ReadObject(ms) as UserInfo;


            // 构建URL内容
            string uriStr ="https://api.mingdao.com/ajaxpage/authorizeAjax.aspx?type=GetRequestCode";//

            Dictionary<string, string> dic = new Dictionary<string, string>() {  
               {"UserEmail",login.userName},
               {"UserPsw",login.userPwd},
               {"app_key","68B09E2BA977"},//BE5472E1492BEFD650D6B4C71F101DCD
               {"redirect_uri","http://www.baidu.com"},//E771069CB1159D197C9733ABF7D5F75 
               {"ProjectID",""},//fe288386-3d26-4eab-b5d2-51eeab82a7f9  
               {"appID",""}, 
               {"state","0"},
                {"chkCode",""}
            };
            //http请求
            HttpWeb.CreatePostHttpResponse(uriStr, dic, LoginResult);  
            
        }

        public async void LoginResult(string responseStr)
        {  
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                WebViewControl.InvokeScriptAsync("loginCallback", new string[] { responseStr });
                // this method does not get called every second
            });  
        } 
    }

     
       



   public  class UserInfo
    {
        private string _type;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }



        private string _userName;

        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _userPwd;

        public string userPwd
        {
            get { return _userPwd; }
            set { _userPwd = value; }
        }


    }


}
