﻿#pragma checksum "..\..\Window_Base.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "25AC799263082AE529CBE870853C9F38"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18408
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


namespace BOBY_quickbar {
    
    
    /// <summary>
    /// Window_Base
    /// </summary>
    public partial class Window_Base : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\Window_Base.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrd;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Window_Base.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton quickbar_dialog_ctrl;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Window_Base.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton quickbar_dialog_alt;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Window_Base.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton quickbar_dialog_extra;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Window_Base.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton quickbar_dialog_sep1;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Window_Base.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton quickbar_dialog_sep2;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Window_Base.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Change;
        
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
            System.Uri resourceLocater = new System.Uri("/BOBY_quickbar_32;component/window_base.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window_Base.xaml"
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
            this.mainGrd = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 10 "..\..\Window_Base.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Open);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 11 "..\..\Window_Base.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Save);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 13 "..\..\Window_Base.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 15 "..\..\Window_Base.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Full_Screen);
            
            #line default
            #line hidden
            return;
            case 6:
            this.quickbar_dialog_ctrl = ((System.Windows.Controls.RadioButton)(target));
            
            #line 25 "..\..\Window_Base.xaml"
            this.quickbar_dialog_ctrl.Checked += new System.Windows.RoutedEventHandler(this.Quickbar_dialog_ctrl_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.quickbar_dialog_alt = ((System.Windows.Controls.RadioButton)(target));
            
            #line 34 "..\..\Window_Base.xaml"
            this.quickbar_dialog_alt.Checked += new System.Windows.RoutedEventHandler(this.Quickbar_dialog_alt_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.quickbar_dialog_extra = ((System.Windows.Controls.RadioButton)(target));
            
            #line 42 "..\..\Window_Base.xaml"
            this.quickbar_dialog_extra.Checked += new System.Windows.RoutedEventHandler(this.Quickbar_dialog_extra_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.quickbar_dialog_sep1 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 50 "..\..\Window_Base.xaml"
            this.quickbar_dialog_sep1.Checked += new System.Windows.RoutedEventHandler(this.Quickbar_dialog_sep1_Checked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.quickbar_dialog_sep2 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 58 "..\..\Window_Base.xaml"
            this.quickbar_dialog_sep2.Checked += new System.Windows.RoutedEventHandler(this.Quickbar_dialog_sep2_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Change = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\Window_Base.xaml"
            this.Change.Click += new System.Windows.RoutedEventHandler(this.Button_Click_Change);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
