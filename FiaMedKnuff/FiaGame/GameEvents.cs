using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

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

                // TODO: Comment/Uncomment for team lock
                if (GameManager.CurrentGame.CurrentTeam != tile.Stander.Team) return;
                Trace.WriteLine($"Boop! You have clicked tile with space {tile.Space}!");

                if(GameManager.CurrentDieNumber == 6 && tile.Stander.CurrentTile.SpaceType == SpaceType.Home)
                {
                    GamePage.stander = tile.Stander;
                    GamePage.dieDecisionPopup.IsOpen = true;
                   
                }
                else if (GameManager.CurrentDieNumber == 6 && tile.Stander.CurrentTile.SpaceType != SpaceType.Home)
                {
                    GamePage.stander = tile.Stander;
                    tile.Stander?.MoveInPath(GameManager.CurrentDieNumber);
                    TileControl.Selectable = false;
                    GameManager.CurrentGame.CurrentGameState = Game.GameState.PreRoll;
                   
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

            Random rnd = new Random();
            int dieThrow = rnd.Next(1, 7);

            return dieThrow;
        }
    }
}
