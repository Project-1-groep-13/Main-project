﻿#pragma checksum "..\..\..\Game1Player.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "52E908DA2D90A09B034EB0ADD4E346B3FE2A4D8F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using project_p1;


namespace project_p1 {
    
    
    /// <summary>
    /// Game1Player
    /// </summary>
    public partial class Game1Player : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Game1Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MyCanvas;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Game1Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle Player;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Game1Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Pause;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Game1Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Scoretext;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Game1Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Damagetext;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Game1Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Quit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/project p1;component/game1player.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Game1Player.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MyCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 9 "..\..\..\Game1Player.xaml"
            this.MyCanvas.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDown);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\Game1Player.xaml"
            this.MyCanvas.KeyUp += new System.Windows.Input.KeyEventHandler(this.OnKeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Player = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 3:
            this.Pause = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Game1Player.xaml"
            this.Pause.Click += new System.Windows.RoutedEventHandler(this.WhenButtonClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Scoretext = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Damagetext = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Quit = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Game1Player.xaml"
            this.Quit.Click += new System.Windows.RoutedEventHandler(this.Quit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

