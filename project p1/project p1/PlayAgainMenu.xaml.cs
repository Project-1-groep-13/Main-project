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
    /// Interaction logic for PlayAgainMenu.xaml
    /// </summary>
    public partial class PlayAgainMenu : Window
    {
        public PlayAgainMenu()
        {
            InitializeComponent();
            ImageBrush bg = new ImageBrush(); //create new imagebrush bg 
            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png")); //getting image from folder for bg
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            this.Background = bg; //setting background as imagebrush bg
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close(); //close window
            EasyMode easyMode = new EasyMode(); //open easymode window
            easyMode.Visibility = Visibility.Visible; //set easymode window as visible 
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
    }
}
