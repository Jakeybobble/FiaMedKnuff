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

    public class Pawn {

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
            Trace.WriteLine(CurrentTile.SpaceType);
        }

        /// <summary>
        /// Move self forwards an amount of spaces
        /// </summary>
        /// <param name="spaces">Spaces to move forwards</param>
        public void MoveInPath(int spaces) {
            if(CurrentTile.SpaceType == SpaceType.Home)
            {
                if (GameManager.CurrentDieNumber == 1)
                {
                    SpaceInPath = 0;
                    Move(Team.Path[SpaceInPath]);
                } else if (GameManager.CurrentDieNumber == 6)
                {
                    SpaceInPath = spaces;
                    Move(Team.Path[SpaceInPath]);

                    string text = "Du får rulla en gång till!";
                    GamePage.ChangeOutputTextBox(text);
                }
            }
            else
            {
                Move(spaces);

                if(GameManager.CurrentDieNumber == 6)
                {
                    string text = "Du får rulla en gång till!";

                    GamePage.ChangeOutputTextBox(text);
                }

            }

        }

        public void Move(int spaces) {
            SpaceInPath = Math.Clamp(SpaceInPath + spaces, 0, Team.Path.Count - 1);
            Move(Team.Path[SpaceInPath]);
            }
        }
    }
