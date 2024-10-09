using System;
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

// Denna klass använder för tillfället kod genererat med ChatGPT

namespace FiaMedKnuff {
    public sealed partial class TileControl : UserControl {

        public TileControl() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Runs once the component is loaded, after the properties are set in XAML
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            // TODO: Register from here.
            Trace.WriteLine(Space);
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(TileControl), new PropertyMetadata("Assets/test_pawn.png"));

        public string ImageSource {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
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

        public int Space {
            get { return (int)GetValue(SpaceProperty); }
            set { SetValue(SpaceProperty, value); }
        }

        public static readonly DependencyProperty SpaceTypeProperty =
            DependencyProperty.Register("Space", typeof(string), typeof(TileControl), new PropertyMetadata("Surrounding"));

        public TileType SpaceType {
            get {
                switch((string)GetValue(SpaceTypeProperty)) {
                    case "Home":
                        return TileType.Home;
                    case "TowardsCenter":
                        return TileType.TowardsCenter;
                    case "Surrounding":
                        return TileType.Surrounding;
                    case "Center":
                        return TileType.Center;
                    default: throw new Exception("Invalid SpaceType found.");
                }
            }
            set {
                switch(value) { // I wish we were in C# 9...
                    case TileType.Home:
                        SetValue(SpaceProperty, "Home");
                        break;
                    case TileType.TowardsCenter:
                        SetValue(SpaceProperty, "TowardsCenter");
                        break;
                    case TileType.Surrounding:
                        SetValue(SpaceProperty, "Surrounding");
                        break;
                    case TileType.Center:
                        SetValue(SpaceProperty, "Center");
                        break;
                }
                
            }
        }

    }
}
