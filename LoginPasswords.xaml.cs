using PRACTICA5.CINEMADataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace PRACTICA5
{
    /// <summary>
    /// Логика взаимодействия для LoginPasswords.xaml
    /// </summary>
    public partial class LoginPasswords : Page
    {
        LoginPasswordsTableAdapter loginpasswords = new LoginPasswordsTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        public LoginPasswords()
        {
            InitializeComponent();
            LoginPassworddg.ItemsSource = loginpasswords.GetData();
            EmplIDcomboboxD.ItemsSource = employees.GetData();
            EmplIDcomboboxD.DisplayMemberPath = "SurName";
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
        private void AddLogDS_Click(object sender, RoutedEventArgs e)
        {
            var ID_Employee = (int)(EmplIDcomboboxD.SelectedItem as DataRowView).Row[0];
            loginpasswords.InsertQuery(LoginboxD.Text, Hash(PasswordboxD.Password), ID_Employee);
            LoginPassworddg.ItemsSource = loginpasswords.GetData();
        }

        private void UpdateLogDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_LoginPassword = (LoginPassworddg.SelectedItem as DataRowView).Row[0];
            var ID_Employee = (int)(EmplIDcomboboxD.SelectedItem as DataRowView).Row[0];
            loginpasswords.UpdateQuery(LoginboxD.Text, Hash(PasswordboxD.Password), ID_Employee, Convert.ToInt32(ID_LoginPassword));
            LoginPassworddg.ItemsSource = loginpasswords.GetData();
        }

        private void DelLogDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_LoginPasswords = (LoginPassworddg.SelectedItem as DataRowView).Row[0];
            loginpasswords.DeleteQuery(Convert.ToInt32(ID_LoginPasswords));
            LoginPassworddg.ItemsSource = loginpasswords.GetData();
        }

        private void EmplIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmplIDcomboboxD.SelectedItem != null)
            {
                var ID_Employee = (int)(EmplIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }

        private void LoginPassworddg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoginPassworddg.SelectedItem != null)
            if (LoginPassworddg.SelectedItem != null)
            {
                DataRowView selectedRow = LoginPassworddg.SelectedItem as DataRowView;
                LoginboxD.Text = selectedRow.Row[1].ToString();
                PasswordboxD.Password = selectedRow.Row[2].ToString();
                int emplid = Convert.ToInt32(selectedRow.Row["Employee_ID"]);

                foreach (DataRowView item in EmplIDcomboboxD.Items)
                {
                        if (Convert.ToInt32(item["ID_Employee"]) == emplid)
                        {
                            EmplIDcomboboxD.SelectedItem = item;
                            break;
                        }
                }

            }
        }

        private void PasswordboxD_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordboxD.Password.Length < 2)
            {
                PasswordboxD.ToolTip = "Пароль должен содержать символы";
                AddLogDS.IsEnabled = false;
                UpdateLogDS.IsEnabled = false;
            }
            else
            {
                PasswordboxD.ToolTip = null;
                AddLogDS.IsEnabled = true;
                UpdateLogDS.IsEnabled = true;
            }
        }

        private void LoginboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginboxD.Text))
            {
                LoginboxD.ToolTip = "Логин не может быть пустым";
                AddLogDS.IsEnabled = false;
                UpdateLogDS.IsEnabled = false;
            }
            else
            {
                LoginboxD.ToolTip = null;
                AddLogDS.IsEnabled = true;
                UpdateLogDS.IsEnabled = true;
            }
        }
    }
}
