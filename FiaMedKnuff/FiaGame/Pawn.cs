using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FiaMedKnuff.FiaGame.Tile;

namespace FiaMedKnuff.FiaGame {
    internal class Pawn {

        public TileState Team;

        public Tile CurrentTile;
        public int Space => CurrentTile.Space;

        public Pawn() { }

        public Pawn(TileState team, Tile currentTile) {
            this.Team = team; this.CurrentTile = currentTile;
        }

        public void Move(int spaces) {
            Tile from = CurrentTile;
            // Temporary code
            Tile to = GameManager.CurrentGame.Tiles[SpaceType.Surrounding][from.Space + spaces];
            
        }

    }
}
