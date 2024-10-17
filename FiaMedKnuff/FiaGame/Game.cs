using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FiaMedKnuff.FiaGame {
    class Game {

        public static int Spaces = 40;

        public Dictionary<SpaceType, Dictionary<int, Tile>> Tiles = new Dictionary<SpaceType, Dictionary<int, Tile>>();

        /// <summary>
        /// The current turn of the game. Increments by the end of the turn.
        /// </summary>
        public int Turn = 0;

        public List<Team> Teams = new List<Team> {
            new Team(TeamColor.Red, Team.TeamType.Player, 2, 0, "Rött lag"),
            new Team(TeamColor.Yellow, Team.TeamType.Bot, 12, 4, "Gult lag"),
            new Team(TeamColor.Green, Team.TeamType.Bot, 22, 8, "Grönt lag"),
            new Team(TeamColor.Blue, Team.TeamType.Bot, 32, 12, "Blått lag")
        };
        public int CurrentTeamIndex = 0;
        public Team CurrentTeam => Teams[CurrentTeamIndex];


        /// <summary>
        /// Makes sure the game starts in the PreRoll state.
        /// </summary>
        public GameState CurrentGameState = GameState.PreRoll;
               
        public enum GameState {
            /// <summary>
            /// Before the player has clicked the die.
            /// </summary>
            PreRoll,
            /// <summary>
            /// The state that generates possible choices after the player rolling the die.
            /// </summary>
            PostRoll
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
            TeamColor[] teamColors = { TeamColor.Red, TeamColor.Yellow, TeamColor.Green, TeamColor.Blue };
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    var teamColor = teamColors[i];
                    var space = i * 4 + j;

                    // Create the tile...
                    var tile = new Tile(space);
                    homeTiles.Add(space, tile);

                    // Create the pawn from the team

                    // ...Create the pawn
                    var team = Teams[i] ?? null;
                    Trace.WriteLine(team);
                    if (team == null) continue;
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

            PostRegister();
            
        }

        /// <summary>
        /// Runs once RegisterTiles() has finished
        /// </summary>
        private void PostRegister() {
            
            // Generate each team path
            foreach(Team team in Teams) {
                team?.GeneratePath();
            }

            RandomizeFirstTurn();        
        }
        /// <summary>
        /// This runs only in the beginning of the game to decide which color goes first in the turnorder. 
        /// </summary>        
        public void RandomizeFirstTurn()
        {
            Random random = new Random();
            int startingTeamIndex = random.Next(0, 3);

            Trace.WriteLine($"Starting index: " + startingTeamIndex);

            GamePage.ChangeOutputTextBox($"Det är " + Teams[startingTeamIndex].Name + "s tur att rulla tärningen!");
            CurrentGameState = GameState.PreRoll;

            var border = GetTurnIndicator(Teams[startingTeamIndex].TeamColor);
            border.BorderBrush = (SolidColorBrush)App.Current.Resources[$"Color{startingTeamIndex}"];

            EndTurn();
        }

        public void StartTurn()
        {
            GamePage.ChangeOutputTextBox($"Det är " + CurrentTeam.Name + "s tur att rulla tärningen!");
            CurrentGameState = GameState.PreRoll;

            var border = GetTurnIndicator(CurrentTeam.TeamColor);
            border.BorderBrush = (SolidColorBrush)App.Current.Resources["RedTeam"]; // TBD Change the color to the currentTeams color!
        }

        public void EndTurn() {

            var border = GetTurnIndicator(CurrentTeam.TeamColor);
            border.BorderBrush = (SolidColorBrush)App.Current.Resources["StandardWhite"];

            CurrentTeamIndex = (CurrentTeamIndex + 1) % Teams.Count;
            Turn++;
            GamePage.ChangeOutputTextBox("");
            StartTurn();
        }

        public static Border GetTurnIndicator(TeamColor color)
        {
            switch (color)
            {
                case TeamColor.Red:
                    return GamePage.redTurnIndicator;
                case TeamColor.Green:
                    return GamePage.greenTurnIndicator;
                case TeamColor.Blue:
                    return GamePage.blueTurnIndicator;
                case TeamColor.Yellow:
                    return GamePage.yellowTurnIndicator;

            } return null;

        }
    }

    public enum SpaceType {
        Home, TowardsCenter, Surrounding, Center
    }  
}
