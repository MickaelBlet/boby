﻿#pragma checksum "..\..\Win_Viewer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D0C636959DD2AE03A74A64A939EAAC1"
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
    /// Win_Viewer
    /// </summary>
    public partial class Win_Viewer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 74 "..\..\Win_Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Player_Lvl;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\Win_Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Player_Name;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\Win_Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Player_Cube;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\Win_Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar HP_Bar;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\Win_Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar MP_Bar;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\Win_Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectangle1;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\Win_Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Minimise;
        
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
            System.Uri resourceLocater = new System.Uri("/boby_shulack;component/win_viewer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Win_Viewer.xaml"
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
            
            #line 11 "..\..\Win_Viewer.xaml"
            ((BOBY_Shulack.Win_Viewer)(target)).LocationChanged += new System.EventHandler(this.Window_LocationChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Player_Lvl = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Player_Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Player_Cube = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.HP_Bar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 6:
            this.MP_Bar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 7:
            this.rectangle1 = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 163 "..\..\Win_Viewer.xaml"
            this.rectangle1.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Title_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Button_Minimise = ((System.Windows.Controls.Button)(target));
            
            #line 178 "..\..\Win_Viewer.xaml"
            this.Button_Minimise.Click += new System.Windows.RoutedEventHandler(this.Button_Minimise_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

