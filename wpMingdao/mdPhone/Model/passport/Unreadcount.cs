using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.passport
{
    /// <summary>
    /// 获取当前登录用户的各种未读消息数量
    /// </summary>
   public class Unreadcount
    {

        private string _updated;

        public string updated
        {
            get { return _updated; }
            set { _updated = value; }
        }


        private string _atme;

        public string atme
        {
            get { return _atme; }
            set { _atme = value; }
        }

        private string _message;

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _replyme;

        public string replyme
        {
            get { return _replyme; }
            set { _replyme = value; }
        }


        private string _sysmsg;

        public string sysmsg
        {
            get { return _sysmsg; }
            set { _sysmsg = value; }
        }

        private string _task;

        public string task
        {
            get { return _task; }
            set { _task = value; }
        }


    }
}
