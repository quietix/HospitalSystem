﻿#pragma checksum "..\..\..\DocumentsWin4\SickleavePage1.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "31ED94A1213587936E044967F87018177010B483EB89C4D16796D20D043F7E69"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWork1.DocumentsWin4;
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


namespace CourseWork1.DocumentsWin4 {
    
    
    /// <summary>
    /// SickleavePage1
    /// </summary>
    public partial class SickleavePage1 : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\DocumentsWin4\SickleavePage1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGrid1;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\DocumentsWin4\SickleavePage1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CB_Patients_1;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\DocumentsWin4\SickleavePage1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CB_Visits_2;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseWork1;component/documentswin4/sickleavepage1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DocumentsWin4\SickleavePage1.xaml"
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
            this.DataGrid1 = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.CB_Patients_1 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\DocumentsWin4\SickleavePage1.xaml"
            this.CB_Patients_1.DropDownClosed += new System.EventHandler(this.CB_Patients_1_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CB_Visits_2 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\DocumentsWin4\SickleavePage1.xaml"
            this.CB_Visits_2.DropDownClosed += new System.EventHandler(this.CB_Visits_2_DropDownClosed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

