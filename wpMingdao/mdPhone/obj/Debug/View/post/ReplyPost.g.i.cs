﻿#pragma checksum "F:\Project\WP8\MDWinphone\wpMingdao\mdPhone\View\post\ReplyPost.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E1680E5909C3FA94D6A5E59164A6B985"
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


namespace mdPhone.View.post {
    
    
    public partial class ReplyPost : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox postText;
        
        internal System.Windows.Controls.ProgressBar fristLoad;
        
        internal System.Windows.Controls.Image atMeImg;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/mdPhone;component/View/post/ReplyPost.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.postText = ((System.Windows.Controls.TextBox)(this.FindName("postText")));
            this.fristLoad = ((System.Windows.Controls.ProgressBar)(this.FindName("fristLoad")));
            this.atMeImg = ((System.Windows.Controls.Image)(this.FindName("atMeImg")));
        }
    }
}

