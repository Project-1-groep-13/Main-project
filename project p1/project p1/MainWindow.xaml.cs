using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;
using project_p2;

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

            ImageBrush bg = new ImageBrush(); //creating imagebrush for background
            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/backgroundfoto.jpg")); //getting image from file 
            Hoofdmenu.Background = bg; //set background for Hoofdmenu as bg image 
            Grid grid = new Grid(); //make new grid 
            grid.Background = bg; //set background grid as bg image 

        }

        private void Start1Player_Click(object sender, RoutedEventArgs e)
        {
            PlayerData playerData = new PlayerData(); //create new 1player window 
            playerData.Visibility = Visibility.Visible; //set 1player as visible

        }

        private void Start2Player_Click(object sender, RoutedEventArgs e)
        {
            PlayerData2 playerData2 = new PlayerData2(); //create new 2player window
            playerData2.Visibility = Visibility.Visible; //set 2player as visible 
 
        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            HighScore highScore = new HighScore(); //create new highscore window
            highScore.Visibility = Visibility.Visible; //set highscore as visible 
            this.Close(); //close hoofdmenu 

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //exit hoofdmenu and shutdown game 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EasyMode easyMode = new EasyMode(); //create new easymode window
            this.Close(); //close hoofdmenu 
            easyMode.Visibility = Visibility.Visible; //set easymode as visible
        }
    }
}

