using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {

    public enum TeamColor {
        None,
        Red,
        Green,
        Blue,
        Yellow,
    }

    internal class Pawn {

        public TeamColor Team;

        public Tile CurrentTile;
        public int Space => CurrentTile.Space;

        public Pawn() { }

        public Pawn(TeamColor team, Tile currentTile) {
            this.Team = team; this.CurrentTile = currentTile;
            currentTile.Stander = this;
        }

        /// <summary>
        /// Move self from one tile to the other and refresh both tiles.
        /// </summary>
        /// <param name="to">Tile to move to</param>
        public void Move(Tile to) {
            Tile from = CurrentTile;
            from.Stander = null; to.Stander = this;
            CurrentTile = to;
            from.Refresh(); to.Refresh();
        }

        /// <summary>
        /// Move self forwards an amount of spaces
        /// </summary>
        /// <param name="spaces">Spaces to move forwards</param>
        public void Move(int spaces) {
            // Mostly temporary code to prove it works
            if (CurrentTile.SpaceType == SpaceType.Home) {
                Move(GameManager.CurrentGame.Tiles[SpaceType.Surrounding][2]);
            } else {
                Tile to = GameManager.CurrentGame.Tiles[SpaceType.Surrounding][(CurrentTile.Space + spaces) % Game.Spaces];
                Move(to);
            }
        }

    }
}
