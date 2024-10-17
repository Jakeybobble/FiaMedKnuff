using FiaMedKnuff.FiaGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FiaMedKnuff {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class GamePage : Page {
        
        public GamePage() {
            this.InitializeComponent();
            changeOutputText = DieTextBlock;
            dieDecisionPopup = DieResultDecisionPopup;
            position1Btn = Position1Option;
            redTurnIndicator = RedBorder;
            yellowTurnIndicator = YellowBorder;
            greenTurnIndicator = GreenBorder;
            blueTurnIndicator = BlueBorder;
        }

        public static TextBlock changeOutputText;
        public static Popup dieDecisionPopup;
        public static Button position1Btn;
        public static Pawn stander;

        public static Border redTurnIndicator;
        public static Border yellowTurnIndicator;
        public static Border greenTurnIndicator;
        public static Border blueTurnIndicator;

        private void DieButton_Click(object sender, RoutedEventArgs e)
        {            
            if (GameManager.CurrentGame.CurrentGameState == Game.GameState.PreRoll)
            {
                int dieThrow= GameEvents.OnDieClicked();
               
                DieTextBlock.Text = $"Du rullade en {dieThrow}:a!";

                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri($@"ms-appx:///Assets/Die/Die{dieThrow}.png"));

                DieButton.Background = img;

                GameManager.CurrentDieNumber = dieThrow;                                
                TileControl.Selectable = true;

                GameManager.CurrentGame.CurrentGameState = Game.GameState.PostRoll;

            }
            else if (GameManager.CurrentGame.CurrentGameState == Game.GameState.PostRoll)
            {
                DieTextBlock.Text = $"Det är inte din tur.";                
            }      
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            // TBD Link to Settings Page
        }

        private void NyttSpelBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGameOptionsPopup.IsOpen = true;
        }

        /// <summary>
        ///  Closes all PopUps in the GamePage view when CloseBtn is clicked, or an option to move was chosen.
        /// </summary>      
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender == NewGameCloseBtn)
            {
                NewGameOptionsPopup.IsOpen = false;
            }
            else if (sender == RulesCloseBtn)
            {
                RulesPopup.IsOpen = false;
            }           
            else if (sender == Position1Option || sender == Position6Option)
            {
                DieResultDecisionPopup.IsOpen = false;
            }

        }

        /// <summary>
        /// When user clicks outside of the Popup, it is closed.
        /// </summary>
        private void PopupOutside_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender == NewGameOptionsPopupGrid)
            {
                NewGameOptionsPopup.IsOpen = false;
            }
            else if (sender == RulesPopupGrid)
            {
                RulesPopup.IsOpen = false;
            }
        }

        /// <summary>
        /// When user clicks inside of the Popup, it stays open. Unless the Closebtn was pressed.  
        /// </summary>
        private void PopupInside_Tapped(object sender, TappedRoutedEventArgs e)
        {           
            e.Handled = true;     
        }


        /// <summary>
        /// Clicking "Starta" navigates the user to the gameboard, and starts a new game.
        /// </summary>
        /// <param name="sender">Starta button</param>

        private void StartaBtn_Click(object sender, RoutedEventArgs e)
        {            
            GameManager.Init();
            Frame.Navigate(typeof(GamePage));
        }

        private void RulesBtn_Click(object sender, RoutedEventArgs e)
        {
            RulesText.Text = "Det slumpas vilken spelare som får börja att kasta tärning.\r\n" +
                "Spelarna går runt planen medsols, turordningen går också medsols.\r\n\n" +
                "För att kunna gå ut på spelplanen måste du få en 1:a eller 6:a på tärningen.\r\n" +
                "Då hamnar du på din cirkels första position. (Markerat med din färg.)\r\n\n" +
                "Varje gång en spelare får en 6:a på tärningen får det två följder:\r\n" +
                "   Spelaren får välja vilken position den vill att sin pjäs ska flyttas till, \r\n" +
                "   antingen 1:a eller 6:e cirkeln från startpositionen.\r\n" +
                "   Spelaren får ett extra tärningskast att använda, och kan därefter välja \r\n" +
                "   vilken spelpjäs som ska flyttas.\r\n\n" +
                "Spelare kan knuffa ut sina motståndares pjäser genom att flytta sin pjäs \r\n" +
                "till en position som är upptagen redan. Pjäsen som blir knuffad \r\n" +
                "hamnar då tillbaka i sin färgcirkel. \r\n\n" +
                "Spelare kan inte ha två egna pjäser på en cirkel.\r\n\n" +
                "Spelare kan inte gå om eller flytta förbi sina egna pjäser. Om spelarens pjäs\r\n" +
                "hamnar på samma position som en av sina andra pjäser så hamnar \r\n" +
                "de bakom varandra.\r\n\n" +
                "När spelaren gått runt ett yttervarv, så börjar pjäsen gå in mot mitten \r\n" +
                "av brädet markerat med spelarens färg. För att gå ut med pjäsen måste spelarens \r\n" +
                "tärningskast matcha antalet steg in till mittcirkeln. Visar tärningen för många \r\n" +
                "steg måste spelaren gå tillbaka överskjutande antal steg.\r\n\n" +
                "Den spelare som går ut med alla sina pjäser först vinner.\r\n";
            
            RulesPopup.IsOpen = true;
        }

        /// <summary>
        /// Makes it possible to change the DieTextBlock across the project. 
        /// </summary>
        /// <param name="text"> Sets the content of DieTextBlock</param>

        public static void ChangeOutputTextBox(string text)
        {
            GamePage.changeOutputText.Text = text;
        }

        /// <summary>
        /// Makes it possible to open the Popup across the project.
        /// </summary>
        public static void DieDecisionPopUp()
        {
            GamePage.dieDecisionPopup.IsOpen = true;
        }

        
        /// <summary>
        /// When user rolls a 6 on the die, they are presented with the option to move pawn to position 1 or 6.
        /// </summary>
        /// <param name="sender"> The Button for moving to cirkle 1 was clicked.</param>
        
        private void Position1Option_Click(object sender, RoutedEventArgs e)
        {
            stander.MoveInPath(0);
            CloseBtn_Click(sender, e);
        }

        /// <summary>
        /// When user rolls a 6 on the die, they are presented with the option to move pawn to position 1 or 6.
        /// </summary>
        /// <param name="sender"> The Button for moving to cirkle 6 was clicked.</param>
        private void Position6Option_Click(object sender, RoutedEventArgs e)
        {
            stander.MoveInPath(5);
            CloseBtn_Click(sender, e);
        }
    }
}
