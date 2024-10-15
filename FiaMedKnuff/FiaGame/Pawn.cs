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

        public TeamColor Team;

        public Tile CurrentTile;
        public int Space => CurrentTile.Space;
        public int SpaceInPath = 0;

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
            Trace.WriteLine(CurrentTile.SpaceType);
        }

        /// <summary>
        /// Move self forwards an amount of spaces
        /// </summary>
        /// <param name="spaces">Spaces to move forwards</param>
        public void MoveInPath(int spaces) {
            if (GameManager.CurrentDieNumber == 1 && CurrentTile.SpaceType == SpaceType.Home)
            {
                SpaceInPath = 0;
                Move(HappyPath[SpaceInPath]);
            }
            else if(GameManager.CurrentDieNumber == 6 && CurrentTile.SpaceType == SpaceType.Home)
            {
                SpaceInPath = 5;
                Move(HappyPath[SpaceInPath]);
            }
            else if(GameManager.CurrentDieNumber == 1 && CurrentTile.SpaceType == SpaceType.Surrounding)
            {
                SpaceInPath = Math.Clamp(SpaceInPath + spaces, 0, HappyPath.Count - 1);
                Move(HappyPath[SpaceInPath]);
            }
            else if (GameManager.CurrentDieNumber == 6 && CurrentTile.SpaceType == SpaceType.Surrounding)
            {
                SpaceInPath = Math.Clamp(SpaceInPath + spaces, 0, HappyPath.Count - 1);
                Move(HappyPath[SpaceInPath]);
                //GamePage.DieTextBlock.Text = "";
            }
            else if (GameManager.CurrentDieNumber != 1 && GameManager.CurrentDieNumber != 6 && CurrentTile.SpaceType == SpaceType.Surrounding)
            {
                SpaceInPath = Math.Clamp(SpaceInPath + spaces, 0, HappyPath.Count - 1);
                Move(HappyPath[SpaceInPath]);
            }




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
