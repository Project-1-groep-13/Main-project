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
        DispatcherTimer Gametimer = new DispatcherTimer(); //making a gametimer 
        bool MoveLeft1, MoveRight1, MoveLeft2, MoveRight2; //making the bools for the movement for both players 
        List<Rectangle> ItemRemover = new List<Rectangle>(); //creating the list for the itemremover 
        const string ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Hajan\\OneDrive - NHL Stenden\\Documenten\\GitHub\\Main-project\\project p1\\project p1\\DataBase\\GameDatabase.mdf;Integrated Security = True";


        Random Ran = new Random(); //randomiser 

        int EnemySpriteCounter = 0; //all the ints, bools, strings and rects for start game 
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

        /// <summary>
        /// Initialise 2 player game
        /// </summary>
        public Game2Player()
        {
            InitializeComponent();
            Gametimer.Interval = TimeSpan.FromMilliseconds(20); //set timespeed for gametimer 
            Gametimer.Tick += Gameloop;
            Gametimer.Start(); //start gametimer  

            MyCanvas.Focus();
            //achtergrond
            ImageBrush background = new ImageBrush(); //create imagebrush for background 
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png")); //taking image background from folder
            background.TileMode = TileMode.Tile;
            background.Viewport = new Rect(0, 0, 0.15, 0.15);
            background.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvas.Background = background; //setting background 

            //player 1 foto//
            ImageBrush Player1Image = new ImageBrush(); //creating imagebrush for player 1 
            Player1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_5.png")); //taking image player 1 from folder
            Player1.Fill = Player1Image; //filling player 1 with playerimage for player 1

            //player 2 foto//
            ImageBrush Player2Image = new ImageBrush(); //creating imagebrush for player 2 
            Player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P2_5.png")); //taking image player 2 from folder
            Player2.Fill = Player2Image; //filling player 2 with playerimage for player 2

        }
        
        /// <summary>
        /// Database connection 
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
        
        /// <summary>
        /// Pauzeknop functionaliteit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Gameplay 2Player gamemode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void Gameloop(object sender, EventArgs e)
        {
            ImageBrush Player1Image = new ImageBrush();
            ImageBrush Player2Image = new ImageBrush();
            Player1HitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height); //creating playerhitbox player 1
            Player2HitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height); //creating playerhitbox player 2

            EnemyCounter -= 1;
            //score setting//
            Scoretext.Content = "score: " + Score;
            Damage1text.Content = "Lives: " + Damage1;
            Damage2text.Content = "Lives: " + Damage2;
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

                    Rect BulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); //creating bullethitbox 
                    if (Canvas.GetTop(x) < 10)
                    {
                        ItemRemover.Add(x); //if bullet is out of window add itemremover
                    }

                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect EnemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height); //creating enemyhitbox

                            if (BulletHitBox.IntersectsWith(EnemyHit)) //bullet and enemy hit, both in itemremover and score +1
                            {
                                ItemRemover.Add(x);
                                ItemRemover.Add(y);
                                Score++;
                            }
                        }
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemy")
                {  //enemy hit op player of bij player langs
                    Canvas.SetTop(x, Canvas.GetTop(x) + EnemySpeed);

                    if (Canvas.GetTop(x) > 750) //if enemy gets under window, enemy in itemremover and damage -1 for player 1 and 2 
                    {
                        ItemRemover.Add(x);
                        Damage1-=1;
                        Damage2-=1;
                    }
                    Rect EnemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); //create enemyhitbox

                    if (Player1HitBox.IntersectsWith(EnemyHitBox)) //player 1 hits enemy, enemy in itemremover and damage player 1 -1 
                    {
                        ItemRemover.Add(x);
                        Damage1 -= 1;
                    }
                    if (Player2HitBox.IntersectsWith(EnemyHitBox)) //player 2 hits enemy, enemy in itemremover and damage player 2 -1 
                    {
                        ItemRemover.Add(x);
                        Damage2 -= 1;
                    }
                }
                //player 1 background change
                if (Damage1 == 4)
                {
                    Player1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_4.png"));
                    Player1.Fill = Player1Image;
                }
                if (Damage1 == 3)
                {
                    Player1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_3.png"));
                    Player1.Fill = Player1Image;
                }
                if (Damage1 == 2)
                {
                    Player1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_2.png"));
                    Player1.Fill = Player1Image;
                }
                if (Damage1 == 1)
                {
                    Player1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P1_1.png"));
                    Player1.Fill = Player1Image;
                }


                //player 2 background change
                if (Damage2 == 4)
                {
                    Player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P2_4.png"));
                    Player2.Fill = Player2Image;
                }
                if (Damage2 == 3)
                {
                    Player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P2_3.png"));
                    Player2.Fill = Player2Image;
                }
                if (Damage2 == 2)
                {
                    Player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P2_2.png"));
                    Player2.Fill = Player2Image;
                }
                if (Damage2 == 1)
                {
                    Player2Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/playerimage/P2_1.png"));
                    Player2.Fill = Player2Image;
                }

            }

            foreach (Rectangle i in ItemRemover) //itemremover clear 
            {
                MyCanvas.Children.Remove(i);
            }
            //moeilijheidsgraad en Game Over

            #region EVENTS AND SCORE LIMITS
            // Maakt spel moeilijker als de score hoger dan 10 is
            if (Score > 10)
            {
                Limit = 20;
                EnemySpeed = 11;
            }
            
            // Maakt spel nog moeilijker als de score hoger dan 50 is
            if (Score > 50)
            {
                Limit = 20;
                EnemySpeed = 12;
            }
            
            // Verwijdert player 2 als zijn levens onder 0 komt
            if (Damage1>0 && Damage2<0)
            {
               Damage2 = 0;
               MyCanvas.Children.Remove(Player2);
            }
            
            // Verwijdert player 1 als zijn levens onder 0 komt
            if (Damage2>0 && Damage1<0)
            {
                Damage1 = 0;
                MyCanvas.Children.Remove(Player1);
            }
            
            // Als beide spelers zijn overleden komt er een pop-up met knoppen voor opnieuw spelen en de score
            if (Damage1 < 0 && Damage2 < 0) 
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
        /// <summary>
        /// If movement buttons are pressed movement starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) // Verplaatsing links P1
            {
                MoveLeft1 = true;
            }
            if (e.Key == Key.Right) // Verplaatsing rechts P1
            {
                MoveRight1 = true;
            }
            if (e.Key == Key.A) // Verplaatsing links P2
            {
                MoveLeft2 = true;
            }
            if (e.Key == Key.D) // Verplaatsing rechts P2
            {
                MoveRight2 = true;
            }

        }
        
        /// <summary>
        /// Als knop losgelaten wordt wordt de actie die bij de knop hoort toegepast. Dit is verplaatsing stoppen of kogel afvuren.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) // Stop bewegen P1
            {
                MoveLeft1 = false;
            }
            if (e.Key == Key.Right) // Stop bewegen P1
            {
                MoveRight1 = false;
            }
            if (e.Key == Key.Space) // kogel afvuren P1
            {
                ImageBrush bullet = new ImageBrush(); //creating new imagebrush for bullet 
                bullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/kannonskogel.png")); //getting image from folder for bullet
                Rectangle NewBullet = new Rectangle //creating new rectangle for bullet 
                {
                    Tag = "bullet",
                    Height = 5,
                    Width = 5,
                    Fill = bullet, //filling rectangle with image bullet 
                    
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player1) + Player1.Width / 2); //set location bullet to player 1 
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player1) - NewBullet.Height);

                MyCanvas.Children.Add(NewBullet); //adding bullet to window
            }
            if (e.Key == Key.A) // Stop bewegen P2
            {
                MoveLeft2 = false;
            }
            if (e.Key == Key.D) // Stop bewegen P2
            {
                MoveRight2 = false;
            }
            if (e.Key == Key.W) // Kogel afvuren P2
            {
                ImageBrush bullet = new ImageBrush(); //creating new imagebrush for bullet 
                bullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/kannonskogel.png")); //getting image from folder for bullet 
                Rectangle NewBullet = new Rectangle //creating new rectangle for bullet 
                {
                    Tag = "bullet",
                    Height = 5,
                    Width = 5,
                    Fill = bullet, //filling rectangle with image bullet 
                   
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player2) + Player2.Width / 2); //set location bullet to player 2 
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player2) - NewBullet.Height);

                MyCanvas.Children.Add(NewBullet); //adding bullet to window 
            }

        }
        
        /// <summary>
        /// Quits game if button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void Quit_Click(object sender, RoutedEventArgs e) //quit button click
        {
            CreateCommand("INSERT INTO [Game2player] ([playerName1],[playerName2],[HighScore]) VALUES ('" + player1name + "','" + player2name + "','" + Score + "')", ConnectionString);
            Gametimer.Stop();
            this.Close();
            
        }

        /// <summary>
        /// P2 movement left and rigth
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
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

        /// <summary>
        /// P2 movement stops and bullet spawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
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
                Rectangle NewBullet = new Rectangle //same as above, bullet rectangle creation player 2 
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                };
                Canvas.SetLeft(NewBullet, Canvas.GetLeft(Player2) + Player2.Width / 2); //setting bullet location to player2 
                Canvas.SetTop(NewBullet, Canvas.GetTop(Player2) - NewBullet.Height);

                MyCanvas.Children.Add(NewBullet); //adding bullet to window

            }
        }

        /// <summary>
        /// Spawn enemie with random skin
        /// </summary>
        /// <returns></returns>
        private void MakeEnimies()
        {
            ImageBrush EnemySprite = new ImageBrush(); //creating imagebrush enemy 
            EnemySpriteCounter = Ran.Next(1, 5); //creating random number for enemy skin 

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
                Fill = EnemySprite, //filling rectangle with enemy image from above 
            };

            Canvas.SetTop(NewEnemy, -100); //setting location enemy 
            Canvas.SetLeft(NewEnemy, Ran.Next(30, 430));
            MyCanvas.Children.Add(NewEnemy); //adding enemy to window 
        }
    }
}
    

