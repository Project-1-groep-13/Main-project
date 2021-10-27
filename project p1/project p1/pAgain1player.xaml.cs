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
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            background.TileMode = TileMode.Tile;
            background.Viewport = new Rect(0, 0, 0.15, 0.15);
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            this.Background = background;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            HighScore highScore = new HighScore();
            highScore.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow hoofdmenu = new MainWindow();
            hoofdmenu.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PlayerData playerData = new PlayerData();
            playerData.Visibility = Visibility.Visible;
        }
    }
}
