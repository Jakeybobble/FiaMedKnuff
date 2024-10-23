using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace FiaMedKnuff.FiaGame {
    public class Team {

        public string Name { get; set; }

        public TeamColor TeamColor = TeamColor.None;

        public enum TeamType {
            Player, Bot
        }
        public TeamType Type = TeamType.Bot;

        public List<Pawn> Pawns = new List<Pawn>();

        /// <summary>
        /// The ID of the space each pawn in the team starts on
        /// </summary>
        public int StartingSpace = 12;

        /// <summary>
        /// The ID of the space each pawn enters their center lane on
        /// </summary>
        public int TowardsCenterStartingSpace = 4;

        /// <summary>
        /// The path which each pawn in the team will move on
        /// </summary>
        public List<Tile> Path = new List<Tile>();

        public Team() { }

        public Team(TeamColor color, TeamType type) {
            TeamColor = color; Type = type;
        }

        public Team(TeamColor color, TeamType type, int startingSpace, int towardsCenterStartingSpace, string name) {
            TeamColor = color; Type = type; StartingSpace = startingSpace; TowardsCenterStartingSpace = towardsCenterStartingSpace; Name = name;
        }

        /// <summary>
        /// Generates a path
        /// </summary>
        public void GeneratePath() {

            for (int i = 0; i < Game.Spaces; i++) {
                int space = (i + StartingSpace) % Game.Spaces;
                Path.Add(GameManager.CurrentGame.Tiles[SpaceType.Surrounding][space]);
            }
            for (int i = 0; i < 4; i++) {
                Path.Add(GameManager.CurrentGame.Tiles[SpaceType.TowardsCenter][TowardsCenterStartingSpace + i]);
            }
            Path.Add(GameManager.CurrentGame.Tiles[SpaceType.Center][0]);
        }

        /// <summary>
        /// If all pawns have won, go to the end screen
        /// </summary>
        public void WinCheck() {
            if (!Pawns.All(pawn => pawn.HasWon)) return;

            ResultatPage.WinningTeam = TeamColor;
            Frame navigationFrame = Window.Current.Content as Frame;
            navigationFrame.Navigate(typeof(ResultatPage));

        }


        /// <summary>
        /// Returns true if any pawn in the team can move
        /// </summary>
        public bool CanMoveCheck() {
            if (Pawns.All(pawn => !pawn.CanMove())) {
                return false;
            }
            return true;
        }
    }
}
