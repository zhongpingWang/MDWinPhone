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
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace mdPhone.View.apps.calendar
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

            string task = DataManager.LoadPostSettings("calendar_todo");
            if (!string.IsNullOrEmpty(task))
            {
                ResultTask(task);
            }

            UserInfo user = UserDataManager.LoadUserSettings();
            if (user != null && string.IsNullOrEmpty(user.Token))
            {
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }

            CalendarViewModel.GetTodo(user.Token, ResultTask);

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
                DataManager.SaveUserInformation(resutlt, "calendar_todo");
                //resutlt 字符串中的 大小写 及名称和类中的一致
                calendarList calednar = new calendarList();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(calednar.GetType());
                calednar = ser.ReadObject(ms) as calendarList;
                ms.Close();
                ms.Dispose();


                Dispatcher.BeginInvoke(() =>
                {
                    calendarImtes.ItemsSource = calednar.calendars;
                });
            }
        }

        //任务详情 
        private void calendar_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            string cId = grid.Tag.ToString(); 
            NavigationService.Navigate(new Uri("/View/apps/calendar/detail.xaml?cid="+cId, UriKind.Relative));
        } 







    }
}