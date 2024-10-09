using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    class Game {

        public Dictionary<SpaceType, Dictionary<int, TileControl>> Spaces = new Dictionary<SpaceType, Dictionary<int, TileControl>>();

        public Game() {

        }

    }

    public enum SpaceType {
        Home, TowardsCenter, Surrounding, Center
    }
}
