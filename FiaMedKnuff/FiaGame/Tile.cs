﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Windows.UI.Xaml.Visibility;

namespace FiaMedKnuff.FiaGame {
    public class Tile {

        public TileControl TileControl;

        /// <summary>
        /// The Pawn that is standing on this tile.
        /// Remember that this is updated manually.
        /// </summary>
        public Pawn Stander;

        public SpaceType SpaceType => TileControl.SpaceType;

        public int Space = -1;

        public Tile() {

        }

        public Tile(int space) {
            Space = space;
        }

        /// <summary>
        /// Refreshes the tile's pawn texture depending on who is standing on it
        /// </summary>
        /// <param name="state"></param>
        public void Refresh() {
            TeamColor state = Stander?.TeamColor ?? TeamColor.None;
            switch (state) {
                case TeamColor.None:
                    TileControl.ImageVisibility = Collapsed;
                    break;
                case TeamColor.Red:
                    TileControl.ImageVisibility = Visible;
                    TileControl.ImageSource = "/Assets/Pawns/RedPawn.png";

                    break;
                case TeamColor.Green:
                    TileControl.ImageVisibility = Visible;
                    TileControl.ImageSource = "/Assets/Pawns/GreenPawn.png";

                    break;
                case TeamColor.Blue:
                    TileControl.ImageVisibility = Visible;
                    TileControl.ImageSource = "/Assets/Pawns/BluePawn.png";

                    break;
                case TeamColor.Yellow:
                    TileControl.ImageVisibility = Visible;
                    TileControl.ImageSource = "/Assets/Pawns/YellowPawn.png";

                    break;
            }
            // Not sure if this should be here, but it is a good way to ensure that there are no left-over selectables...
            // TileControl.Selectable = false;

        }

    }
}
