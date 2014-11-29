using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace mdPhone.Helper
{
    public class HttpWeb
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0;MSIE 7.0b;MSIE 7.0;MSIE 8.0b;MSIE 8.0;MSIE 9.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";


        public delegate void callbackResponse(IAsyncResult ia);

        public delegate void callbackResult(string result);

        /// <summary>   
        /// 创建POST方式的HTTP请求   
        /// </summary>   
        /// <param name="url">请求的URL</param>   
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>   
        public static void CreatePostHttpResponse(string url, IDictionary<string, string> parameters, callbackResponse callback)
        {
            //判断url不为空
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            //创建请求
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //请求类型
            request.Method = "POST";
            //http 标头
            request.ContentType = "application/x-www-form-urlencoded";
         
            request.UserAgent = DefaultUserAgent;
            //参数
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                //开始请求
                request.BeginGetRequestStream(new AsyncCallback((ia) =>
                {

                    HttpWebRequest httpWebRequest = (HttpWebRequest)ia.AsyncState;

                    using (Stream stream = httpWebRequest.EndGetRequestStream(ia))
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    request.BeginGetResponse(new AsyncCallback(callback), request);

                }), request);
            }
            else
            {
                request.BeginGetResponse(new AsyncCallback(callback), request);
            }
        }

        /// <summary>   
        /// 创建POST方式的HTTP请求   
        /// </summary>   
        /// <param name="url">请求的URL</param>   
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>   
        public static void CreatePostHttpResponse(string url, IDictionary<string, string> parameters, callbackResult callback)
        {
            //判断url不为空
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            //创建请求
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //请求类型
            request.Method = "POST";
            //http 标头
            request.ContentType = "application/x-www-form-urlencoded";

            request.UserAgent = DefaultUserAgent;
            //参数
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    //if (i > 0)
                    if (url.IndexOf("?") > -1)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("?{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());

                //开始请求
                request.BeginGetRequestStream(new AsyncCallback((ia) =>
                {

                    HttpWebRequest httpWebRequest = (HttpWebRequest)ia.AsyncState;

                    using (Stream stream = httpWebRequest.EndGetRequestStream(ia))
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    request.BeginGetResponse((x) => {

                        HttpWebRequest req = x.AsyncState as HttpWebRequest;
                        string responseStr = string.Empty;

                        try
                        {
                            // 获取请求
                            using (HttpWebResponse response = (HttpWebResponse)req.EndGetResponse(x))
                            {
                                // Get the response stream  
                                StreamReader reader = new StreamReader(response.GetResponseStream());
                                responseStr = reader.ReadToEnd(); 
                            }
                            callback(responseStr);
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                       
                    
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


        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">会调</param>
        public static void CreateGetHttpResponse(string url, IDictionary<string, string> parameters, callbackResponse callback)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                   // if (i > 0)
                    if (url.IndexOf("?") > -1)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("?{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                url += buffer.ToString();
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.BeginGetResponse(new AsyncCallback(callback), request);
        }


        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">会调</param>
        public static void CreateGetHttpResponse(string url, IDictionary<string, string> parameters, callbackResult callback)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (url.IndexOf("?")>-1)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                url += buffer.ToString();
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.BeginGetResponse((ia) =>
            {
                string response = null;
                Stream stream = null;
                try
                {
                    //获取异步操作返回的的信息   
                    HttpWebRequest httpWebRequest = (HttpWebRequest)ia.AsyncState;
                    //结束对 Internet 资源的异步请求  
                    WebResponse webResponse = httpWebRequest.EndGetResponse(ia);
                    stream = webResponse.GetResponseStream();
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        response = sr.ReadToEnd();
                    }
                    callback(response);

                }
                catch (Exception)
                {
                    
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                } 
            }, request);
        }
    }
}
