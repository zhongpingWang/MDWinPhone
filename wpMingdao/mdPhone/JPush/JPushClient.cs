using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.JPush
{
    public  class JPushClient
    {
        JPush.JPushSDK jpushSDK = new JPushSDK();

        public string SendMessage() {

           var c= jpushSDK.AppKey;
           return c.ToString();




        
        } 
    }
}
