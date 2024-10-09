using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FiaMedKnuff
{

    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            // TBD Link to Settings Page
        }

        private void NyttSpelBtn_Click(object sender, RoutedEventArgs e)
        {           
            NewGameOptionsPopup.IsOpen = true;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGameOptionsPopup.IsOpen = false;
        }

        private void ClosePopup_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NewGameOptionsPopup.IsOpen = false;
        }
    }
}
