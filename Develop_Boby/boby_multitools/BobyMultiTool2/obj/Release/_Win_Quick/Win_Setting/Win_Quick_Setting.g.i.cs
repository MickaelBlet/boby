﻿#pragma checksum "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FE431E7D31E518B6C02737852C8D696B"
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


namespace BobyMultitools {
    
    
    /// <summary>
    /// Win_Quick_Setting
    /// </summary>
    public partial class Win_Quick_Setting : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 120 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rt_Title;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Close;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button @new;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button open;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox tFile;
        
        #line default
        #line hidden
        
        /// <summary>
        /// dataGrid Name Field
        /// </summary>
        
        #line 161 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.DataGrid dataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/boby_multitools;component/_win_quick/win_setting/win_quick_setting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
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
            this.rt_Title = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 114 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            this.rt_Title.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.rt_Title_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bt_Close = ((System.Windows.Controls.Button)(target));
            
            #line 134 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            this.bt_Close.Click += new System.Windows.RoutedEventHandler(this.bt_Close_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.@new = ((System.Windows.Controls.Button)(target));
            
            #line 143 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            this.@new.Click += new System.Windows.RoutedEventHandler(this.new_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.open = ((System.Windows.Controls.Button)(target));
            
            #line 144 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            this.open.Click += new System.Windows.RoutedEventHandler(this.open_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 145 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.save_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tFile = ((System.Windows.Controls.ComboBox)(target));
            
            #line 153 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            this.tFile.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.tFile_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 154 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            this.tFile.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.tFile_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
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
            case 8:
            
            #line 188 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged_1);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 197 "..\..\..\..\_Win_Quick\Win_Setting\Win_Quick_Setting.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

