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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FiaMedKnuff
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        int gameBoardValue;

        private void DieButton_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int dieThrow = rnd.Next(1, 7);
            DieTextBlock.Text = $"Du rullade en {dieThrow}:a!";

            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri($@"ms-appx:///Assets/Die/Die{dieThrow}.png"));

            DieButton.Background = img;

            var list = new List<int>(12) { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            gameBoardValue = dieThrow;
            string gameboardString = gameBoardValue.ToString();

            GameboardTextBlock.Text = gameboardString;
        }
    }
}
