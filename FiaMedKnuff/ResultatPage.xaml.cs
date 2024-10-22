using FiaMedKnuff.FiaGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FiaMedKnuff
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultatPage : Page
    {

        public static TeamColor WinningTeam = TeamColor.None;

        public ResultatPage()
        {
            this.InitializeComponent();

            string assetName = "Pawn.png";
            string name = "No one";
            switch (WinningTeam) {
                case TeamColor.Red:
                    assetName = "RedPawn";
                    name = "Rött lag";
                    break;
                case TeamColor.Yellow:
                    assetName = "YellowPawn";
                    name = "Gult lag";
                    break;
                case TeamColor.Green:
                    assetName = "GreenPawn";
                    name = "Grönt lag";
                    break;
                case TeamColor.Blue:
                    assetName = "BluePawn";
                    name = "Blått lag";
                    break;
            }

            PawnImage.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Pawns/{assetName}.png"));
            WinningText.Text = $"{name} har vunnit spelet!";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
            
        }

        
    }
}
