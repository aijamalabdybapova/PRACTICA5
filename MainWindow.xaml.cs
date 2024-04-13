using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PRACTICA5.CINEMADataSetTableAdapters;

namespace PRACTICA5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginPasswordsTableAdapter adapter = new LoginPasswordsTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private static string Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                string sb = "";
                for (int i = 0; i < hash.Length; i++)
                {
                    sb += hash[i].ToString("x2");
                }
                return sb;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var allLogins = adapter.GetData().Rows;
            for (int i = 0; i < allLogins.Count; i++)
            {
                if (allLogins[i][1].ToString() == LoginTbx.Text &&
                    allLogins[i][2].ToString() == Hash(PasswordTbx.Password))
                {
                    int roleid = (int)allLogins[i][3];
                    switch (roleid)
                    {
                        case 1:
                            FirstRoleWindow role = new FirstRoleWindow();
                            role.Show();
                            this.Close();
                            break;
                        case 2:
                            SecondRoleWindow second = new SecondRoleWindow();
                            second.Show();
                            this.Close();
                            break;
                        case 4:
                            ThirdRoleWindow kinoprocat = new ThirdRoleWindow();
                            kinoprocat.Show();
                            this.Close();
                            break;
                        case 5:
                            FourWindow admin2 = new FourWindow();
                            admin2.Show();
                            this.Close();
                            break;
                   
                    }
                }
            }
        }

        private void LoginTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTbx.Text))
            {
                LoginTbx.ToolTip = "Логин не может быть пустым";
                LoginButton.IsEnabled = false;
            }
            else
            {
                LoginTbx.ToolTip = null;
                LoginButton.IsEnabled = true;
            }
        }

        private void PasswordTbx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordTbx.Password.Length < 2)
            {
                PasswordTbx.ToolTip = "Пароль должен содержать не менее 2 символов";
                LoginButton.IsEnabled = false;
            }
            else
            {
                PasswordTbx.ToolTip = null;
                LoginButton.IsEnabled = true;
            }
        }
    }
}
