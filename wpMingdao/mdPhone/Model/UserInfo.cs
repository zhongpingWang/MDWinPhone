using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model
{
    public class UserInfo
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

        private string _userimage;

        public string userimage
        {
            get { return _userimage; }
            set { _userimage = value; }
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


        private string _userEmail;

        public string UserEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }


        private string _userPwd;

        public string UserPwd
        {
            get { return _userPwd; }
            set { _userPwd = value; }
        }


        private string _token;

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _url;

        public string url
        {
            get { return _url; }
            set { _url = value; }
        }



        private string _applicationimage;

        public string applicationimage
        {
            get { return _applicationimage; }
            set { _applicationimage = value; }
        }



        private string _applicationname;

        public string applicationname
        {
            get { return _applicationname; }
            set { _applicationname = value; }
        }



        private string _username;

        public string username
        {
            get { return _username; }
            set { _username = value; }
        }


        private int _type;

        public int type
        {
            get { return _type; }
            set { _type = value; }
        }



        private string _error;

        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        private string _companyName;

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        private string _projectID;

        public string ProjectID
        {
            get { return _projectID; }
            set { _projectID = value; }
        }


        private string _access_token;

        public string access_token
        {
            get { return _access_token; }
            set { _access_token = value; }
        }


        private string _expires_in;

        public string expires_in
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }


        private string _refresh_token;

        public string refresh_token
        {
            get { return _refresh_token; }
            set { _refresh_token = value; }
        }


        private bool _rememberPwd=false;

        public bool RememberPwd
        {
            get { return _rememberPwd; }
            set { _rememberPwd = value; }
        }

      
    }
}
