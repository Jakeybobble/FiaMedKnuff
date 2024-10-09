using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    internal static class GameManager {
        public static Game Game;

        /// <summary>
        /// Runs as the application starts
        /// </summary>
        public static void Init() {
            Game = new Game();
        }

        public static Game CurrentGame => Game;
    }
}
