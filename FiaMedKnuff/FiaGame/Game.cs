using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    class Game {

        public Dictionary<TileType, Dictionary<int, TileControl>> Tiles = new Dictionary<TileType, Dictionary<int, TileControl>>();

        public Game() {

        }

    }

    public enum TileType {
        Home, TowardsCenter, Surrounding, Center
    }
}
