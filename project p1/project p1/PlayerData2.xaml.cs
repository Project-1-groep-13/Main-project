using project_p2;
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
    /// Interaction logic for PlayerData2.xaml
    /// </summary>
    public partial class PlayerData2 : Window
    {
        public PlayerData2()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            
            main.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game2Player game2Player = new Game2Player();
            game2Player.Visibility = Visibility.Visible;
            game2Player.player1name = this.PlayerName1.Text;
            game2Player.player2name = this.PlayerName2.Text;
            this.Close();
        }
    }
}
