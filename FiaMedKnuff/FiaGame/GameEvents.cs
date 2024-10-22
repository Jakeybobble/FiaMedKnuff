using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;

namespace FiaMedKnuff.FiaGame {
    internal static class GameEvents
    {
        /// <summary>
        /// When user clicks a pawn to move, the pawn is moved. If the die result was a 6, user gets prompted with a Decision Popup. 
        /// </summary>
        /// <param name="tile"></param>
        public static void OnTileClicked(Tile tile) 
        {
            if (GameManager.CurrentGame.CurrentGameState == Game.GameState.PostRoll)
            {
                if (tile.Stander == null) return;
                                
                if (GameManager.CurrentGame.CurrentTeam != tile.Stander.Team) return;
                Trace.WriteLine($"Boop! You have clicked tile with space {tile.Space}!");

                if(GameManager.CurrentDieNumber == 6 && tile.Stander.CurrentTile.SpaceType == SpaceType.Home)
                {
                    GamePage.stander = tile.Stander;
                    GamePage.dieDecisionPopup.IsOpen = true;
                    GamePage.GlowEffectDie();
                   
                }
                else if (GameManager.CurrentDieNumber == 6 && tile.Stander.CurrentTile.SpaceType != SpaceType.Home)
                {
                    GamePage.stander = tile.Stander;
                    tile.Stander?.MoveInPath(GameManager.CurrentDieNumber);
                    TileControl.Selectable = false;
                    GameManager.CurrentGame.CurrentGameState = Game.GameState.PreRoll;
                    GamePage.GlowEffectDie();
                   
                }
                else
                {
                    tile.Stander?.MoveInPath(GameManager.CurrentDieNumber); // Move the pawn if it exists
                    GameManager.CurrentGame.EndTurn();
                    TileControl.Selectable = false;
                }
            }           
        }

        public static int OnDieClicked() {

            GamePage.EndGlowEffectDie();

            // Return if state is PostRoll
            if (GameManager.CurrentGame.CurrentGameState == Game.GameState.PostRoll) {
                GamePage.changeOutputText.Text = "Det är inte din tur.";
                return -1;
            }
            
            // Roll and set die number
            Random rnd = new Random();
            int dieThrow = rnd.Next(1, 7);

            GameManager.CurrentDieNumber = dieThrow;

            // Set all to be selectable and change to PostRoll
            TileControl.Selectable = true;
            GameManager.CurrentGame.CurrentGameState = Game.GameState.PostRoll;
            return dieThrow;


        }
    }
}
