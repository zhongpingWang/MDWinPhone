﻿#pragma checksum "F:\Project\WP8\MDWinphone\wpMingdao\mdPhone\View\post\Post.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A97F77F1105046B509D7047CEAE6A7F6"
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


namespace mdPhone.View {
    
    
    public partial class Post : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard AnimateTextSB;
        
        internal System.Windows.Media.Animation.DoubleAnimation FontAnimation;
        
        internal System.Windows.Media.Animation.ColorAnimation FontColorAnimation;
        
        internal System.Windows.Media.Animation.Storyboard AnimateSqueezeTextSB;
        
        internal System.Windows.Media.Animation.DoubleAnimation FontAnimationSqueeze;
        
        internal System.Windows.Media.Animation.ColorAnimation FontColorAnimationSqueeze;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ProgressBar getNew;
        
        internal System.Windows.Controls.ProgressBar fristLoad;
        
        internal System.Windows.Controls.TextBlock title;
        
        internal System.Windows.Controls.ListBox postListBox;
        
        internal System.Windows.Controls.Button loadMore;
        
        internal System.Windows.Controls.Image allMsgImg;
        
        internal System.Windows.Controls.Image sysMsgImg;
        
        internal System.Windows.Controls.Image atMeImg;
        
        internal System.Windows.Controls.Image replyMeImg;
        
        internal System.Windows.Controls.Image refresh;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/mdPhone;component/View/post/Post.xaml", System.UriKind.Relative));
            this.AnimateTextSB = ((System.Windows.Media.Animation.Storyboard)(this.FindName("AnimateTextSB")));
            this.FontAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("FontAnimation")));
            this.FontColorAnimation = ((System.Windows.Media.Animation.ColorAnimation)(this.FindName("FontColorAnimation")));
            this.AnimateSqueezeTextSB = ((System.Windows.Media.Animation.Storyboard)(this.FindName("AnimateSqueezeTextSB")));
            this.FontAnimationSqueeze = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("FontAnimationSqueeze")));
            this.FontColorAnimationSqueeze = ((System.Windows.Media.Animation.ColorAnimation)(this.FindName("FontColorAnimationSqueeze")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.getNew = ((System.Windows.Controls.ProgressBar)(this.FindName("getNew")));
            this.fristLoad = ((System.Windows.Controls.ProgressBar)(this.FindName("fristLoad")));
            this.title = ((System.Windows.Controls.TextBlock)(this.FindName("title")));
            this.postListBox = ((System.Windows.Controls.ListBox)(this.FindName("postListBox")));
            this.loadMore = ((System.Windows.Controls.Button)(this.FindName("loadMore")));
            this.allMsgImg = ((System.Windows.Controls.Image)(this.FindName("allMsgImg")));
            this.sysMsgImg = ((System.Windows.Controls.Image)(this.FindName("sysMsgImg")));
            this.atMeImg = ((System.Windows.Controls.Image)(this.FindName("atMeImg")));
            this.replyMeImg = ((System.Windows.Controls.Image)(this.FindName("replyMeImg")));
            this.refresh = ((System.Windows.Controls.Image)(this.FindName("refresh")));
        }
    }
}

