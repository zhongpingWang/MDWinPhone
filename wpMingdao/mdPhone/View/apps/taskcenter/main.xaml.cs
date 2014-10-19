using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using mdPhone.Helper;
using mdPhone.Model;
using mdPhone.ViewModel;
using mdPhone.Model.task;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace mdPhone.View.apps.taskcenter
{
    public partial class main : PhoneApplicationPage
    {
        public main()
        {
            InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //UserInfo user=App.Current.Resources["user"] as UserInfo;

            string task = DataManager.LoadPostSettings("task");
            if (!string.IsNullOrEmpty(task))
            {
                ResultTask(task);
            }

            UserInfo user = UserDataManager.LoadUserSettings();
            if (user != null && string.IsNullOrEmpty(user.Token))
            {
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }

            TaskViewModel.GetMyJoin(user.Token, ResultTask);

            base.OnNavigatedTo(e);
        }

        public void ResultTask(string resutlt)
        {
            //错误信息 
            if (resutlt.IndexOf("error_code") > -1)
            {
                NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
            }
            else
            {
                DataManager.SaveUserInformation(resutlt,"task");
                //resutlt 字符串中的 大小写 及名称和类中的一致
                taskList task = new taskList();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(task.GetType());
                task = ser.ReadObject(ms) as taskList;
                ms.Close();
                ms.Dispose();


                Dispatcher.BeginInvoke(() =>
                {
                    tasksImtes.ItemsSource = task.tasks;
                });
            }
        } 

    }

    public class taskList
    {
        private List<tasks> _tasks;

        public List<tasks> tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }
    }

}