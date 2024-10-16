﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FiaMedKnuff.FiaGame;
using Windows.UI.Core;

// Denna klass använder för tillfället kod genererat med ChatGPT

namespace FiaMedKnuff {
    public sealed partial class TileControl : UserControl {

        public TileControl() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Controls whether the click or hover events should run
        /// </summary>
        public static bool Selectable = false;

        private Tile tile;

        /// <summary>
        /// Runs once the component is loaded, after the properties are set in XAML
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) {
                return;
            }

            Register();

        }

        /// <summary>
        /// Register the tile into the current game // TODO: Better summary
        /// </summary>
        private void Register() {
            GameManager.CurrentGame.RegisterTiles();

            var tile = GameManager.CurrentGame.Tiles[SpaceType][Space];
            tile.TileControl = this;
            this.tile = tile;

            tile.Refresh();

        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(TileControl), new PropertyMetadata("Assets/Pawns/Redpawn.png"));

        public string ImageSource {
            get { return (string)GetValue(ImageSourceProperty); }
            set {
                HighlightImageSource = $"{value.Remove(value.Length-4)}_Highlighted.png";
                SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ImageVisibilityProperty =
            DependencyProperty.Register("ImageVisibility", typeof(Visibility), typeof(TileControl), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ImageVisibility {
            get { return (Visibility)GetValue(ImageVisibilityProperty); }
            set { SetValue(ImageVisibilityProperty, value); }
        }

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush), typeof(TileControl), new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.White)));

        public SolidColorBrush BackgroundColor {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty SpaceProperty =
            DependencyProperty.Register("Space", typeof(int), typeof(TileControl), new PropertyMetadata(-1));

        private int space = -1;
        public int Space {
            get => space;
            set => space = value;
        }

        public static readonly DependencyProperty HighlightingOpacityProperty =
            DependencyProperty.Register("HighlightingOpacity", typeof(float), typeof(TileControl), new PropertyMetadata(0f));

        public float HighlightingOpacity {
            get => (float)GetValue(HighlightingOpacityProperty);
            set => SetValue(HighlightingOpacityProperty, value);
        }

        public static readonly DependencyProperty HighlightImageOpacityProperty =
            DependencyProperty.Register("HighlightImageOpacity", typeof(float), typeof(TileControl), new PropertyMetadata(0f));

        public float HighlightImageOpacity {
            get => (float)GetValue(HighlightImageOpacityProperty);
            set => SetValue(HighlightImageOpacityProperty, value);
        }

        public static readonly DependencyProperty HighlightImageSourceProperty =
            DependencyProperty.Register("HighlightImageSource", typeof(string), typeof(TileControl), new PropertyMetadata("Assets/Pawns/RedPawn_Highlighted.png"));

        public string HighlightImageSource {
            get { return (string)GetValue(HighlightImageSourceProperty); }
            set { SetValue(HighlightImageSourceProperty, value); }
        }

        public static readonly DependencyProperty SpaceTypeProperty =
            DependencyProperty.Register("Space", typeof(SpaceType), typeof(TileControl), new PropertyMetadata(SpaceType.Surrounding));

        public SpaceType SpaceType {
            get => (SpaceType)GetValue(SpaceTypeProperty);
            set => SetValue(SpaceTypeProperty, value);
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e) {
            if (!Selectable) return;
            GameEvents.OnTileClicked(tile);
        }

        private void Border_PointerEntered(object sender, PointerRoutedEventArgs e) {
            if (!Selectable) return;
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 1);
        }

        private void Border_PointerExited(object sender, PointerRoutedEventArgs e) {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        }
    }
}
