using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace mdPhone.Controls
{
    [TemplateVisualState(Name = ListBox2BottomPanel.InactiveVisualState, GroupName = ListBox2BottomPanel.ActivityVisualStateGroup)]
    [TemplateVisualState(Name = ListBox2BottomPanel.PullingUpVisualState, GroupName = ListBox2BottomPanel.ActivityVisualStateGroup)]
    [TemplateVisualState(Name = ListBox2BottomPanel.ReadyToReleaseVisualState, GroupName = ListBox2BottomPanel.ActivityVisualStateGroup)]
    [TemplateVisualState(Name = ListBox2BottomPanel.LoadingVisualState, GroupName = ListBox2BottomPanel.ActivityVisualStateGroup)]
    
    public class ListBox2BottomPanel : Control
    {
        #region 上拉加载面板的状态名称
        //滚动中
        private const string ActivityVisualStateGroup = "ActivityStates";
        //无操作
        private const string InactiveVisualState = "Inactive";
        //上拉
        private const string PullingUpVisualState = "PullingUp";
        //松手加载
        private const string ReadyToReleaseVisualState = "ReadyToRelease";
        //加载中
        private const string LoadingVisualState = "Loading";

        #endregion

        /// <summary>
        /// 用于绑定上拉文字显示，松手加载的滚动条
        /// </summary>
        private ScrollViewer targetScrollViewer;

        /// <summary>
        /// 当滚动条上拉到指定程度(PullThreshold)时引发这个事件，并显示松手加载
        /// 如果开始加载的时候事件要把IsLoading设置为true
        /// 当加载完成时把IsLoading设回false.
        /// </summary>
        public event EventHandler LoadRequested;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ListBox2BottomPanel()
        {
            this.DefaultStyleKey = typeof(ListBox2BottomPanel);
            this.LayoutUpdated += this.ListBox2BottomPanel_LayoutUpdated;
        }

        #region IsLoading DependencyProperty  是否正在加载

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(ListBox2BottomPanel), new PropertyMetadata(false, (d, e) => { ((ListBox2BottomPanel)d).OnIsLoadingChanged(e); }));
        public bool IsLoading
        {
            get
            {
                return (bool)this.GetValue(ListBox2BottomPanel.IsLoadingProperty);
            }
            set
            {
                this.SetValue(ListBox2BottomPanel.IsLoadingProperty, value);
            }
        }

        protected void OnIsLoadingChanged(DependencyPropertyChangedEventArgs e)
        {
            string activityState = (bool)e.NewValue ? ListBox2BottomPanel.LoadingVisualState : ListBox2BottomPanel.InactiveVisualState;
            VisualStateManager.GoToState(this, activityState, false);
        }

        #endregion

        #region PullThreshold DependencyProperty 设置上拉距离

        public static readonly DependencyProperty PullThresholdProperty = DependencyProperty.Register(
            "PullThreshold", typeof(double), typeof(ListBox2BottomPanel), new PropertyMetadata(80.0));

        /// <summary>
        /// 得到或设置一个滚动条从顶部下拉的最小距离，当达到这个距离松手时会触发加载事件
        /// 默认距离是80. 推荐最大值不要超过125.
        /// </summary>
        public double PullThreshold
        {
            get
            {
                return (double)this.GetValue(ListBox2BottomPanel.PullThresholdProperty);
            }
            set
            {
                this.SetValue(ListBox2BottomPanel.PullThresholdProperty, value);
            }
        }

        #endregion

        #region PullDistance DependencyProperty  滚动条上拉的距离

        public static readonly DependencyProperty PullDistanceProperty = DependencyProperty.Register(
            "PullDistance", typeof(double), typeof(ListBox2BottomPanel), new PropertyMetadata(0.0));

        /// <summary>
        /// 得到实际滚动条已经上拉的距离。
        /// 用它绑定图形距离
        /// </summary>
        public double PullDistance
        {
            get
            {
                return (double)this.GetValue(ListBox2BottomPanel.PullDistanceProperty);
            }
            private set
            {
                this.SetValue(ListBox2BottomPanel.PullDistanceProperty, value);
            }
        }

        #endregion  

        #region PullFraction DependencyProperty  相对PullDistance上拉距离分值

        public static readonly DependencyProperty PullFractionProperty = DependencyProperty.Register(
            "PullFraction", typeof(double), typeof(ListBox2BottomPanel), new PropertyMetadata(0.0));

        /// <summary>
        /// 得到滚动条从底部上拉的距离分数。即上拉PullThreshold的几分之几，取值范围（0.0 - 1.0）
        /// </summary>
        public double PullFraction
        {
            get
            {
                return (double)this.GetValue(ListBox2BottomPanel.PullFractionProperty);
            }
            private set
            {
                this.SetValue(ListBox2BottomPanel.PullFractionProperty, value);
            }
        }

        #endregion

        #region PullingUpTemplate DependencyProperty  提示上拉加载的模板

        public static readonly DependencyProperty PullingUpTemplateProperty = DependencyProperty.Register(
            "PullingUpTemplate", typeof(DataTemplate), typeof(ListBox2BottomPanel), null);

        /// <summary>
        /// Gets or sets a template that is progressively revealed has the ScrollViewer is pulled up.
        /// </summary>
        public DataTemplate PullingUpTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ListBox2BottomPanel.PullingUpTemplateProperty);
            }
            set
            {
                this.SetValue(ListBox2BottomPanel.PullingUpTemplateProperty, value);
            }
        }

        #endregion

        #region ReadyToReleaseTemplate DependencyProperty  提示松开加载的模板

        public static readonly DependencyProperty ReadyToReleaseTemplateProperty = DependencyProperty.Register(
            "ReadyToReleaseTemplate", typeof(DataTemplate), typeof(ListBox2BottomPanel), null);

        /// <summary>
        /// Gets or sets the template that is displayed after the ScrollViewer is pulled up past the PullThreshold.
        /// </summary>
        public DataTemplate ReadyToReleaseTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ListBox2BottomPanel.ReadyToReleaseTemplateProperty);
            }
            set
            {
                this.SetValue(ListBox2BottomPanel.ReadyToReleaseTemplateProperty, value);
            }
        }

        #endregion

        #region LoadingTemplate DependencyProperty  提示正在加载的模板

        public static readonly DependencyProperty LoadingTemplateProperty = DependencyProperty.Register(
            "LoadingTemplate", typeof(DataTemplate), typeof(ListBox2BottomPanel), null);

        /// <summary>
        /// Gets or sets the template that is displayed while the ScrollViewer is loading.
        /// </summary>
        public DataTemplate LoadingTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ListBox2BottomPanel.LoadingTemplateProperty);
            }
            set
            {
                this.SetValue(ListBox2BottomPanel.LoadingTemplateProperty, value);
            }
        }

        #endregion

        #region Initial setup  初始化设置

        private void ListBox2BottomPanel_LayoutUpdated(object sender, EventArgs e)
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
                        throw new InvalidOperationException("ListBox2BottomPanel requires the ScrollViewer " +
                            "to have ManipulationMode=Control. (ListBoxes may require re-templating.");
                    }
                }
            }
        }

        #endregion

        #region Pull-Up detection  上拉检测

        /// <summary>
        /// 当滚动条被拖拽时，显示动画和对应的控件模型
        /// </summary>
        private void targetScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            UIElement scrollContent = (UIElement)this.targetScrollViewer.Content;
            CompositeTransform ct = scrollContent.RenderTransform as CompositeTransform;
            if (ct != null && !this.IsLoading)
            {
                string activityState;
                if (-ct.TranslateY > this.PullThreshold)
                {
                    this.PullDistance = -ct.TranslateY;
                    this.PullFraction = 1.0;
                    activityState = ListBox2BottomPanel.ReadyToReleaseVisualState;
                }
                else if (-ct.TranslateY > 0)
                {
                    this.PullDistance = -ct.TranslateY;
                    double threshold = this.PullThreshold;
                    this.PullFraction = threshold == 0.0 ? 1.0 : Math.Min(1.0, -ct.TranslateY / threshold);
                    activityState = ListBox2BottomPanel.PullingUpVisualState;
                }
                else
                {
                    this.PullDistance = 0;
                    this.PullFraction = 0;
                    activityState = ListBox2BottomPanel.InactiveVisualState;
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
            if (ct != null && !this.IsLoading)
            {
                VisualStateManager.GoToState(this, ListBox2BottomPanel.InactiveVisualState, false);
                this.PullDistance = 0;
                this.PullFraction = 0;

                if (-ct.TranslateY >= this.PullThreshold)
                {
                    EventHandler handler = this.LoadRequested;
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

                System.Diagnostics.Debug.WriteLine(current.GetType().ToString());

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
