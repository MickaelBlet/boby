﻿#pragma checksum "..\..\..\..\Resources\Global_Resources\Windows.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8A596E9AEB14C581B09CB69E8A001955"
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
using boby_multitools.Resources.Global_Resources;


namespace boby_multitools.Resources.Global_Resources {
    
    
    /// <summary>
    /// Windows
    /// </summary>
    public partial class Windows : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\..\Resources\Global_Resources\Windows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border title_bar;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Resources\Global_Resources\Windows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label title;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\Resources\Global_Resources\Windows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label title_double_shadow;
        
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
            System.Uri resourceLocater = new System.Uri("/boby_add_files;component/resources/global_resources/windows.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Resources\Global_Resources\Windows.xaml"
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
            this.title_bar = ((System.Windows.Controls.Border)(target));
            
            #line 45 "..\..\..\..\Resources\Global_Resources\Windows.xaml"
            this.title_bar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.title_bar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.title = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.title_double_shadow = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
