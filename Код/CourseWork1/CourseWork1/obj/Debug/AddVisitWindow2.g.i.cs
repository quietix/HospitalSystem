﻿#pragma checksum "..\..\AddVisitWindow2.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "56EC5E7EB2B2BC9DCD971A664160D3808A420DDE4BC6CA99388C492F3B8E834E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWork1;
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


namespace CourseWork1 {
    
    
    /// <summary>
    /// AddVisitWindow2
    /// </summary>
    public partial class AddVisitWindow2 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGrid1;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CB_Patient_1;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CB_Direction_2;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CB_Doctor_3;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Date_1;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Complaints_2;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Diagnosis_3;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_HasSickleave_4;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\AddVisitWindow2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_SickleaveDuration_5;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseWork1;component/addvisitwindow2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddVisitWindow2.xaml"
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
            
            #line 27 "..\..\AddVisitWindow2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Insert_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 28 "..\..\AddVisitWindow2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_SetDafaultValues_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 29 "..\..\AddVisitWindow2.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Back_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CB_Patient_1 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 60 "..\..\AddVisitWindow2.xaml"
            this.CB_Patient_1.DropDownClosed += new System.EventHandler(this.CB_Patient_1_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CB_Direction_2 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\AddVisitWindow2.xaml"
            this.CB_Direction_2.DropDownClosed += new System.EventHandler(this.CB_Direction_2_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CB_Doctor_3 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 62 "..\..\AddVisitWindow2.xaml"
            this.CB_Doctor_3.DropDownClosed += new System.EventHandler(this.CB_Doctor_3_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TB_Date_1 = ((System.Windows.Controls.TextBox)(target));
            
            #line 64 "..\..\AddVisitWindow2.xaml"
            this.TB_Date_1.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.TB_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 64 "..\..\AddVisitWindow2.xaml"
            this.TB_Date_1.LostFocus += new System.Windows.RoutedEventHandler(this.TB_Date_1_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TB_Complaints_2 = ((System.Windows.Controls.TextBox)(target));
            
            #line 65 "..\..\AddVisitWindow2.xaml"
            this.TB_Complaints_2.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.TB_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 65 "..\..\AddVisitWindow2.xaml"
            this.TB_Complaints_2.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_Complaints_2_KeyUp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.TB_Diagnosis_3 = ((System.Windows.Controls.TextBox)(target));
            
            #line 66 "..\..\AddVisitWindow2.xaml"
            this.TB_Diagnosis_3.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.TB_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 66 "..\..\AddVisitWindow2.xaml"
            this.TB_Diagnosis_3.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_Diagnosis_3_KeyUp);
            
            #line default
            #line hidden
            return;
            case 11:
            this.TB_HasSickleave_4 = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\AddVisitWindow2.xaml"
            this.TB_HasSickleave_4.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.TB_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 67 "..\..\AddVisitWindow2.xaml"
            this.TB_HasSickleave_4.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_HasSickleave_4_KeyUp);
            
            #line default
            #line hidden
            
            #line 67 "..\..\AddVisitWindow2.xaml"
            this.TB_HasSickleave_4.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TB_HasSickleave_4_TextChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.TB_SickleaveDuration_5 = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\AddVisitWindow2.xaml"
            this.TB_SickleaveDuration_5.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.TB_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 68 "..\..\AddVisitWindow2.xaml"
            this.TB_SickleaveDuration_5.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_SickleaveDuration_5_KeyUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

