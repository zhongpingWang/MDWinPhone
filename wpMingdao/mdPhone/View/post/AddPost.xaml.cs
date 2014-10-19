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
            Dictionary<string, string> postDic = new Dictionary<string, string>() {
               {"g_id",string.Empty},
               {"p_msg",postText.Text.Trim()},
               {"p_type","0"},
               {"s_type","3"},
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
                NavigationService.Navigate(new Uri("../Login.xaml", UriKind.Relative));
            }
            else
            {
                //DataManager.SaveUserInformation(resutlt, "post");
                ////resutlt 字符串中的 大小写 及名称和类中的一致
                //Posts post = new Posts();
                //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(post.GetType());
                //post = ser.ReadObject(ms) as Posts;
                //ms.Close();
                //ms.Dispose();


                //Dispatcher.BeginInvoke(() =>
                //{
                //    this.postListBox.ItemsSource = post.posts;
                //    fristLoad.Visibility = Visibility.Collapsed;
                //    getNew.Visibility = Visibility.Collapsed;

                //});
            }
        }

    }
}