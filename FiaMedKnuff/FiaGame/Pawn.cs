using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public TeamColor TeamColor => Team.TeamColor;
        public Team Team;

        public Tile CurrentTile;
        public int Space => CurrentTile.Space;
        public int SpaceInPath = 0;

        public Pawn() { }

        public Pawn(Team team, Tile currentTile) {
            this.Team = team; this.CurrentTile = currentTile;
            Trace.WriteLine("I've been put into this team.");
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
        public void MoveInPath(int spaces) {
            if (CurrentTile.SpaceType == SpaceType.Home) {
                SpaceInPath = 0;
                Move(Team.Path[0]);
            } else {
                Move(spaces);
            }

        }

        public void Move(int spaces) {
            SpaceInPath = Math.Clamp(SpaceInPath + spaces, 0, Team.Path.Count - 1);
            Move(Team.Path[SpaceInPath]);
        }

        // TODO: Have these be stored in each team instead
        public static int startingSpace = 2;
        public static int towardsCenterStart = 0;
        public static List<Tile> HappyPath = new List<Tile>();
        public static void GenerateHappyPath() {

            for (int i = 0; i < Game.Spaces; i++) {
                int space = (i + startingSpace) % Game.Spaces;
                HappyPath.Add(GameManager.CurrentGame.Tiles[SpaceType.Surrounding][space]);
            }
            for (int i = 0; i < 4; i++) {
                HappyPath.Add(GameManager.CurrentGame.Tiles[SpaceType.TowardsCenter][towardsCenterStart + i]);
            }
            HappyPath.Add(GameManager.CurrentGame.Tiles[SpaceType.Center][0]);
        }

    }
}
