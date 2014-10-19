using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.post
{
    public class Posts
    {
        private List<posts> _posts;

        public List<posts> posts
        {
            get { return _posts; }
            set { _posts = value; }
        }

        private string _more;

        public string more
        {
            get { return _more; }
            set { _more = value; }
        }

        private string _sincepostid;

        public string sincepostid
        {
            get { return _sincepostid; }
            set { _sincepostid = value; }
        }

    }
}
