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

            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/backgroundfoto.jpg"));
            Hoofdmenu.Background = bg;
            Grid grid = new Grid();
            grid.Background = bg;

        }

        private void Start1Player_Click(object sender, RoutedEventArgs e)
        {
            PlayerData playerData = new PlayerData();
            this.Close();
            playerData.Visibility = Visibility.Visible;

        }

        private void Start2Player_Click(object sender, RoutedEventArgs e)
        {
            PlayerData2 playerData2 = new PlayerData2();
            playerData2.Visibility = Visibility.Visible;
            this.Close();
 
        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            HighScore highScore = new HighScore();
            highScore.Visibility = Visibility.Visible;
            this.Close();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EasyMode easyMode = new EasyMode();
            this.Close();
            easyMode.Visibility = Visibility.Visible;
        }
    }
}

