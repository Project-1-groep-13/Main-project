﻿using System;
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
    /// Interaction logic for PlayerData.xaml
    /// </summary>
    public partial class PlayerData : Window
    {


        public PlayerData()
        {
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Game1Player game1Player = new Game1Player();
            this.Visibility = Visibility.Hidden;
            game1Player.Visibility = Visibility.Visible;
            

        }
    }
}