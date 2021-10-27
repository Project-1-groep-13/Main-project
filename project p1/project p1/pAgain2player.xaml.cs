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
    /// Interaction logic for pAgain2player.xaml
    /// </summary>
    public partial class pAgain2player : Window
    {
        public pAgain2player()
        {
            InitializeComponent();
            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            this.Background = bg;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            HighScore highScore = new HighScore();
            highScore.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow main = new MainWindow();
            main.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            PlayerData2 playerData2 = new PlayerData2();
            playerData2.Visibility = Visibility.Visible;
        }
    }
}
