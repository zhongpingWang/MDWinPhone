using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.JPush
{
    public class JPushHttpClient
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


    }
}
