using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace project_p1
{
    /// <summary>
    /// Interaction logic for Game1Player.xaml
    /// </summary>
    public partial class EasyMode : Window
    {
        DispatcherTimer Gametimer = new DispatcherTimer();
        bool MoveLeft, MoveRight;
        List<Rectangle> ItemRemover = new List<Rectangle>();

        Random Ran = new Random();

        int EnemySpriteCounter = 0;
        int EnemyCounter = 100;
        int PlayerSpeed = 10;
        int Limit = 50;
        int Score = 0;
        int Damage = 5;
        int EnemySpeed = 10;
        bool PauseOnOff = true;
        Rect PlayerHitBox;
        public EasyMode()
        {
            InitializeComponent();
            Gametimer.Interval = TimeSpan.FromMilliseconds(20);
            Gametimer.Tick += Gameloop;
            Gametimer.Start();

            MyCanvass.Focus();
            //achtergrond//
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            background.TileMode = TileMode.Tile;
            background.Viewport = new Rect(0, 0, 0.15, 0.15);
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvass.Background = background;

            //player foto//
            ImageBrush PlayerImage = new ImageBrush();
            PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_5.png"));
            Player.Fill = PlayerImage;

            
        }
        private void WhenButtonClick(object sender, RoutedEventArgs e)
        {

            if (PauseOnOff == true)
            {
                Gametimer.Stop();
            }
            if (PauseOnOff == false)
            {
                Gametimer.Start();
            }
            if (PauseOnOff == true)
            {
                PauseOnOff = false;
            }
            else
            {
                PauseOnOff = true;
            }
        }

        private void Gameloop(object sender, EventArgs e)
        {
            ImageBrush PlayerImage = new ImageBrush();
            PlayerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            EnemyCounter -= 1;
            //score setting//
            Scoretext.Content = "score: " + Score;
            Damagetext.Content = "Levens: " + Damage;
            //enemy spawning//
            if (EnemyCounter < 0)
            {
                MakeEnimies();
                EnemyCounter = Limit;
            }
            //player movement//
            if (MoveLeft == true && Canvas.GetLeft(Player) > 0)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - PlayerSpeed);
            }
            if (MoveRight == true && Canvas.GetLeft(Player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + PlayerSpeed);
            }
            //actual gameplay//
            foreach (var x in MyCanvass.Children.OfType<Rectangle>())
            {   //bullet hit op enemy//
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    Rect BulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) < 10)
                    {
                        ItemRemover.Add(x);
                    }

                    foreach (var y in MyCanvass.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect EnemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (BulletHitBox.IntersectsWith(EnemyHit))
                            {
                                ItemRemover.Add(x);
                                ItemRemover.Add(y);
                                Score++;
                            }
                        }
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemy")
                {  //enemy hit op player of bij player langs//
                    Canvas.SetTop(x, Canvas.GetTop(x) + EnemySpeed);

                    if (Canvas.GetTop(x) > 750)
                    {
                        ItemRemover.Add(x);
                        Damage -=1;
                    }
                    Rect EnemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (PlayerHitBox.IntersectsWith(EnemyHitBox))
                    {
                        ItemRemover.Add(x);
                        Damage -=1;
                    }
                }

            }

            foreach (Rectangle i in ItemRemover)
            {
                MyCanvass.Children.Remove(i);
            }
           

            //limit mode 
            if (Score == 20)
            {
                Gametimer.Stop();
                PlayAgainMenu playAgainMenu = new PlayAgainMenu();
                this.Visibility = Visibility.Hidden;
                playAgainMenu.ScoreGot.Content += Convert.ToString(Score);
                playAgainMenu.Visibility = Visibility.Visible;
            }
            if (Damage <0)
            {
                Gametimer.Stop();
                PlayAgainMenu playAgainMenu = new PlayAgainMenu();
                this.Visibility = Visibility.Hidden;
                playAgainMenu.ScoreGot.Content += Convert.ToString(Score);
                playAgainMenu.Visibility = Visibility.Visible;
            }
            if (Damage == 4)
            {
                PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_4.png"));
                Player.Fill = PlayerImage;
            }
            if (Damage == 3)
            {
                PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_3.png"));
                Player.Fill = PlayerImage;
            }
            if (Damage == 2)
            {
                PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_2.png"));
                Player.Fill = PlayerImage;
            }
            if (Damage == 1)
            {
                PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_1.png"));
                Player.Fill = PlayerImage;
            }


        }
        //knop voor verplaatsing instellen//
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                MoveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                MoveRight = true;
            }

        }
        //knop voor verplaatsing instellen + bullet spawnen//
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                MoveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                MoveRight = false;
            }
            if (e.Key == Key.Space)
            {
                ImageBrush bullet = new ImageBrush();
                bullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/kannonskogel.png"));
                Rectangle NewBullet = new Rectangle
                {                  
                    Tag = "bullet",
                    Height = 5,
                    Width = 5,
                    Fill = bullet,
                    
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player) + Player.Width / 2);
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player) - NewBullet.Height);

                MyCanvass.Children.Add(NewBullet);

            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (PauseOnOff == true)
            {
                Gametimer.Stop();
            }
            if (PauseOnOff == false)
            {
                Gametimer.Start();
            }
            if (PauseOnOff == true)
            {
                PauseOnOff = false;
            }
            else
            {
                PauseOnOff = true;
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Gametimer.Stop();
            this.Hide();
            MainWindow hoofdmenu = new MainWindow();
            hoofdmenu.Visibility = Visibility.Visible;
        }

        //enemys generaten//
        private void MakeEnimies()
        {
            ImageBrush EnemySprite = new ImageBrush();
            EnemySpriteCounter = Ran.Next(1, 5);

            switch (EnemySpriteCounter)
            {
                case 1:
                    EnemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/1.png"));
                    break;
                case 2:
                    EnemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/2.png"));
                    break;
                case 3:
                    EnemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3.png"));
                    break;
                case 4:
                    EnemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/4.png"));
                    break;
                case 5:
                    EnemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/5.png"));
                    break;

            }

            Rectangle NewEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = EnemySprite,
            };

            Canvas.SetTop(NewEnemy, -100);
            Canvas.SetLeft(NewEnemy, Ran.Next(30, 430));
            MyCanvass.Children.Add(NewEnemy);
        }
    }
}
