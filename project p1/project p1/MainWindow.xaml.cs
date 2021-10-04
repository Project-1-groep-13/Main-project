using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project_p1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            Hoofdmenu.Background = bg;

        }

        private void Start1Player_Click(object sender, RoutedEventArgs e)
        {
            Game1Player game1Player = new Game1Player();
            MainWindow hoofdmenu = new MainWindow();
            hoofdmenu.Visibility = Visibility.Hidden;
            game1Player.Visibility = Visibility.Visible;

        }

        private void Start2Player_Click(object sender, RoutedEventArgs e)
        {
            MainWindow hoofdmenu = new MainWindow();
            hoofdmenu.Visibility = Visibility.Hidden;

        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            MainWindow hoofdmenu = new MainWindow();
            hoofdmenu.Visibility = Visibility.Hidden;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

