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
using System.IO.IsolatedStorage;

namespace mdPhone.View.apps.calendar
{
    public partial class detail : PhoneApplicationPage
    {
        public detail()
        {
            InitializeComponent();
        }

        private string cId;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //UserInfo user=App.Current.Resources["user"] as UserInfo;
            // List<CalendarDetail> calendarDeails=null;
            //var localSettings = IsolatedStorageSettings.ApplicationSettings;
            //var result = localSettings.Where(a => a.Key == "calendar_detail");
            //if (result.Any())
            //{
            //    calendarDeails = localSettings["calendar_detail"] as List<CalendarDetail>;
            //}


            //List<CalendarDetail> calendarDeails = DataManager.LoadPostSettings("calendar_detail") as List<CalendarDetail>;
            //if (!string.IsNullOrEmpty(task))
            //{
            //    ResultTask(task);
            //}

             cId = NavigationContext.QueryString["cid"];
            string calendarOld = DataManager.LoadPostSettings(cId);
            if (!string.IsNullOrEmpty(calendarOld))
            {
                ResultTask(calendarOld);
            }  

            UserInfo user = UserDataManager.LoadUserSettings();
            if (user != null && string.IsNullOrEmpty(user.Token))
            {
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
           
            CalendarViewModel.CalendarDetail(user.Token,cId, ResultTask);

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
                DataManager.SaveUserInformation(resutlt, cId);
                //resutlt 字符串中的 大小写 及名称和类中的一致
                CalendarDetail calednar = new CalendarDetail();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resutlt));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(calednar.GetType());
                calednar = ser.ReadObject(ms) as CalendarDetail;
                ms.Close();
                ms.Dispose();


                Dispatcher.BeginInvoke(() =>
                {
                    calendarName.Text = calednar.calendar.title;
                    calendarAddress.Text = calednar.calendar.address;
                   // calendarImtes.ItemsSource = calednar.calendars;
                });
            }
        }



    }

    public class CalendarDetail
    {
        private calendar _calendar;

        public calendar calendar
        {
            get { return _calendar; }
            set { _calendar = value; }
        }
    }



    public class calendar
    {
        private string _id;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _title;

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _address;

        public string address
        {
            get { return _address; }
            set { _address = value; }
        }


    }

}