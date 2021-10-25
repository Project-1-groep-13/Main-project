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
using System.Data.SqlClient;
using System.Data;

namespace project_p1
{
    /// <summary>
    /// Interaction logic for Game1Player.xaml
    /// </summary>
    public partial class Game1Player : Window
    {
        DispatcherTimer Gametimer = new DispatcherTimer();
        bool MoveLeft, MoveRight;
        List<Rectangle> ItemRemover = new List<Rectangle>();
        //Connection String must be chnaged in different branches to match the file path of the device that you are using
        const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\NHL First year Periode 1\\programmeren\\arcade\\arcade 4.0\\project p1\\project p1\\DataBase\\GameDatabase.mdf;Integrated Security=True";
        PlayerData playerData = new PlayerData();


        Random Ran = new Random();

        int EnemySpriteCounter = 0;
        int EnemyCounter = 100;
        int PlayerSpeed = 10;
        int Limit = 50;
       public int Score = 0;
        int Damage = 0;
        int EnemySpeed = 10;
        bool PauseOnOff = true;
        public string player1Name;

        Rect PlayerHitBox;

        public Game1Player()
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

            //player foto//
            ImageBrush PlayerImage = new ImageBrush();
            PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            Player.Fill = PlayerImage;

            
        }


        /// <summary>
        /// Creating a method to perform query on to the databse
        /// </summary>
        /// <param name="queryString">type your query here</param>
        /// <param name="connectionString">Database connection here</param>
        private static void CreateCommand(string queryString , string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //pause button while playing
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
            PlayerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            EnemyCounter -= 1;
            //score setting//
            Scoretext.Content = "score: " + Score;
            Damagetext.Content = "Damage: " + Damage;
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
                        Damage += 10;
                    }
                    Rect EnemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (PlayerHitBox.IntersectsWith(EnemyHitBox))
                    {
                        ItemRemover.Add(x);
                        Damage += 5;
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

            if (Score > 40)
            {
                Limit = 20;
                EnemySpeed = 11;
            }
            if (Score > 100)
            {
                Limit = 20;
                EnemySpeed = 12;
            }

            if (Damage > 99)
            {
                CreateCommand("INSERT INTO [Game1player] ([playerName],[HighScore]) VALUES ('" + player1Name + "','" + Score + "')", ConnectionString);

                Gametimer.Stop();
                Damagetext.Content = "damage: 100";
                Damagetext.Foreground = Brushes.Red;
                PlayAgainMenu playAgainMenu = new PlayAgainMenu();
                this.Visibility = Visibility.Hidden;
                playAgainMenu.ScoreGot.Content += Convert.ToString(Score);
                playAgainMenu.Visibility = Visibility.Visible;
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
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
                Rectangle NewBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player) + Player.Width / 2);
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player) - NewBullet.Height);
                MyCanvas.Children.Add(NewBullet);

            }
        }
        //Quiting the game
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            
            CreateCommand("INSERT INTO [Game1player] ([playerName],[HighScore]) VALUES ('" + player1Name + "','" + Score + "')", ConnectionString);
            Gametimer.Stop();
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Visibility = Visibility.Visible;
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
            MyCanvas.Children.Add(NewEnemy);
        }
    }
}
    

