﻿#pragma checksum "F:\Project\WP8\MDWinphone\wpMingdao\mdPhone\View\user\userInfo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4CEF02A76FDAAC993512ADDFFB8AE994"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace mdPhone.View.user {
    
    
    public partial class userInfo : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ProgressBar loading;
        
        internal System.Windows.Controls.StackPanel userBox;
        
        internal System.Windows.Controls.Image uerImg;
        
        internal System.Windows.Controls.TextBlock userCompanyName;
        
        internal System.Windows.Controls.TextBlock userJoin;
        
        internal System.Windows.Controls.TextBlock userName;
        
        internal System.Windows.Controls.TextBlock userJob;
        
        internal System.Windows.Controls.TextBlock userEmail;
        
        internal System.Windows.Controls.TextBlock userGrade;
        
        internal System.Windows.Controls.TextBlock userMark;
        
        internal System.Windows.Controls.TextBlock userPhone;
        
        internal System.Windows.Controls.TextBlock uerAddress;
        
        internal Microsoft.Phone.Controls.ToggleSwitch togglePush;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/mdPhone;component/View/user/userInfo.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.loading = ((System.Windows.Controls.ProgressBar)(this.FindName("loading")));
            this.userBox = ((System.Windows.Controls.StackPanel)(this.FindName("userBox")));
            this.uerImg = ((System.Windows.Controls.Image)(this.FindName("uerImg")));
            this.userCompanyName = ((System.Windows.Controls.TextBlock)(this.FindName("userCompanyName")));
            this.userJoin = ((System.Windows.Controls.TextBlock)(this.FindName("userJoin")));
            this.userName = ((System.Windows.Controls.TextBlock)(this.FindName("userName")));
            this.userJob = ((System.Windows.Controls.TextBlock)(this.FindName("userJob")));
            this.userEmail = ((System.Windows.Controls.TextBlock)(this.FindName("userEmail")));
            this.userGrade = ((System.Windows.Controls.TextBlock)(this.FindName("userGrade")));
            this.userMark = ((System.Windows.Controls.TextBlock)(this.FindName("userMark")));
            this.userPhone = ((System.Windows.Controls.TextBlock)(this.FindName("userPhone")));
            this.uerAddress = ((System.Windows.Controls.TextBlock)(this.FindName("uerAddress")));
            this.togglePush = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("togglePush")));
        }
    }
}

