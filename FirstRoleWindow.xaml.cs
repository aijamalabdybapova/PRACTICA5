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
    /// Логика взаимодействия для FirstRoleWindow.xaml
    /// </summary>
    public partial class FirstRoleWindow : Window
    {
        public FirstRoleWindow()
        {
            InitializeComponent();
        }

        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Roles();
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Employees();
        }

        private void btnLoginPasswords_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new LoginPasswords();
        }

        private void BACKDS_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
