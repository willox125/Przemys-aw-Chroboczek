﻿

#pragma checksum "C:\Users\Przemek\Desktop\Przemysław Chroboczek\Przemysław Chroboczek\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "219DF35E50DDB3D547CD4FC9CA001DA4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Przemysław_Chroboczek
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Maps.MapControl myMap; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel dataField; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel decisionField; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel finishField; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel thanksField; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel directionsField; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock turnByTurnTb; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock totalTimeLabel; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock totalDistanceTb; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock timeLabel; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock estimatedTimeTb; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock lenghtTb; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox fromTb; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox toTb; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton goBarButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton directionsBarButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton mapBarButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton nightButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton landmarksButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///MainPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            myMap = (global::Windows.UI.Xaml.Controls.Maps.MapControl)this.FindName("myMap");
            dataField = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("dataField");
            decisionField = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("decisionField");
            finishField = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("finishField");
            thanksField = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("thanksField");
            directionsField = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("directionsField");
            turnByTurnTb = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("turnByTurnTb");
            totalTimeLabel = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("totalTimeLabel");
            totalDistanceTb = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("totalDistanceTb");
            timeLabel = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("timeLabel");
            estimatedTimeTb = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("estimatedTimeTb");
            lenghtTb = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("lenghtTb");
            fromTb = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("fromTb");
            toTb = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("toTb");
            goBarButton = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("goBarButton");
            directionsBarButton = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("directionsBarButton");
            mapBarButton = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("mapBarButton");
            nightButton = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("nightButton");
            landmarksButton = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("landmarksButton");
        }
    }
}



