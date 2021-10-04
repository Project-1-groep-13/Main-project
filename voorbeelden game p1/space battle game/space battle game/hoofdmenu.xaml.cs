using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace space_battle_game
{
    /// <summary>
    /// Interaction logic for hoofdmenu.xaml
    /// </summary>
    /* <Grid DataGridRow.Selected="1">
        <Grid.ColumnDefinitions>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
        </Grid.ColumnDefinitions>
    </Grid>
   */
    public partial class hoofdmenu : Window
    {
        public hoofdmenu()
        {
            InitializeComponent();

            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            Hoofdmenu.Background = bg;
        }

        private void Start1Player_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Start2Player_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
