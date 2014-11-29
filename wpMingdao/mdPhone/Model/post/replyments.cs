using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.post
{
    public class Replyments
    {
        private List<replyment> _replyments;

        public List<replyment> replyments
        {
            get { return _replyments; }
            set { _replyments = value; }
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



    public class replyment
    {
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
        

        private string _create_time;

        public string create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }

        private user _user;

        public user user
        {
            get { return _user; }
            set { _user = value; }
        }

        private Ref _ref;

        public Ref ref2
        {
            get { return _ref; }
            set { _ref = value; }
        }

       
    }

    public class Details
    {
        private string _thumbnail_pic;

        public string Thumbnail_pic
        {
            get { return _thumbnail_pic; }
            set { _thumbnail_pic = value; }
        }

        private string _middle_pic;

        public string middle_pic
        {
            get { return _middle_pic; }
            set { _middle_pic = value; }
        }

        private string _original_pic;

        public string original_pic
        {
            get { return _original_pic; }
            set { _original_pic = value; }
        }

        private string _original_filename;

        public string original_filename
        {
            get { return _original_filename; }
            set { _original_filename = value; }
        }


        private string _file_type;

        public string file_type
        {
            get { return _file_type; }
            set { _file_type = value; }
        }

        private string _original_file;

        public string original_file
        {
            get { return _original_file; }
            set { _original_file = value; }
        }

        private int _allow_down;

        public int allow_down
        {
            get { return _allow_down; }
            set { _allow_down = value; }
        }

        

    
    }


    public class Ref
    {
        private Post _post;

        public Post post
        {
            get { return _post; }
            set { _post = value; }
        }  
    }

    public class Post
    {
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

        private user _user;

        public user user
        {
            get { return _user; }
            set { _user = value; }
        }

    }

    public class Replyment
    {
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

        private List<Details> _details;

        public List<Details> details
        {
            get { return _details; }
            set { _details = value; }
        }
    }


}
