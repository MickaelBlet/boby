﻿#pragma checksum "..\..\..\_Win_Buff\Win_Script.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6BEE28ED61C0AFE5F0CD13242972739A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using BobyMultitools;
using MyControl;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace BobyMultitools {
    
    
    /// <summary>
    /// Win_Script
    /// </summary>
    public partial class Win_Script : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 32 "..\..\..\_Win_Buff\Win_Script.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Setting;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\_Win_Buff\Win_Script.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Close;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\_Win_Buff\Win_Script.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox console;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\_Win_Buff\Win_Script.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lb_Buff;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\_Win_Buff\Win_Script.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add_script;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\_Win_Buff\Win_Script.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button remove_script;
        
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
            System.Uri resourceLocater = new System.Uri("/boby_multitools;component/_win_buff/win_script.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\_Win_Buff\Win_Script.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.bt_Setting = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\_Win_Buff\Win_Script.xaml"
            this.bt_Setting.Click += new System.Windows.RoutedEventHandler(this.bt_Setting_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bt_Close = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\_Win_Buff\Win_Script.xaml"
            this.bt_Close.Click += new System.Windows.RoutedEventHandler(this.bt_Close_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.console = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.lb_Buff = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.add_script = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\_Win_Buff\Win_Script.xaml"
            this.add_script.Click += new System.Windows.RoutedEventHandler(this.add_script_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.remove_script = ((System.Windows.Controls.Button)(target));
            
            #line 118 "..\..\..\_Win_Buff\Win_Script.xaml"
            this.remove_script.Click += new System.Windows.RoutedEventHandler(this.remove_script_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 88 "..\..\..\_Win_Buff\Win_Script.xaml"
            ((System.Windows.Controls.ComboBox)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.tFile_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 89 "..\..\..\_Win_Buff\Win_Script.xaml"
            ((System.Windows.Controls.ComboBox)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.ComboBox_MouseWheel);
            
            #line default
            #line hidden
            
            #line 90 "..\..\..\_Win_Buff\Win_Script.xaml"
            ((System.Windows.Controls.ComboBox)(target)).PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.ComboBox_PreviewMouseWheel);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 99 "..\..\..\_Win_Buff\Win_Script.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_Edit_Click);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 108 "..\..\..\_Win_Buff\Win_Script.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_Play_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

