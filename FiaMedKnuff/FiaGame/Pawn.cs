﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

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
        public int SpaceInPath = 0;

        public Pawn() { }

        public Pawn(Team team, Tile currentTile) {
            this.Team = team; this.CurrentTile = currentTile;
            currentTile.Stander = this;
            team.Pawns.Add(this);
        }

        /// <summary>
        /// Set self to stand at set Tile, and shove if a pawn is in the way
        /// </summary>
        /// <param name="to">Tile to set position to</param>
        public void SetTile(Tile to) {
            Tile from = CurrentTile;
            if (to.Stander != null) {
                to.Stander.Shove();
            }
            from.Stander = null; to.Stander = this;
            CurrentTile = to;
            from.Refresh(); to.Refresh();
        }

        /// <summary>
        /// Set self to stand at set Tile in Team.Path
        /// </summary>
        /// <param name="space">Space index</param>
        public void SetTile(int space) {
            SpaceInPath = space;
            SetTile(Team.Path[SpaceInPath]);
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
                    SetTile(0);
                } else if (GameManager.CurrentDieNumber == 6)
                {
                    SetTile(spaces);

                    string text = "Du får rulla en gång till!";
                    GamePage.ChangeOutputTextBox(text);                  
                }
            }
            else
            {

                if (SpaceInPath + spaces > Team.Path.Count - 1) { // Move back if roll is too high
                    // TODO: Make pawn go to first empty spot + not shove teammates
                    var count = Team.Path.Count - 1;
                    var newSpace = count - (SpaceInPath + spaces) % count;
                    SetTile(newSpace);

                } else {


                    Move(spaces);


                    if(CurrentTile.SpaceType == SpaceType.Center)
                    {
                        Frame navigationFrame = Window.Current.Content as Frame;
                        navigationFrame.Navigate(typeof(ResultatPage));
                    }
                    if (GameManager.CurrentDieNumber == 6) {
                        string text = "Du får rulla en gång till!";
                        GamePage.ChangeOutputTextBox(text);
                    }
                }
            }
        }

        /// <summary>
        /// Move forwards in Team.Path
        /// </summary>
        /// <param name="spaces">Amount of spaces to move forwards</param>
        public void Move(int spaces) {
            var tile = Team.Path[SpaceInPath];
            var space = SpaceInPath;
            for(int i = 1; i <= spaces; i++) {
                var newSpace = Math.Clamp(space + i, 0, Team.Path.Count - 1);

                if (Team.Path[newSpace].Stander?.Team == Team) {
                    break;
                }
                SpaceInPath = newSpace;
                tile = Team.Path[SpaceInPath];

            }
            SetTile(tile);
            }

        /// <summary>
        /// Shoves this pawn back home
        /// </summary>
        public void Shove() {
            var homeSpace = Team.TowardsCenterStartingSpace; // Funnily enough, this should be the same number.
            for(int i = 0; i < 4; i++) {
                Tile tile = GameManager.CurrentGame.Tiles[SpaceType.Home][i + homeSpace];
                if (tile.Stander == null) {
                    SetTile(tile);
                    break;
                }
            }
        }
     }
}
