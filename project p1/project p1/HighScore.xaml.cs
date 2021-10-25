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
        //Connection String must be chnaged in different branches to match the file path of the device that you are using
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

            
            
            
            dtGridView();
           


        }

        /// <summary>
        ///getting Data from the database to show in DataGrid
        /// </summary>
        private void dtGridView()
        {
           
            SqlConnection sqlConnection = new SqlConnection();
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("Select top 5 playerName AS Name,HighScore AS Score From Game1player Order By HighScore DESC",connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            dtGrid.DataContext = dt;
        }

        /// <summary>
        /// Creating a method to perform query on to the databse
        /// </summary>
        /// <param name="queryString">type your query here</param>
        /// <param name="connectionString">Database connection here</param>
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

    }
}


