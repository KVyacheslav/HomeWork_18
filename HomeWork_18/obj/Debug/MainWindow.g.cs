﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B18C630140B16C982BF2BAAF990B26B87AECEE686621F7A812868B35C2A21E9D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using HomeWork_18;
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


namespace HomeWork_18 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvClients;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbName;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbIsVip;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbCountBankAcc;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbCountBankCredits;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvBankAccounts;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvBankCredits;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextMonth;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbDate;
        
        #line default
        #line hidden
        
        
        #line 212 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbClients;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvLogs;
        
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
            System.Uri resourceLocater = new System.Uri("/HomeWork_18;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 15 "..\..\MainWindow.xaml"
            ((HomeWork_18.MainWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 15 "..\..\MainWindow.xaml"
            ((HomeWork_18.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lvClients = ((System.Windows.Controls.ListView)(target));
            
            #line 49 "..\..\MainWindow.xaml"
            this.lvClients.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LvClients_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 57 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowWindowAddClient);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 59 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowWindowEditClient);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 61 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveClient);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 63 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Refresh);
            
            #line default
            #line hidden
            return;
            case 8:
            this.tbName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.tbIsVip = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.tbCountBankAcc = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.tbCountBankCredits = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.lvBankAccounts = ((System.Windows.Controls.ListView)(target));
            return;
            case 13:
            
            #line 119 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBankAccount);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 121 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PutBankAccount);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 123 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TransferToBankAccount);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 125 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TransferToClientBankAccount);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 127 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveBankAccount);
            
            #line default
            #line hidden
            return;
            case 18:
            this.lvBankCredits = ((System.Windows.Controls.ListView)(target));
            return;
            case 19:
            
            #line 167 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBankCredit);
            
            #line default
            #line hidden
            return;
            case 20:
            this.btnNextMonth = ((System.Windows.Controls.Button)(target));
            
            #line 193 "..\..\MainWindow.xaml"
            this.btnNextMonth.Click += new System.Windows.RoutedEventHandler(this.NextMonth);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 199 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            return;
            case 22:
            this.tbDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 23:
            this.cbClients = ((System.Windows.Controls.ComboBox)(target));
            
            #line 217 "..\..\MainWindow.xaml"
            this.cbClients.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CbClients_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 24:
            this.lvLogs = ((System.Windows.Controls.ListView)(target));
            
            #line 229 "..\..\MainWindow.xaml"
            this.lvLogs.SizeChanged += new System.Windows.SizeChangedEventHandler(this.lvLogs_SizeChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
