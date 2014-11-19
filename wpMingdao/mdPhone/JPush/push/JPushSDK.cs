using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mdPhone.JPush
{
     public class JPushSDK
    {
        public  string HOST_NAME_SSL = "https://api.jpush.cn";
        public  string HOST_NAME = "http://api.jpush.cn:8800";
        public  string PUSH_PATH = "/v2/push";


        private string _appKey; 
        public string AppKey
        {
            get {
                if (_appKey==null)
                {
                  Uri uri = new Uri("config.xml", UriKind.Relative);
                  var configDoc = XDocument.Load(uri.Tostring());
                  var configAppkey =configDoc.Element("JPush").Element("appkey").Value; 

                  if (!string.IsNullOrEmpty(configAppkey.Tostring()))
                  {
                      _appKey = configAppkey.Tostring();
                  }
                }    
                return _appKey;  
            }
            set { _appKey = value; }
        } 


    }
}
