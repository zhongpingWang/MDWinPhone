using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace mdPhone.Controls
{
    [TemplateVisualState(Name = ListBox2TopPanel.InactiveVisualState, GroupName = ListBox2TopPanel.ActivityVisualStateGroup)]
    [TemplateVisualState(Name = ListBox2TopPanel.PullingDownVisualState, GroupName = ListBox2TopPanel.ActivityVisualStateGroup)]
    [TemplateVisualState(Name = ListBox2TopPanel.ReadyToReleaseVisualState, GroupName = ListBox2TopPanel.ActivityVisualStateGroup)]
    [TemplateVisualState(Name = ListBox2TopPanel.RefreshingVisualState, GroupName = ListBox2TopPanel.ActivityVisualStateGroup)]
    
    public class ListBox2TopPanel : Control
    {
        #region 下拉刷新面板的状态名称
        //滚动中
        private const string ActivityVisualStateGroup = "ActivityStates";
        //无操作
        private const string InactiveVisualState = "Inactive";
        //下拉
        private const string PullingDownVisualState = "PullingDown";
        //松手刷新
        private const string ReadyToReleaseVisualState = "ReadyToRelease";
        //刷新中
        private const string RefreshingVisualState = "Refreshing";

        #endregion

        /// <summary>
        /// 用于绑定下拉文字显示，松手刷新的滚动条
        /// </summary>
        private ScrollViewer targetScrollViewer;

        /// <summary>
        /// 当滚动条下拉到指定程度(PullThreshold)时引发这个事件，并显示松手刷新
        /// 如果开始刷新的时候事件要把IsRefreshing设置为true
        /// 当刷新完成时把IsRefreshing设回false.
        /// </summary>
        public event EventHandler RefreshRequested;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ListBox2TopPanel()
        {
            this.DefaultStyleKey = typeof(ListBox2TopPanel);
            this.LayoutUpdated += this.ListBox2TopPanel_LayoutUpdated;
        }

        #region IsRefreshing DependencyProperty  是否正在刷新

        public static readonly DependencyProperty IsRefreshingProperty = DependencyProperty.Register
        (
            "IsRefreshing", typeof(bool), typeof(ListBox2TopPanel),
            new PropertyMetadata(false, (d, e) => 
            {
                ((ListBox2TopPanel)d).OnIsRefreshingChanged(e); 
            })
        );

        public bool IsRefreshing
        {
            get
            {
                return (bool)this.GetValue(ListBox2TopPanel.IsRefreshingProperty);
            }
            set
            {
                this.SetValue(ListBox2TopPanel.IsRefreshingProperty, value);
            }
        }

        protected void OnIsRefreshingChanged(DependencyPropertyChangedEventArgs e)
        {
            string activityState = (bool)e.NewValue ? ListBox2TopPanel.RefreshingVisualState : ListBox2TopPanel.InactiveVisualState;
            VisualStateManager.GoToState(this, activityState, false);
        }

        #endregion

        #region PullThreshold DependencyProperty 设置下拉距离

        public static readonly DependencyProperty PullThresholdProperty = DependencyProperty.Register(
            "PullThreshold", typeof(double), typeof(ListBox2TopPanel), new PropertyMetadata(70.0));

        /// <summary>
        /// 得到或设置一个滚动条从顶部下拉的最小距离，当达到这个距离松手时会触发刷新事件
        /// 默认距离是70. 推荐最大值不要超过125.
        /// </summary>
        public double PullThreshold
        {
            get
            {
                return (double)this.GetValue(ListBox2TopPanel.PullThresholdProperty);
            }
            set
            {
                this.SetValue(ListBox2TopPanel.PullThresholdProperty, value);
            }
        }

        #endregion

        #region PullDistance DependencyProperty  滚动条拉下的距离

        public static readonly DependencyProperty PullDistanceProperty = DependencyProperty.Register(
            "PullDistance", typeof(double), typeof(ListBox2TopPanel), new PropertyMetadata(0.0));

        /// <summary>
        /// 得到滚动条已经拉下的距离。
        /// 用它绑定图形距离
        /// </summary>
        public double PullDistance
        {
            get
            {
                return (double)this.GetValue(ListBox2TopPanel.PullDistanceProperty);
            }
            private set
            {
                this.SetValue(ListBox2TopPanel.PullDistanceProperty, value);
            }
        }

        #endregion  

        #region PullFraction DependencyProperty  相对PullDistance拉下距离分值

        public static readonly DependencyProperty PullFractionProperty = DependencyProperty.Register(
            "PullFraction", typeof(double), typeof(ListBox2TopPanel), new PropertyMetadata(0.0));

        /// <summary>
        /// 得到滚动条从顶部拉下的距离分数。即拉下PullThreshold的几分之几，取值范围（0.0 - 1.0）
        /// </summary>
        public double PullFraction
        {
            get
            {
                return (double)this.GetValue(ListBox2TopPanel.PullFractionProperty);
            }
            private set
            {
                this.SetValue(ListBox2TopPanel.PullFractionProperty, value);
            }
        }

        #endregion

        #region PullingDownTemplate DependencyProperty  提示下拉刷新的模板

        public static readonly DependencyProperty PullingDownTemplateProperty = DependencyProperty.Register(
            "PullingDownTemplate", typeof(DataTemplate), typeof(ListBox2TopPanel), null);

        /// <summary>
        /// Gets or sets a template that is progressively revealed has the ScrollViewer is pulled down.
        /// </summary>
        public DataTemplate PullingDownTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ListBox2TopPanel.PullingDownTemplateProperty);
            }
            set
            {
                this.SetValue(ListBox2TopPanel.PullingDownTemplateProperty, value);
            }
        }

        #endregion

        #region ReadyToReleaseTemplate DependencyProperty  提示松开刷新的模板

        public static readonly DependencyProperty ReadyToReleaseTemplateProperty = DependencyProperty.Register(
            "ReadyToReleaseTemplate", typeof(DataTemplate), typeof(ListBox2TopPanel), null);

        /// <summary>
        /// Gets or sets the template that is displayed after the ScrollViewer is pulled down past
        /// the PullThreshold.
        /// </summary>
        public DataTemplate ReadyToReleaseTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ListBox2TopPanel.ReadyToReleaseTemplateProperty);
            }
            set
            {
                this.SetValue(ListBox2TopPanel.ReadyToReleaseTemplateProperty, value);
            }
        }

        #endregion

        #region RefreshingTemplate DependencyProperty  提示正在刷新的模板

        public static readonly DependencyProperty RefreshingTemplateProperty = DependencyProperty.Register(
            "RefreshingTemplate", typeof(DataTemplate), typeof(ListBox2TopPanel), null);

        /// <summary>
        /// Gets or sets the template that is displayed while the ScrollViewer is refreshing.
        /// </summary>
        public DataTemplate RefreshingTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ListBox2TopPanel.RefreshingTemplateProperty);
            }
            set
            {
                this.SetValue(ListBox2TopPanel.RefreshingTemplateProperty, value);
            }
        }

        #endregion

        #region Initial setup  初始化设置

        private void ListBox2TopPanel_LayoutUpdated(object sender, EventArgs e)
        {
            if (this.targetScrollViewer == null)
            {
                //找到要绑定的目标滚动条
                this.targetScrollViewer = FindVisualElement<ScrollViewer>(VisualTreeHelper.GetParent(this));
                if (this.targetScrollViewer != null)
                {
                    this.targetScrollViewer.MouseMove += this.targetScrollViewer_MouseMove;
                    this.targetScrollViewer.MouseLeftButtonUp += this.targetScrollViewer_MouseLeftButtonUp;

                    //滚得条的类型必须是控件类型
                    if (this.targetScrollViewer.ManipulationMode != ManipulationMode.Control)
                    {
                        throw new InvalidOperationException("ListBox2TopPanel requires the ScrollViewer " +
                            "to have ManipulationMode=Control. (ListBoxes may require re-templating.");
                    }
                }
            }
        }

        #endregion

        #region Pull-down detection  下拉检测

        /// <summary>
        /// 当滚动条被拖拽时，显示动画和对应的控件模型
        /// </summary>
        private void targetScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            UIElement scrollContent = (UIElement)this.targetScrollViewer.Content;
            CompositeTransform ct = scrollContent.RenderTransform as CompositeTransform;
            if (ct != null && !this.IsRefreshing)
            {
                string activityState;
                if (ct.TranslateY > this.PullThreshold)
                {
                    this.PullDistance = ct.TranslateY;
                    this.PullFraction = 1.0;
                    activityState = ListBox2TopPanel.ReadyToReleaseVisualState;
                }
                else if (ct.TranslateY > 0)
                {
                    this.PullDistance = ct.TranslateY;
                    double threshold = this.PullThreshold;
                    this.PullFraction = threshold == 0.0 ? 1.0 : Math.Min(1.0, ct.TranslateY / threshold);
                    activityState = ListBox2TopPanel.PullingDownVisualState;
                }
                else
                {
                    this.PullDistance = 0;
                    this.PullFraction = 0;
                    activityState = ListBox2TopPanel.InactiveVisualState;
                }
                VisualStateManager.GoToState(this, activityState, false);
            }
        }

        /// <summary>
        /// 当松手时检查是否要刷新
        /// </summary>
        private void targetScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UIElement scrollContent = (UIElement)this.targetScrollViewer.Content;
            CompositeTransform ct = scrollContent.RenderTransform as CompositeTransform;
            if (ct != null && !this.IsRefreshing)
            {
                VisualStateManager.GoToState(this, ListBox2TopPanel.InactiveVisualState, false);
                this.PullDistance = 0;
                this.PullFraction = 0;

                if (ct.TranslateY >= this.PullThreshold)
                {
                    EventHandler handler = this.RefreshRequested;
                    if (handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                }
            }
        }

        #endregion

        #region Utility methods  查找第一个符合要求的元素方法

        /// <summary>
        /// 查找容器内所有元素，并返回第一个是T类型的元素
        /// </summary>
        /// <typeparam name="T">要搜索的元素类型</typeparam>
        /// <param name="initial">要搜索的容器</param>
        /// <returns>返回要找的元素，没找到返回null</returns>
        private static T FindVisualElement<T>(DependencyObject container) where T : DependencyObject
        {
            Queue<DependencyObject> childQueue = new Queue<DependencyObject>();
            childQueue.Enqueue(container);

            while (childQueue.Count > 0)
            {
                DependencyObject current = childQueue.Dequeue();

               // System.Diagnostics.Debug.WriteLine(current.GetType().ToString());

                T result = current as T;
                if (result != null && result != container)
                {
                    return result;
                }

                int childCount = VisualTreeHelper.GetChildrenCount(current);
                for (int childIndex = 0; childIndex < childCount; childIndex++)
                {
                    childQueue.Enqueue(VisualTreeHelper.GetChild(current, childIndex));
                }
            }

            return null;
        }

        #endregion
    }
}
