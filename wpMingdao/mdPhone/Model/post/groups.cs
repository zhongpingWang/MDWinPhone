using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.post
{
    public class groups
    {
        private string _id;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _avstar;

        public string avstar
        {
            get { return _avstar; }
            set { _avstar = value; }
        }

        private string _avatar;

        public string avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }


    }
}
