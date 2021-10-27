using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;
using project_p1;
using System.Data.SqlClient;

namespace project_p2
{
    /// <summary>
    /// Interaction logic for Game2Player.xaml
    /// </summary>
    public partial class Game2Player : Window
    {
        DispatcherTimer Gametimer = new DispatcherTimer();
        bool MoveLeft1, MoveRight1, MoveLeft2, MoveRight2;
        List<Rectangle> ItemRemover = new List<Rectangle>();
        const string ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Hajan\\OneDrive - NHL Stenden\\Documenten\\GitHub\\Main-project\\project p1\\project p1\\DataBase\\GameDatabase.mdf;Integrated Security = True";


        Random Ran = new Random();

        int EnemySpriteCounter = 0;
        int EnemyCounter = 100;
        int PlayerSpeed = 10;
        int Limit = 50;
        int Score = 0;
        int Damage1 = 5;
        int Damage2 = 5;
        int EnemySpeed = 10;
        bool PauseOnOff = true;
        public string player1name, player2name;

        Rect Player1HitBox;
        Rect Player2HitBox;

        public Game2Player()
        {
            InitializeComponent();
            Gametimer.Interval = TimeSpan.FromMilliseconds(20);
            Gametimer.Tick += Gameloop;
            Gametimer.Start();

            MyCanvas.Focus();
            //achtergrond//
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            background.TileMode = TileMode.Tile;
            background.Viewport = new Rect(0, 0, 0.15, 0.15);
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvas.Background = background;

            //player 1 foto//
            ImageBrush Player1Image = new ImageBrush();
            Player1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/1.png"));
            Player1.Fill = Player1Image;

            //player 2 foto//
            ImageBrush Player2Image = new ImageBrush();
            Player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/2.png"));
            Player2.Fill = Player2Image;

        }

        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
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
            Player1HitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);
            Player2HitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);

            EnemyCounter -= 1;
            //score setting//
            Scoretext.Content = "score: " + Score;
            Damage1text.Content = "Levens: " + Damage1;
            Damage2text.Content = "Levens: " + Damage2;
            //enemy spawning//
            if (EnemyCounter < 0)
            {
                MakeEnimies();
                EnemyCounter = Limit;
            }
            //player 1 movement//
            if (MoveLeft1 == true && Canvas.GetLeft(Player1) > 0)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - PlayerSpeed);
            }
            if (MoveRight1 == true && Canvas.GetLeft(Player1) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + PlayerSpeed);
            }

            //player 2 movement//
            if (MoveLeft2 == true && Canvas.GetLeft(Player2) > 0)
            {
                Canvas.SetLeft(Player2, Canvas.GetLeft(Player2) - PlayerSpeed);
            }
            if (MoveRight2 == true && Canvas.GetLeft(Player2) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Player2, Canvas.GetLeft(Player2) + PlayerSpeed);
            }

            //actual gameplay//
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {   //bullet hit op enemy//
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    Rect BulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) < 10)
                    {
                        ItemRemover.Add(x);
                    }

                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
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
                        Damage1-=1;
                        Damage2-=1;
                    }
                    Rect EnemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Player1HitBox.IntersectsWith(EnemyHitBox))
                    {
                        ItemRemover.Add(x);
                        Damage1 -= 1;
                    }
                    if (Player2HitBox.IntersectsWith(EnemyHitBox))
                    {
                        ItemRemover.Add(x);
                        Damage2 -= 1;
                    }
                }

            }

            foreach (Rectangle i in ItemRemover)
            {
                MyCanvas.Children.Remove(i);
            }
            //moeilijheidsgraad en Game Over//
            /*
                if (Score > 10)
            {
                Limit = 20;
                EnemySpeed = 20;
            }
            */
            #region EVENTS AND SCORE LIMITS
            if (Score > 10)
            {
                Limit = 20;
                EnemySpeed = 11;
            }
            if (Score > 50)
            {
                Limit = 20;
                EnemySpeed = 12;
            }
            if (Damage1>0 && Damage2<0)
            {
               Damage2 = 0;
               MyCanvas.Children.Remove(Player2);
            }
            if (Damage2>0 && Damage1<0)
            {
                Damage1 = 0;
                MyCanvas.Children.Remove(Player1);
            }
            if (Damage1==0 && Damage2==0)
            {
                Damage2 = 0;
                Damage1 = 0;
            }
            if (Damage1 <0 && Damage2<0)
            {
                CreateCommand("INSERT INTO [Game2player] ([playerName1],[playerName2],[HighScore]) VALUES ('" + player1name + "','" + player2name + "','" + Score + "')", ConnectionString);
                Gametimer.Stop();
                Damage1text.Content = "Lives p1 : 0";
                Damage1text.Foreground = Brushes.Red;
                Damage2text.Content = "Lives p2 : 0";
                Damage2text.Foreground = Brushes.Red;
                pAgain2player pAgain2 = new pAgain2player();
                pAgain2.Visibility = Visibility.Visible;
                pAgain2.ScoreGot.Content = Convert.ToString(Score);
                this.Close();
            }
            #endregion

           
        }
        //knop voor verplaatsing instellen//
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                MoveLeft1 = true;
            }
            if (e.Key == Key.Right)
            {
                MoveRight1 = true;
            }
            if (e.Key == Key.A)
            {
                MoveLeft2 = true;
            }
            if (e.Key == Key.D)
            {
                MoveRight2 = true;
            }

        }
        //knop voor verplaatsing instellen + bullet spawnen
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                MoveLeft1 = false;
            }
            if (e.Key == Key.Right)
            {
                MoveRight1 = false;
            }
            if (e.Key == Key.Space)
            {
                Rectangle NewBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player1) + Player1.Width / 2);
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player1) - NewBullet.Height);

                MyCanvas.Children.Add(NewBullet);
            }
            if (e.Key == Key.A)
            {
                MoveLeft2 = false;
            }
            if (e.Key == Key.D)
            {
                MoveRight2 = false;
            }
            if (e.Key == Key.W)
            {
                Rectangle NewBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player2) + Player2.Width / 2);
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player2) - NewBullet.Height);

                MyCanvas.Children.Add(NewBullet);
            }

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            CreateCommand("INSERT INTO [Game2player] ([playerName1],[playerName2],[HighScore]) VALUES ('" + player1name + "','" + player2name + "','" + Score + "')", ConnectionString);

            Gametimer.Stop();
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Visibility = Visibility.Visible;
            
        }

        //knop voor verplaatsing instellen
        private void OnKeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                MoveLeft2 = true;
            }
            if (e.Key == Key.D)
            {
                MoveRight2 = true;
            }

        }

        //knop voor verplaatsing instellen + bullet spawnen//
        private void OnKeyUp2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                MoveLeft2 = false;
            }
            if (e.Key == Key.D)
            {
                MoveRight2 = false;
            }
            if (e.Key == Key.W)
            {
                Rectangle NewBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player2) + Player2.Width / 2);
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player2) - NewBullet.Height);

                MyCanvas.Children.Add(NewBullet);

            }
        }

        //enemys generaten
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
            MyCanvas.Children.Add(NewEnemy);
        }
    }
}
    

