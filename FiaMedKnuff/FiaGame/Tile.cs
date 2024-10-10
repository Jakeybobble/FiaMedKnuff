﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Windows.UI.Xaml.Visibility;

namespace FiaMedKnuff.FiaGame {
    internal class Tile {

        public TileControl TileControl;
        public enum TileState { 
            None,
            Red,
            Green,
            Blue,
            Yellow,
        }
        public TileState State = TileState.None;

        public int Space = -1;

        public Tile() {

        }

        public Tile(int space) {
            Space = space;
        }

        /// <summary>
        /// Updates the image of the tile to set state
        /// </summary>
        /// <param name="state"></param>
        public void Update(TileState state) {
            // TODO: Set image sources
            State = state;
            switch (State) {
                case TileState.None:
                    TileControl.ImageVisibility = Collapsed;
                    break;
                case TileState.Red:
                    TileControl.ImageVisibility = Visible;

                    break;
                case TileState.Green:
                    TileControl.ImageVisibility = Visible;

                    break;
                case TileState.Blue:
                    TileControl.ImageVisibility = Visible;

                    break;
                case TileState.Yellow:
                    TileControl.ImageVisibility = Visible;

                    break;
            }

        }

    }
}
