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
        private const String HOST_NAME_SSL = "https://api.jpush.cn";
        private const String HOST_NAME = "http://api.jpush.cn:8800";
        private const String PUSH_PATH = "/v2/push";


        private string _appKey; 
        public string AppKey
        {
            get {
                if (_appKey==null)
                {
                  Uri uri = new Uri("config.xml", UriKind.Relative);
                  var configDoc = XDocument.Load(uri.ToString());
                  var configAppkey = from config in configDoc.Element("JPush").Elements("appkey")
                              select new
                              {
                                  value=config.Value
                                 
                              };

                  if (!string.IsNullOrEmpty(configAppkey.ToString()))
                  {
                      _appKey = configAppkey.ToString();
                  }
                }    
                return _appKey;  
            }
            set { _appKey = value; }
        }
        public string masterSecret;
        public bool enableSSL = false;
        public long timeToLive;
        public bool apnsProduction = false;


    }
}
