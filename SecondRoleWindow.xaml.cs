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
    /// Логика взаимодействия для SecondRoleWindow.xaml
    /// </summary>
    public partial class SecondRoleWindow : Window
    {
        public SecondRoleWindow()
        {
            InitializeComponent();
        }

        private void btnTickets_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Tickets();
        }

        private void btnSnacks_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Snacks();
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Orders();
        }

        private void BACKDS_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnKassa_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Kassa();
        }
    }
}
