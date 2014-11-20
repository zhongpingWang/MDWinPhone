using cn.jpush.api.common;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdPhone.JPush;

namespace cn.jpush.api.push
{
    internal class PushClient:BaseHttpClient
    {  
        private String appKey;
        private String masterSecret;
        private bool enableSSL = false;
        private long timeToLive;
        private bool apnsProduction = false;
        private HashSet<DeviceEnum> devices = new HashSet<DeviceEnum>();
        mdPhone.JPush.JPushSDK jpushSDK = new mdPhone.JPush.JPushSDK();

      
        public PushClient(String masterSecret, long timeToLive, HashSet<DeviceEnum> devices, bool apnsProduction)
        {
            this.appKey = jpushSDK.AppKey;
            this.masterSecret = masterSecret;
            this.timeToLive = timeToLive;
            this.devices = devices;
            this.apnsProduction = apnsProduction;
        }

        public void sendNotification(String notificationContent, NotificationParams notParams, String extras,callbackResult callback)
        {
            if ( !string.IsNullOrEmpty(extras) )
            {
                notParams.NotyfyMsgContent.n_extras = extras;
            } 
            //System.Net.WebUtility.UrlEncode()
            notParams.NotyfyMsgContent.n_content = System.Net.HttpUtility.UrlEncode(notificationContent);
            //notParams.NotyfyMsgContent.n_content = notificationContent;
            sendMessage(notParams, MsgTypeEnum.NOTIFICATIFY, callback);
        }

        public void sendCustomMessage(String msgTitle, String msgContent, CustomMessageParams cParams, String extras,callbackResult callback)
        {
            if (msgTitle != null)
            {
                //cParams.CustomMsgContent.title = msgTitle;
                cParams.CustomMsgContent.title = System.Net.HttpUtility.UrlEncode(msgTitle);
            }
            if ( !string.IsNullOrEmpty(extras) )
            {
                cParams.CustomMsgContent.extras = extras;
            }

            cParams.CustomMsgContent.message = System.Net.HttpUtility.UrlEncode(msgContent);
            sendMessage(cParams, MsgTypeEnum.COUSTOM_MESSAGE, callback);
        }
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }

        private void sendMessage(MessageParams msgParams, MsgTypeEnum msgType,callbackResult callback) 
        {
            msgParams.ApnsProduction = this.apnsProduction ? 1 : 0;
            msgParams.AppKey = this.appKey;
            msgParams.MasterSecret = this.masterSecret;
            if (msgParams.TimeToLive == MessageParams.NO_TIME_TO_LIVE) 
            {
                msgParams.TimeToLive = this.timeToLive;            
            }
            if (this.devices != null)
            {
                foreach (DeviceEnum device in this.devices)
                {
                    msgParams.addPlatform(device);
                }
            }
           sendPush(msgParams, msgType,callback); 
        }

        private void sendPush(MessageParams msgParams, MsgTypeEnum msgType,callbackResult callback) 
        {
             
           
            String url = enableSSL ?jpushSDK.HOST_NAME_SSL : jpushSDK.HOST_NAME;
            url += jpushSDK.PUSH_PATH;
            String pamrams = prase(msgParams, msgType);
            //Console.WriteLine("begin post");
            sendPost(url, null, pamrams,callback);
            //  ResponseResult result = 
            ////Console.WriteLine("end post");

            //MessageResult messResult = new MessageResult();
            //if (result.responseCode == System.Net.HttpStatusCode.OK)
            //{
            //    //Console.WriteLine("responseContent===" + result.responseContent);
            //    messResult = (MessageResult)JsonTool.JsonToObject(result.responseContent, messResult);
            //    String content = result.responseContent;
            //}
            //messResult.ResponseResult = result;

            //return messResult;

        }

        private String prase(MessageParams message, MsgTypeEnum msgType) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(message.SendNo).Append((int)message.ReceiverType).Append(message.ReceiverValue).Append(message.MasterSecret);
            String verificationCode = sb.ToString();
            //Console.WriteLine(verificationCode);
            verificationCode = Md5.getMD5Hash(verificationCode);
            sb.Clear();
            message.setMsgContent();
            String receiverVallue = System.Net.HttpUtility.UrlEncode(message.ReceiverValue);
            sb.Append("sendno=").Append(message.SendNo).Append("&app_key=").Append(message.AppKey).Append("&receiver_type=").Append((int)message.ReceiverType)
                .Append("&receiver_value=").Append(receiverVallue).Append("&verification_code=").Append(verificationCode)
                .Append("&msg_type=").Append((int)msgType).Append("&msg_content=").Append(message.MsgContent).Append("&platform=").Append(message.getPlatform())
                .Append("&apns_production=").Append(message.ApnsProduction);
            if(message.TimeToLive >= 0)
            {
                sb.Append("&time_to_live=").Append(message.TimeToLive);
            }
            if(message.OverrideMsgId != null)
            {
                sb.Append("&override_msg_id=").Append(message.OverrideMsgId);
            }
            Console.WriteLine(sb.ToString());
            //Debug.Print(sb.ToString());
            return sb.ToString();
        }

    }

    enum MsgTypeEnum
    {
        NOTIFICATIFY = 1,
        COUSTOM_MESSAGE =2
    }
}
