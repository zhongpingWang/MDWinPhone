using mdPhone.Model.calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model
{
    public class calendarList
    {
       private List<calendars> _calendars;

       public List<calendars> calendars
        {
            get { return _calendars; }
            set { _calendars = value; }
        }
    }
}
