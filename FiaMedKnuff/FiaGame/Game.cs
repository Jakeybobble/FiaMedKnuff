using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    class Game {

        //public Dictionary<SpaceType, Dictionary<int, TileControl>> Spaces = new Dictionary<SpaceType, Dictionary<int, TileControl>>();
        public Dictionary<SpaceType, Dictionary<int, Tile>> Tiles = new Dictionary<SpaceType, Dictionary<int, Tile>>();

        /// <summary>
        /// The current turn of the game. Increments by the end of the turn.
        /// </summary>
        public int Turn = 0;



        public enum GameState {
            PreRoll,
        }

        public Game() {
            // TODO: When it is time, register tiles and players here instead.
        }

        private bool HasRegistered = false;
        /// <summary>
        /// Fills the Tiles dictionary with tiles and puts pawns into each home
        /// </summary>
        public void RegisterTiles() {
            if (HasRegistered) return;
            HasRegistered = true;

            // Create each tile class so that a TileControl can register itself to it after loading
            var homeTiles = new Dictionary<int, Tile>();
            Tile.TeamColor[] teams = { Tile.TeamColor.Red, Tile.TeamColor.Yellow, Tile.TeamColor.Green, Tile.TeamColor.Blue };
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    var team = teams[i];
                    var space = i * 4 + j;

                    // Create the tile...
                    var tile = new Tile(space);
                    homeTiles.Add(space, tile);

                    // ...Create the pawn
                    var pawn = new Pawn(team, tile);
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
