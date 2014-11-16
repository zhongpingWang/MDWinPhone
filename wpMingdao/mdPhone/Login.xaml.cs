using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ImageTools.IO.Gif;
using ImageTools;
using mdPhone.ViewModel;
using System.IO;
using mdPhone.Model;
using System.Text;
using System.Runtime.Serialization.Json;
using mdPhone.Helper;
using System.IO.IsolatedStorage;

namespace mdPhone
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
            IsShowPassWordTip();
        }

        //重写回退事件  如果 动画层存在 则去除动画层否则退出
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (loginAnimate.Visibility != Visibility.Collapsed || manyProject.Visibility != Visibility.Collapsed)
            {
                e.Cancel = true;
                Dispatcher.BeginInvoke(() =>
                              {
                                  loginAnimate.Visibility = Visibility.Collapsed;
                                  manyProject.Visibility = Visibility.Collapsed;

                              });

            }
            base.OnBackKeyPress(e);  
        }


        //记住密码
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            loginAnimate.Visibility = Visibility.Collapsed;
            manyProject.Visibility = Visibility.Collapsed;
            login.Visibility = Visibility.Visible;
           

            if (e.NavigationMode == NavigationMode.New)
            { 
                UserInfo user = UserDataManager.LoadUserSettings();
                if (user != null && user.RememberPwd && !string.IsNullOrEmpty(user.Token))
                {
                    loginAnimate.Visibility = Visibility.Visible;
                    App.Token = user.Token;
                    NavigationService.Navigate(new Uri("/View/MainPost.xaml", UriKind.Relative));
                }
                else if (user != null && user.RememberPwd)
                {
                    txtUserEmail.Text = user.UserEmail;
                    txtPasswrod.Password = user.UserPwd;
                    loginAnimate.Visibility = Visibility.Visible;
                    txtPassWordTip.Visibility = Visibility.Collapsed;
                    LoginViewModel.UserLogin(user.UserEmail, user.UserPwd, "", LoginResult);
                }
            }
            base.OnNavigatedTo(e);
        }



        //登陆
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userEmail = txtUserEmail.Text.Trim();
            if (string.IsNullOrEmpty(userEmail))
            {
                MessageBox.Show("请输入邮箱"); 
                return;
            }
            string pwd = this.txtPasswrod.Password;
            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("请输入密码"); 
                return;
            }
            loginAnimate.Visibility = Visibility.Visible;
            //加载动画
           // ImageTools.IO.Decoders.AddDecoder<GifDecoder>();
           // Image1.Source = new ExtendedImage() { UriSource = new Uri("/Images/loading.gif", UriKind.Relative) };//Local animated gif image binding with ImageTools
            //用户登录
            LoginViewModel.UserLogin(userEmail, pwd, "", LoginResult); 
        } 
        
        public void LoginResult(string responseStr)
        {
            //多网络
            if (responseStr.IndexOf("CompanyName") > 0)
            {
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(responseStr));
                var deserializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(System.Collections.Generic.List<UserInfo>));
                //反序列化本地文件为List集合
                var list = (System.Collections.Generic.List<UserInfo>)deserializer.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                //前台
                Dispatcher.BeginInvoke(() =>
                {
                    netsImtes.ItemsSource = list;
                    manyProject.Visibility = Visibility.Visible;
                    loginAnimate.Visibility = Visibility.Collapsed;
                });
            }
            else
            {
                UserInfo login = new UserInfo();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(responseStr));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(login.GetType());
                login = ser.ReadObject(ms) as UserInfo;
                login.CompanyName = companyName;
                UserDataManager.SaveUserInformation(login);
                ms.Close();
                ms.Dispose();
                if (login.type == 1)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show(login.error);
                        loginAnimate.Visibility = Visibility.Collapsed;
                    });
                }
                else
                {
                    int codeindex = login.url.IndexOf("code") + 5;
                    string code = login.url.Substring(codeindex);
                    LoginViewModel.GetUserToken(code, GetUserTokenResult);
                }
            }
        }


        public void GetUserTokenResult(string response) 
        {
            //成功
            if (response.IndexOf("access_token") > -1)
            {
                UserInfo token = new UserInfo();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(token.GetType());
                token = ser.ReadObject(ms) as UserInfo;
                ms.Close();
                ms.Dispose();
                Dispatcher.BeginInvoke(() =>
                {

                    UserInfo user = UserDataManager.LoadUserSettings();
                    user.UserEmail = txtUserEmail.Text.Trim();
                    user.UserPwd = txtPasswrod.Password.Trim();
                    user.Token = token.access_token;
                    App.Token = token.access_token;
                    if (rememberPwd.IsChecked == true)
                    {
                        user.RememberPwd = true;
                    } 
                    UserDataManager.SaveUserInformation(user);
                    NavigationService.Navigate(new Uri("/View/MainPost.xaml", UriKind.Relative)); 
                });
            }
            else
            {
                //失败
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(response);
                    loginAnimate.Visibility = Visibility.Collapsed;
                });
            } 
        }

        private string companyName = string.Empty;
        //多网络选择
        private void TextBlock_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            string projectId = textBlock.Tag.ToString();
            companyName = textBlock.Text;
            string userEmail = txtUserEmail.Text.Trim();
            string pwd = this.txtPasswrod.Password;
            loginAnimate.Dispatcher.BeginInvoke(() =>
            {  
                loginAnimate.Visibility = Visibility.Visible;
                LoginViewModel.UserLogin(userEmail, pwd, projectId, LoginResult); 
            });
        }

        //失去焦点
        private void txtPasswrod_LostFocus(object sender, RoutedEventArgs e)
        {
            IsShowPassWordTip();
        }

        //是否显示输入密码提示
        private void IsShowPassWordTip() {

            if (!string.IsNullOrEmpty(txtPasswrod.Password))
            {
                txtPassWordTip.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtPassWordTip.Visibility = Visibility.Visible;
            }
        
        }

        //去除密码提示
        private void txtPasswrod_GotFocus(object sender, RoutedEventArgs e)
        {
            string pwd = this.txtPasswrod.Password;
            txtPassWordTip.Visibility = Visibility.Collapsed;
        }

        private void mySelf_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        { 
            txtUserEmail.Text = "zhongping.wang@mingdao.com"; 
            txtPasswrod.Password = "lovezhongping123"; 
        } 

    }
}