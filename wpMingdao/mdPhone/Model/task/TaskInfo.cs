using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.task
{
   public class tasks
    {

        private string _autoid;

        public string autoid
        {
            get { return _autoid; }
            set { _autoid = value; }
        }


        private string _guid;

        public string guid
        {
            get { return _guid; }
            set { _guid = value; }
        }


        private string _title;

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _expire_date;

        public string expire_date
        {
            get { return _expire_date; }
            set { _expire_date = value; }
        }

        private string _finished_date;

        public string finished_date
        {
            get { return _finished_date; }
            set { _finished_date = value; }
        }

        private string _create_time;

        public string create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }


        private string _unread_count;

        public string unread_count
        {
            get { return _unread_count; }
            set { _unread_count = value; }
        }


        private string _sub_count;

        public string sub_count
        {
            get { return _sub_count; }
            set { _sub_count = value; }
        }

        private user _user; 
        public user user
        {
            get { return _user; }
            set { _user = value; }
        }


    }
}
