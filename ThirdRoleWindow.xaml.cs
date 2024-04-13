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

namespace PRACTICA5
{
    /// <summary>
    /// Логика взаимодействия для ThirdRoleWindow.xaml
    /// </summary>
    public partial class ThirdRoleWindow : Window
    {
        public ThirdRoleWindow()
        {
            InitializeComponent();
        }

        private void btnMovies_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Movies();
        }

        private void btnDirectors_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Directors();
        }

        private void btnGenres_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Genres();
        }

        private void btnScreenTypes_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ScreenTypes();
        }

        private void btnScreens_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Screens();
        }

        private void btnShowTimes_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ShowTimes();
        }

        private void BACKDS_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
