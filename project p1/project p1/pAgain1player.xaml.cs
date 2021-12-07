using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace project_p1
{
    /// <summary>
    /// Interaction logic for pAgain1player.xaml
    /// </summary>
    public partial class pAgain1player : Window
    {
        public pAgain1player()
        {
            InitializeComponent();
            //achtergrond//
            ImageBrush background = new ImageBrush(); //create imagebrush for background
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png")); //getting image from file 
            background.TileMode = TileMode.Tile;
            background.Viewport = new Rect(0, 0, 0.15, 0.15);
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            this.Background = background; //set background from imagebrush background
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close(); //close window
            HighScore highScore = new HighScore(); //open highscore window
            highScore.Visibility = Visibility.Visible; //set highscore window as visible 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); //close window
            MainWindow hoofdmenu = new MainWindow(); //open hoofdmenu window
            hoofdmenu.Visibility = Visibility.Visible; //set hoofdmenu window as visible 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide(); //close window
            PlayerData playerData = new PlayerData(); //open player1 window again 
            playerData.Visibility = Visibility.Visible; //set player1 window as visible 
        }
    }
}
