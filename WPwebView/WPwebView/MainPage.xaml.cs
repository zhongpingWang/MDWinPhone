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

// “WebView 应用程序”模板在 http://go.microsoft.com/fwlink/?LinkID=391641 上有介绍

namespace WPwebView
{
    public sealed partial class MainPage : Page
    {
        // TODO: 在此处替换为您的 URL。//ms-appx-web:///Html/index.html?a=1
        //https://api.mingdao.com/auth2/authorize?app_key=68B09E2BA977&redirect_uri=http://zhongping.wang@mingdao.com
        private static readonly Uri HomeUri = new Uri("ms-appx-web:///Html/index.html", UriKind.Absolute);//

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

        private async void Browser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!args.IsSuccess)
            {
                Debug.WriteLine("Navigation to this page failed, check your internet connection.");
            }
            else
            {

                string login = @"function UserLogin(ProjectID) {
                        backurl = ''; 
                        var isright = CheckIsEmpty();
                         if (isright) {
                             var username = $('#UserEmail').val();
        username = username.replace(/(^\s*)|(\s*$)/g, '');
        var usernpsw = $('#UserPsw').val();
        usernpsw = usernpsw.replace(/(^\s*)|(\s*$)/g, ''); 
        var isright2 =true
        if (isright2) {
            var app_key = GetQueryString('app_key');
            if (!app_key)
                app_key = GetQueryString('client_id');
            var state = '';
            if (GetQueryString('state') != null) {
                state = GetQueryString('state');
            } 
            AjaxRequest('../ajaxpage/authorizeAjax.aspx?type=GetRequestCode', 'POST',
            {
                UserEmail: username,
                UserPsw: usernpsw,
                ProjectID: ProjectID,
                app_key: app_key,
                state: state,
                chkCode: $('#txt_chkCode').val()
            },
            function (data) {
                 window.external.notify(data);
                //授权验证码的时候 出现错误
                if (data.type == '1') {
                    alert(data.error, 2);

                    if ($('#tr_chkCode').is(':visible')) {
                        $('#btn_chkCode').click();
                    }
                }
                //授权成功
                else if (data.type == '3') {
                    backurl = data.url;
                    jQuery.mobile.changePage('#allow_access');
                }
                else if (data.type == '4')
                    window.location.href = data.url;
                else if (data.type == '5') {
                    alert('外联用户无法访问', 2);
                }
                else if (data.type == '6') {
                    alert('免费网络无法使用企业安装模式的应用。请升级高级模式', 3);
                    setTimeout(function () { window.location.href = 'http://www.mingdao.com/upgradechoose'; }, 2000);
                }
                else if (data.type == '9') {
                    alert('当前网络没有安装' + data.appName + '应用，请到明道应用市场安装应用', 3);
                    setTimeout(function () { window.location.href = 'http://app.mingdao.com'; }, 2000);
                }
                else if (data.type == '10') {
                    alert('当前用户没有安装' + data.appName + '应用，请到明道应用市场安装应用', 3);
                    setTimeout(function () { window.location.href = 'http://app.mingdao.com'; }, 2000);
                }
                else if (data.type == '7') {
                    alert(data.error, 2);
                    $('#tr_chkCode').show();
                    $('#btn_chkCode').click();
                }
                else if (data.type == '8') {
                    alert(data.error, 2);
                    $('#tr_chkCode').show();
                    $('#btn_chkCode').click();
                }
                //此用户拥有多个网络
                else {
                    var login = $('#MDlogin_Click');
                    login.CheckProject({
                        UserEmail: username,
                        UserPsw: usernpsw,
                        ProjectID: ProjectID,
                        app_key: app_key,
                        state: state,
                        type: 'GetRequestCode',
                        checked: function (data) {
                            UserLogin(data.ProjectID);
                        }
                    });
                }

            }
            );
        }
    }
}";
                //login = " function UserLogin(){ alert(3234); window.external.notify(123); alert(3);   }  ";
                //await WebViewControl.InvokeScriptAsync("eval", new string[] { login });
              //  await WebViewControl.InvokeScriptAsync("setValue", new string[] { "testsss" });
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
            await WebViewControl.InvokeScriptAsync("setValue", new string[] { "testsss" });
        }

        /// <summary>
        /// 导航到初始主页。
        /// </summary>
        private void HomeAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            WebViewControl.Navigate(HomeUri);
        }

        //js回调
        private void WebViewControl_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var a = e.Value;
        }

        private void WebViewControl_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            var a = 0;
        }
    }
}
