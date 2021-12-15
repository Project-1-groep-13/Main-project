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
        //Connection String must be changed in different branches to match the file path of the device that you are using
        const string ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Hajan\\OneDrive - NHL Stenden\\Documenten\\GitHub\\Main-project\\project p1\\project p1\\DataBase\\GameDatabase.mdf;Integrated Security = True";
        PlayerData playerData = new PlayerData();



        Random Ran = new Random(); //random number generator 

        int EnemySpriteCounter = 0; //all ints, bools and strings for starting game 
        int EnemyCounter = 100;
        int PlayerSpeed = 10;
        int Limit = 50;
        public int Score = 0;
        int Damage = 5;
        int EnemySpeed = 10;
        bool PauseOnOff = true;
        public string player1Name;

        Rect PlayerHitBox;

        public Game1Player()
        {
            InitializeComponent();
            Gametimer.Interval = TimeSpan.FromMilliseconds(20); //set timespeed 
            Gametimer.Tick += Gameloop; 
            Gametimer.Start(); //start timer 

            MyCanvas.Focus();
            //achtergrond//
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png")); //get picture from folder
            background.TileMode = TileMode.Tile;
            background.Viewport = new Rect(0, 0, 0.15, 0.15);
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvas.Background = background; //set background canvas to the background from folder

            //player foto//
            ImageBrush PlayerImage = new ImageBrush(); //make new playerimage 
            PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_5.png")); //get playerimage from folder
            Player.Fill = PlayerImage; //fill Player with playerimage



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
            ImageBrush PlayerImage = new ImageBrush();
            PlayerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height); //make playerhitbox

            EnemyCounter -= 1; //no more enemys on field than one 
            //score setting//
            Scoretext.Content = "score: " + Score; //scoretext on window
            Damagetext.Content = "Lives: " + Damage; //damagetext on window
            //enemy spawning//
            if (EnemyCounter < 0)
            {
                MakeEnimies(); //go to MakeEnemies
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
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20); //setting location of bullet

                    Rect BulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); //make bullethitbox
                    if (Canvas.GetTop(x) < 10)
                    {
                        ItemRemover.Add(x); //set bullet to itemremover
                    }

                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect EnemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height); //make enemyhitbox

                            if (BulletHitBox.IntersectsWith(EnemyHit)) //if bullet and enemy hit, both to itemremover with score +1
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

                    if (Canvas.GetTop(x) > 750) //if enemy gets to bottom, enemy in itemremover and damage -1
                    {
                        ItemRemover.Add(x);
                        Damage -=1;
                    }
                    Rect EnemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); //make anemyhitbox

                    if (PlayerHitBox.IntersectsWith(EnemyHitBox)) //if playerhitbox hits enemyhitbox then add enemy in itemremover and damge -1
                    {
                        ItemRemover.Add(x);
                        Damage -=1;
                    }
                }
                if (Damage == 4) //changing images for the ammount of lives for the player 
                {
                    PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_4.png"));
                    Player.Fill = PlayerImage;
                }
                if (Damage == 3) //changing images for the ammount of lives for the player 
                {
                    PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_3.png"));
                    Player.Fill = PlayerImage;
                }
                if (Damage == 2) //changing images for the ammount of lives for the player 
                {
                    PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_2.png"));
                    Player.Fill = PlayerImage;
                }
                if (Damage == 1) //changing images for the ammount of lives for the player 
                {
                    PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_1.png"));
                    Player.Fill = PlayerImage;
                }

            }

            foreach (Rectangle i in ItemRemover) //itemremover delete from window
            {
                MyCanvas.Children.Remove(i);
            }
            //moeilijheidsgraad en Game Over//        
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

            if (Damage <0 )
            {
                CreateCommand("INSERT INTO [Game1player] ([playerName],[HighScore]) VALUES ('" + player1Name + "','" + Score + "')", ConnectionString);

                Gametimer.Stop();
                Damagetext.Content = "damage: 100";
                Damagetext.Foreground = Brushes.Red;
                pAgain1player pAgain1 = new pAgain1player();
                this.Visibility = Visibility.Hidden;
                pAgain1.ScoreGot.Content += Convert.ToString(Score);
                pAgain1.Visibility = Visibility.Visible;
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
                ImageBrush bullet = new ImageBrush(); //adding imagebrush for bullet
                bullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/kannonskogel.png")); //getting image from folder for bullet
                Rectangle NewBullet = new Rectangle //creating bullet rectangle
                {
                    Tag = "bullet",
                    Height = 5,
                    Width = 5,
                    Fill = bullet, //filling bullet rectangle with image from above 
                    
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player) + Player.Width / 2); //setting location start bullet
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player) - NewBullet.Height); //setting location start bullet 
                MyCanvas.Children.Add(NewBullet); //adding bullet to the window

            }
        }
        //Quiting the game
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            
            CreateCommand("INSERT INTO [Game1player] ([playerName],[HighScore]) VALUES ('" + player1Name + "','" + Score + "')", ConnectionString);
            Gametimer.Stop();
            this.Hide();
        }

        //enemys generaten//
        private void MakeEnimies()
        {
            ImageBrush EnemySprite = new ImageBrush(); //creating imagebrush for enemy 
            EnemySpriteCounter = Ran.Next(1, 5); //creating random number for the enemypicture 

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

            Rectangle NewEnemy = new Rectangle //creating rectangle for enemy 
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = EnemySprite, //filling rectangle with image enemy from above 
            };

            Canvas.SetTop(NewEnemy, -100); //set location from enemy 
            Canvas.SetLeft(NewEnemy, Ran.Next(30, 430)); //set location from enemy 
            MyCanvas.Children.Add(NewEnemy); //set enemy in window 
        }
    }
}
    

