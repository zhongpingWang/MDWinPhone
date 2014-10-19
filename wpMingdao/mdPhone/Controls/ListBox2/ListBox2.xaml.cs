using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace mdPhone.Controls
{
    public partial class ListBox2 : UserControl
    {
        public event SelectionChangedEventHandler SelectionChanged;//列表相选中响应事件
        public event EventHandler RefreshRequested;//下拉刷新响应事件
        public event EventHandler LoadRequested;//上拉加载响应事件

        #region 列表的 ItemsSource 属性
        public static readonly DependencyProperty ItemsSourceProperty = 
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ListBox2), new PropertyMetadata(""));
        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)base.GetValue(ItemsSourceProperty);
            }
            set
            {
                base.SetValue(ItemsSourceProperty, value);
            }
        }
        #endregion

        #region 列表的 DataTemplate 属性
        private DataTemplate _ItemTemplate = null;
        public DataTemplate ItemTemplate
        {
            get
            {
                return _ItemTemplate;
            }
            set
            {
                _ItemTemplate = value;
                mainPanel.ItemTemplate = value;
            }
        }
        #endregion

        #region 判断是否正在刷新

        public static readonly DependencyProperty IsRefreshingProperty = 
            DependencyProperty.Register("IsRefreshing", typeof(bool), typeof(ListBox2), new PropertyMetadata(false, (d, e) => ((ListBox2)d).OnIsRefreshingChanged(e)));
        public bool IsRefreshing
        {
            get
            {
                return (bool)this.GetValue(ListBox2.IsRefreshingProperty);
            }
            set
            {
                this.SetValue(ListBox2.IsRefreshingProperty, value);
            }
        }

        protected void OnIsRefreshingChanged(DependencyPropertyChangedEventArgs e)
        {
            this.topPanel.IsRefreshing = (bool)e.NewValue;
        }

        #endregion

        #region 判断是否正在加载

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(ListBox2), new PropertyMetadata(false, (d, e) => ((ListBox2)d).OnIsLoadingChanged(e)));
        public bool IsLoading
        {
            get
            {
                return (bool)this.GetValue(ListBox2.IsLoadingProperty);
            }
            set
            {
                this.SetValue(ListBox2.IsLoadingProperty, value);
            }
        }

        protected void OnIsLoadingChanged(DependencyPropertyChangedEventArgs e)
        {
            this.bottomPanel.IsLoading = (bool)e.NewValue;
        }

        #endregion

        public ListBox2()
        {
            InitializeComponent();
        }

        //下拉刷新响应事件
        private void topPanel_RefreshRequested(object sender, EventArgs e)
        {
            if (RefreshRequested != null)
            {
                RefreshRequested(sender, e);
            }
        }

        //列表相选中响应事件
        private void mainPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender, e);
            }
        }

        //上拉加载响应事件
        private void bottomPanel_LoadRequested(object sender, EventArgs e)
        {
            if (LoadRequested != null)
            {
                LoadRequested(sender, e);
            }
        }
    }

    public class NegativeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(double))
            {
                return -System.Convert.ToDouble(value);
            }
            else if (targetType == typeof(Thickness))
            {
                double doubleValue = -System.Convert.ToDouble(value);
                switch ((string)parameter)
                {
                    case "Left": return new Thickness(doubleValue, 0, 0, 0);
                    case "Top": return new Thickness(0, doubleValue, 0, 0);
                    case "Right": return new Thickness(0, 0, doubleValue, 0);
                    case "Bottom": return new Thickness(0, 0, 0, doubleValue);
                    default: return new Thickness(doubleValue);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
