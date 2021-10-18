using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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


namespace project_p1
{
    /// <summary>
    /// Interaction logic for HighScore.xaml
    /// </summary>
    public partial class HighScore : Window
    {
        //Dictionary<string, int> highscores = new Dictionary<string, int>();
        const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\NHL First year Periode 1\\programmeren\\arcade\\arcade 4.0\\project p1\\project p1\\DataBase\\GameDatabase.mdf;Integrated Security=True";


        public HighScore()
        {
            InitializeComponent();
            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            this.Background = bg;

            
            //setHighScore();
            dtGridView();
            //getHighScore();
            //CreateLabels();


        }


        private void dtGridView()
        {
           
            SqlConnection sqlConnection = new SqlConnection();
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("Select playerName,HighScore From Game1player",connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            dtGrid.DataContext = dt;
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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO [Game1player] ([playerName],[HighScore],[Date]) VALUES ('wwweee',55,')";
            CreateCommand(query, ConnectionString);
        }

        //private void getHighScore()
        //{
        //    highscores.Clear();
        //    string query = "SELECT ([playerName],[HighScore],[Date]) FROM Game1player";
        //    SqlConnection sqlConnection = new SqlConnection(ConnectionString);

        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        // Call Read before accessing data.
        //        while (reader.Read())
        //        {
        //            highscores.Add((string)reader[0], (int)reader[1]);
        //        }

        //        // Call Close when done reading.
        //        reader.Close();
        //    }

        //}

        //private void CreateLabels()
        //{
        //    HighScorePanel.Children.Clear();
        //    var sortedHighScore = from score in highscores orderby score.Value descending select score;

        //    foreach (KeyValuePair<string,int> highscore in sortedHighScore)
        //    {
        //        Label label = new Label();
        //        label.Content = "Player " + highscore.Key + " Scored " + highscore.Value;
        //        label.HorizontalAlignment = HorizontalAlignment.Center;
        //        HighScorePanel.Children.Add(label);
        //    }

        //}





        //private void setHighScore()
        //{
        //    PlayerData playerData = new PlayerData();
        //    string player1name = Convert.ToString(playerData.PlayerName1.Text);
        //    Game1Player game1Player = new Game1Player();
        //    int score = Convert.ToInt32(game1Player.Score);
        //    string query = "INSERT INTO [Game1player] ([playerName],[HighScore],[Date]) VALUES ('Test','55')";
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    SqlCommand command = new SqlCommand();
        //    try
        //    {
        //        command.CommandText = query;
        //        command.CommandType = CommandType.Text;
        //        command.Connection = connection;
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("error");
        //        connection.Close();
        //    }

        //}


    }
}
