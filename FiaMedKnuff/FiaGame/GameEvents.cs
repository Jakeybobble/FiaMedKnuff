using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    internal static class GameEvents
    {
        public static void OnTileClicked(Tile tile) 
        {
            if (GameManager.CurrentGame.CurrentGameState == Game.GameState.PostRoll)
            {
                Trace.WriteLine($"Boop! You have clicked tile with space {tile.Space}!");
                tile.Stander?.MoveInPath(GameManager.CurrentDieNumber); // Move the pawn if it exists

                GameManager.CurrentGame.CurrentGameState = Game.GameState.PreRoll;
                TileControl.Selectable = false;
            }           
        }

        public static int OnDieClicked() {
            
            Random rnd = new Random();
            int dieThrow = rnd.Next(1, 7);

            return dieThrow;
        }
    }
}
