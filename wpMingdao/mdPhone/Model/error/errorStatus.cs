
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.error
{
    public  class errorStatus
    {
        private static string _errorcode;

        public string error_code
        {
            get { return _errorcode; }
            set { _errorcode = value; }
        }
    }
}
