using cn.jpush.api.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace cn.jpush.api.common
{
    class BaseHttpClient
    {
        private const String CHARSET = "UTF-8";
	    private const String RATE_LIMIT_QUOTA = "X-Rate-Limit-Limit";
	    private const String RATE_LIMIT_Remaining = "X-Rate-Limit-Remaining";
	    private const String RATE_LIMIT_Reset = "X-Rate-Limit-Reset";
	
	    protected const int RESPONSE_OK = 200;
	
	    //设置连接超时时间
	    private const int DEFAULT_CONNECTION_TIMEOUT = (20 * 1000); // milliseconds
	
	    //设置读取超时时间
	    private const int DEFAULT_SOCKET_TIMEOUT = (30 * 1000); // milliseconds

        public void sendPost(String url, String auth, String reqParams, callbackResult callback) 
        {
            this.sendRequest("POST", url, auth, reqParams, callback);        
        }

        public void sendGet(String url, String auth, String reqParams, callbackResult callback)
        {
             this.sendRequest("GET", url, auth, reqParams,callback);
        }

        public delegate void callbackResult(string result);

        public delegate void callbackResponse(IAsyncResult ia);
        /**
         *
         * method "POST" or "GET"
         * url
         * auth   可选
         */
        public void sendRequest(String method, String url, String auth, String reqParams, callbackResult callback)
        {
            //Console.WriteLine("begin send" + reqParams);
            ResponseResult result = new ResponseResult();

            HttpWebRequest request = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers["Charset"] = CHARSET;
                if (!String.IsNullOrEmpty(auth))
                {
                    request.Headers["Authorization"] = "Basic " + auth;
                }
                if (!string.IsNullOrEmpty(reqParams))
                {
                    byte[] data = Encoding.UTF8.GetBytes(reqParams.ToString()); 

                    //开始请求
                    request.BeginGetRequestStream(new AsyncCallback((ia) =>
                    {

                        HttpWebRequest httpWebRequest = (HttpWebRequest)ia.AsyncState;

                        using (Stream stream = httpWebRequest.EndGetRequestStream(ia))
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        request.BeginGetResponse((x) =>
                        {

                            HttpWebRequest req = x.AsyncState as HttpWebRequest;
                            string responseStr = string.Empty;

                            // 获取请求
                            using (HttpWebResponse response = (HttpWebResponse)req.EndGetResponse(x))
                            {
                                // Get the response stream  
                                StreamReader reader = new StreamReader(response.GetResponseStream());
                                responseStr = reader.ReadToEnd();

                            }
                            callback(responseStr);

                        }, request);

                    }), request);

                }
                else
                {

                    request.BeginGetResponse((ia) =>
                    {

                        HttpWebRequest req = ia.AsyncState as HttpWebRequest;
                        string responseStr = string.Empty;

                        // 获取请求
                        using (HttpWebResponse response = (HttpWebResponse)req.EndGetResponse(ia))
                        {
                            // Get the response stream  
                            StreamReader reader = new StreamReader(response.GetResponseStream());
                            responseStr = reader.ReadToEnd();

                        }
                        callback(responseStr);
                    }, request);
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally 
            { 
                 
            }
            
        }
    }
}
