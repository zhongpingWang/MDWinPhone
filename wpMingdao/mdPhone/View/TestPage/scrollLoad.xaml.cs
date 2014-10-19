using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Threading;
using System.Collections.ObjectModel;

namespace mdPhone.View.TestPage
{
    public partial class scrollLoad : PhoneApplicationPage
    {

        private Timer refreshTimer;
        private Timer loadTimer;
        private ObservableCollection<string> DemoData;
        private int index = 0;

        public scrollLoad()
        {
            InitializeComponent();
            DemoData = new ObservableCollection<string>();
            AddString(0, 12);
            this.testListBox.ItemsSource = this.DemoData;
        }

        private void AddString(int statnum, int num)
        {
            string buf = "列表项";
            for (int i = 0; i < num; i++)
            {
                DemoData.Add(buf + ++index);
            }
        }




        private void Refresh()
        {
            DemoData.Clear();
            index = 0;
            string buf = "刷新项";
            for (int i = 1; i <= 12; i++)
            {
                DemoData.Add(buf + i);
            }
        }

        //下拉刷新响应事件
        private void testListBox_RefreshRequested(object sender, EventArgs e)
        {
            //如果底部没有在加载，则响应下拉刷新
            if (!this.testListBox.IsLoading)
            {
                this.testListBox.IsRefreshing = true;
                this.refreshTimer = new Timer(
                    delegate
                    {
                        this.Dispatcher.BeginInvoke(
                            delegate
                            {
                                Refresh();
                                this.testListBox.ItemsSource = DemoData;
                                this.testListBox.IsRefreshing = false;
                                MessageBox.Show("刷新成功");
                            });
                    },
                    null, 3000, -1);
            }
        }

        //上拉加载响应事件
        private void testListBox_LoadRequested(object sender, EventArgs e)
        {
            //如果顶部没有在刷新，则响应上拉加载
            if (!this.testListBox.IsRefreshing)
            {
                this.testListBox.IsLoading = true;
                this.loadTimer = new Timer(
                    delegate
                    {
                        this.Dispatcher.BeginInvoke(
                            delegate
                            {
                                AddString(index, 10);
                                this.testListBox.ItemsSource = DemoData;
                                this.testListBox.IsLoading = false;
                                MessageBox.Show("加载完成");
                            });
                    },
                    null, 3000, -1);
            }
        }


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


    }
}