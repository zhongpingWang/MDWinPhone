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

    public class posts
    {
        private string _id;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _guid;

        public string guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        private string _text;

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        private List<targs> _tags;

        public List<targs> tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        private string _create_time;

        public string create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }

        private string _source;

        public string source
        {
            get { return _source; }
            set { _source = value; }
        }

        private int _reply_count;

        public int reply_count
        {
            get { return _reply_count; }
            set { _reply_count = value; }
        }

        private int _like_count;

        public int like_count
        {
            get { return _like_count; }
            set { _like_count = value; }
        }

        private int _reshared_count;

        public int reshared_count
        {
            get { return _reshared_count; }
            set { _reshared_count = value; }
        }


        private int _favorite_count;

        public int favorite_count
        {
            get { return _favorite_count; }
            set { _favorite_count = value; }
        }

        private int _favorite;

        public int favorite
        {
            get { return _favorite; }
            set { _favorite = value; }
        }

        private int _like;

        public int like
        {
            get { return _like; }
            set { _like = value; }
        }

        private int _type;

        public int type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int _share_type;

        public int share_type
        {
            get { return _share_type; }
            set { _share_type = value; }
        }

        private List<groups> _groups;

        public List<groups> groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        private user _user;
        public user user
        {
            get { return _user; }
            set { _user = value; }
        }




    }

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
