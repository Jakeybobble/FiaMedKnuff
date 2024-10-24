using FiaMedKnuff.FiaGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
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

        private async void NyttSpelBtn_Click(object sender, RoutedEventArgs e)
        {

            animateDirectionDown.Begin();

            var element = new MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Sounds");
            StorageFile file = await folder.GetFileAsync("trudelutt.wav");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();

            //det äe extra kod för ljud bak

            {
                animateDirectionDown.Begin();

            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {            
            animateUpAndOut.Begin();
        }
               
        // Clicking "Starta" navigates the user to the gameboard.
        private async void StartaBtn_Click(object sender, RoutedEventArgs e)
        {
         

            var element = new MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\Sounds");
            StorageFile file = await folder.GetFileAsync("bakgrund_musik.mp3");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Volume=0.02;
        
            element.Play();


            GameManager.Init();
            Frame.Navigate(typeof(GamePage));
        }

        private void ParentGrid_SizeChanged(object sender, SizeChangedEventArgs e) {
            NewGameDialog.Width = ParentGrid.ActualWidth;
            NewGameDialog.Height = ParentGrid.ActualHeight;

            CreditsDialog.Width = ParentGrid.ActualWidth;
            CreditsDialog.Height = ParentGrid.ActualHeight;
        }

        private void CreditsBtn_Click(object sender, RoutedEventArgs e) {
            animateCreditsDown.Begin();
        }

        private void CreditsCloseBtn_Click(object sender, RoutedEventArgs e) {
            animateCreditsUp.Begin();
        }
    }
}
