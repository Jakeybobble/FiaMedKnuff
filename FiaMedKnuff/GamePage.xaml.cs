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
using Windows.Storage;
using Windows.Media.Playback;
using Windows.Media.Core;

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
            DieBtnBorder = dieBtnBorder;

            redSlots = RedSlots;
            yellowSlots = YellowSlots;
            greenSlots = GreenSlots;
            blueSlots = BlueSlots;

            rollsCheatBox = RollsCheatBox;

            dieButton = DieButton;

            RollDiePopupElement = RollDiePopup;
            
        }
        private DispatcherTimer timer;

        public static TextBlock changeOutputText;
        public static Popup dieDecisionPopup;
        public static Button position1Btn;
        public static Border DieBtnBorder;
        public static Pawn stander;

        public static Border redTurnIndicator;
        public static Border yellowTurnIndicator;
        public static Border greenTurnIndicator;
        public static Border blueTurnIndicator;

        private static StackPanel redSlots;
        private static StackPanel yellowSlots;
        private static StackPanel greenSlots;
        private static StackPanel blueSlots;

        private static TextBox rollsCheatBox;

        public static bool DieIsRollable = true;

        private static Button dieButton;

        public static Popup RollDiePopupElement;

        
        /*
        private void DieButton_Click(object sender, RoutedEventArgs e)
        {
            RollDiePopup.IsOpen = true;

            // Timer to auto close DieRollAnimation.
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.9); // 2 seconds delay
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        */

        /// <summary>
        /// Closes the RollDiePopup, stops the timer, and gives die result outputs to user.
        /// </summary>
        private void Timer_Tick(object sender, object e)
        {
            RollDiePopup.IsOpen = false;

            // Stops the timer to prevent multiple ticks.
            timer.Stop();

            //int dieThrow = GameEvents.OnDieClicked();
            // Sets the image of the die.
          
        }
      
        /// <summary>
        /// Opens the RollDiePopup and handles the die roll animation.
        /// </summary>
        private async void DieButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DieIsRollable) return;

            await GameEvents.OnDieClicked();

        }

        public static void SetDie(int dieThrow) {
            if (dieThrow != -1) {
                // Sets the image of the die to match the result rolled.
                GamePage.changeOutputText.Text = $"Du rullade en {dieThrow}:a!";
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri($@"ms-appx:///Assets/Die/Die{dieThrow}.png"));
                dieButton.Background = img;
            }
        }


        /// <summary>
        /// Gives the Glow Effect on the Die when it needs to be pressed by the user.
        /// </summary>
        public static void GlowEffectDie()
        {
            DieBtnBorder.Background = new SolidColorBrush(Windows.UI.Colors.White);
            DieBtnBorder.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Yellow);

        }

        /// <summary>
        /// Ends the Glow Effect after the die has been pressed.
        /// </summary>
        public static void EndGlowEffectDie()
        {            
            DieBtnBorder.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
            DieBtnBorder.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Orange);
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            // TBD Link to Settings Page
        }

        /// <summary>
        /// Starts the animation which brings the NewGameOptions into view.
        /// </summary>
        private void NyttSpelBtn_Click(object sender, RoutedEventArgs e)
        {            
            animateDirectionDown.Begin();
        }

        /// <summary>
        ///  Closes all PopUps in the GamePage view when CloseBtn is clicked, or an option was clicked in DieResultDecision.
        /// </summary>      
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender == NewGameCloseBtn)
            {
                animateUpAndOut.Begin();
            }
            else if (sender == RulesCloseBtn)
            {
                animateUpAndOutRules.Begin();
            }
            else if (sender == Position1Option || sender == Position6Option)
            {
                DieResultDecisionPopup.IsOpen = false;
            }

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
                "Varje gång en spelare får en 6:a på tärningen får det två följder:\r\n\n" +
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
                        
            animateDownRules.Begin();
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
            GameManager.CurrentGame.CurrentGameState = Game.GameState.PreRoll;
        }

        /// <summary>
        /// When user rolls a 6 on the die, they are presented with the option to move pawn to position 1 or 6.
        /// </summary>
        /// <param name="sender"> The Button for moving to cirkle 6 was clicked.</param>
        private void Position6Option_Click(object sender, RoutedEventArgs e)
        {
            stander.MoveInPath(5);
            CloseBtn_Click(sender, e);
            GameManager.CurrentGame.CurrentGameState = Game.GameState.PreRoll;
        }

        /// <summary>
        /// Makes sure the window fits the current screen being used.
        /// </summary>
        private void ParentGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            NewGameDialog.Width = ParentGrid.ActualWidth;
            NewGameDialog.Height = ParentGrid.ActualHeight;

            RulesPopup.Width = ParentGrid.ActualWidth;
            RulesPopup.Height = ParentGrid.ActualHeight;

            DieResultDecisionPopupBackground.Width = ParentGrid.ActualWidth;
            DieResultDecisionPopupBackground.Height = ParentGrid.ActualHeight;

            DieAnimationPopupBackground.Width = ParentGrid.ActualWidth;
            DieAnimationPopupBackground.Height = ParentGrid.ActualHeight;
        }

        /// <summary>
        /// Updates pawn slots of [team] to show how many pawns of that team have won
        /// </summary>
        /// <param name="team">Team</param>
        public static void UpdatePawnSlots(Team team) {
            (StackPanel, TeamColor)[] slots = new(StackPanel, TeamColor)[] {
                (redSlots, TeamColor.Red),
                (yellowSlots, TeamColor.Yellow),
                (greenSlots, TeamColor.Green),
                (blueSlots, TeamColor.Blue),
                };

            foreach ((StackPanel panel, TeamColor color) in slots) {
                if (color != team.TeamColor) continue;
                int count = team.Pawns.Count(p => p.HasWon);

                for(int i = 0; i < count; i++) {
                    Image image = (Image) panel.Children[i];
                    switch(color) {
                        case TeamColor.Red: image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Pawns/RedPawn.png")); break;
                        case TeamColor.Yellow: image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Pawns/YellowPawn.png")); break;
                        case TeamColor.Green: image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Pawns/GreenPawn.png")); break;
                        case TeamColor.Blue: image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Pawns/BluePawn.png")); break;
                    }
                }
            }

        }

        private void RollsCheatBox_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox box = (TextBox)sender;
            var input = box.Text;
            foreach(char c in input) {
                if ((c < '0' || c > '9') && c != ' ') return;
            }
            var array = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            int[] ints = Array.ConvertAll(array, s => int.Parse(s));

            Trace.WriteLine(string.Join("-", ints));
            GameEvents.ForcedRolls = ints.ToList();

        }

        public static void UpdateRollsCheatBox() {
            int i = 0;
            foreach(char c in rollsCheatBox.Text) {
                i++;
                if (c == ' ') break;
            }
            rollsCheatBox.Text = rollsCheatBox.Text.Remove(0, i);
        }

        private void RulesCloseBtn_RightTapped(object sender, RightTappedRoutedEventArgs e) {
            CheatPanel.Visibility = CheatPanel.Visibility == Visibility ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
