﻿#pragma checksum "..\..\Win_Choose.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ABFD77D91358AF5083F7B495132E59EC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BOBY_Shulack {
    
    
    /// <summary>
    /// Win_Choose
    /// </summary>
    public partial class Win_Choose : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 156 "..\..\Win_Choose.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectangle1;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\Win_Choose.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Close;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\Win_Choose.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox_Game;
        
        #line default
        #line hidden
        
        /// <summary>
        /// tbLog Name Field
        /// </summary>
        
        #line 210 "..\..\Win_Choose.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.TextBox tbLog;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/boby_shulack;component/win_choose.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Win_Choose.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\Win_Choose.xaml"
            ((BOBY_Shulack.Win_Choose)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.win_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.rectangle1 = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 150 "..\..\Win_Choose.xaml"
            this.rectangle1.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Title_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Button_Close = ((System.Windows.Controls.Button)(target));
            
            #line 166 "..\..\Win_Choose.xaml"
            this.Button_Close.Click += new System.Windows.RoutedEventHandler(this.Button_Close_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listBox_Game = ((System.Windows.Controls.ListBox)(target));
            
            #line 182 "..\..\Win_Choose.xaml"
            this.listBox_Game.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.listBox_Game_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 185 "..\..\Win_Choose.xaml"
            this.listBox_Game.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.listBox_Game_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 208 "..\..\Win_Choose.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Start_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbLog = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

