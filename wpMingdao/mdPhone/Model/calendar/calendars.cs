using mdPhone.Model.task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.calendar
{
    public class calendars
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


        private string _start_time;

        public string start_time
        {
            get { return _start_time; }
            set { _start_time = value; }
        }


        private string _end_time;

        public string end_time
        {
            get { return _end_time; }
            set { _end_time = value; }
        }


        private string _allday;

        public string allday
        {
            get { return _allday; }
            set { _allday = value; }
        }


        private string _private;

        public string private1
        {
            get { return _private; }
            set { _private = value; }
        }

        private string _is_recur;

        public string is_recur
        {
            get { return _is_recur; }
            set { _is_recur = value; }
        }

        private user _user;

        public user user
        {
            get { return _user; }
            set { _user = value; }
        }


    }
}
