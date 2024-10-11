using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    internal static class GameEvents {
        public static void OnTileClicked(Tile tile) {
            Trace.WriteLine($"Boop! You have clicked tile with space {tile.Space}!");
        }
    }
}
