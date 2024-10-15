using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiaMedKnuff.FiaGame {
    internal class Team {
        public TeamColor TeamColor = TeamColor.None;
        public enum TeamType {
            Player, Bot
        }
        public TeamType Type = TeamType.Bot;

        /// <summary>
        /// The ID of the space each pawn in the team starts on
        /// </summary>
        public int StartingSpace = 12;
        /// <summary>
        /// The ID of the space each pawn enters their center lane on
        /// </summary>
        public int TowardsCenterStartingSpace = 4;
        public List<Tile> Path = new List<Tile>();

        public Team() { }

        public Team(TeamColor color, TeamType type) {
            TeamColor = color; Type = type;
        }

        public Team(TeamColor color, TeamType type, int startingSpace, int towardsCenterStartingSpace) {
            TeamColor = color; Type = type; StartingSpace = startingSpace; TowardsCenterStartingSpace = towardsCenterStartingSpace;
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
    }
}
