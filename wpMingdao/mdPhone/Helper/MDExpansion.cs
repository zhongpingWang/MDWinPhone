using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Helper
{
    public static class MDExpansion
    {
        /// <summary>
        /// 特殊字符 替换
        /// </summary>
        /// <param name="_this">原字符串</param>
        /// <returns>返回字符串</returns>
        public static string QueryToken(this string _this)
        {
            if (string.IsNullOrEmpty(_this))
            {
                return string.Empty;
            }
            else
            {
                return _this + "?access_token="+App.Token;
            }
        } 


    }
}
