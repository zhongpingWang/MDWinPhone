using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace mdPhone.Helper
{
    
   public  class textConvert : IValueConverter
    {
        //值从数据源到绑定目标的过程进行转换   
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //value:绑定的值,也就是要进行转换的值      
            //parameter:对应Binding中的ConverterParameter属性,用来传递参数,  
            //language:对应Binding中的ConverterLanguage属性,用来确定要转换值的语言,一般在全球化应用中使用.     
            return value;
           
        }

        //不常用,用在双向绑定中.当UI元素值改变时,返回到数据源把值转换回来     
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
