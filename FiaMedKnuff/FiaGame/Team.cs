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

        public Team() { }

        public Team(TeamColor color, TeamType type) {

        }
    }
}
