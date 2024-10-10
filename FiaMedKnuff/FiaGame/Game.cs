using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    class Game {

        //public Dictionary<SpaceType, Dictionary<int, TileControl>> Spaces = new Dictionary<SpaceType, Dictionary<int, TileControl>>();
        public Dictionary<SpaceType, Dictionary<int, Tile>> Tiles = new Dictionary<SpaceType, Dictionary<int, Tile>>();

        public Game() {

        }

        private bool HasRegistered = false;
        public void RegisterTiles() {
            if (HasRegistered) return;
            HasRegistered = true;

            // Create each tile class so that a TileControl can register itself to it after loading
            var homeTiles = new Dictionary<int, Tile>();
            Tile.TileState[] teams = { Tile.TileState.Red, Tile.TileState.Yellow, Tile.TileState.Green, Tile.TileState.Blue };
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    var team = teams[i];
                    var space = i * 4 + j;

                    var tile = new Tile(space);
                    tile.State = team;
                    homeTiles.Add(space, tile);
                }
                
            }
            Tiles.Add(SpaceType.Home, homeTiles);

            var surroundTiles = new Dictionary<int, Tile>();
            for (int i = 0; i < 40; i++) {
                surroundTiles.Add(i, new Tile(i));
            }
            Tiles.Add(SpaceType.Surrounding, surroundTiles);

            var towardsCenterTiles = new Dictionary<int, Tile>();
            for (int i = 0; i < 4 * 4; i++) {
                towardsCenterTiles.Add(i, new Tile(i));
            }
            Tiles.Add(SpaceType.TowardsCenter, towardsCenterTiles);

            Tiles.Add(SpaceType.Center, new Dictionary<int, Tile> { { 0, new Tile(0) } });

        }

    }

    public enum SpaceType {
        Home, TowardsCenter, Surrounding, Center
    }
}
