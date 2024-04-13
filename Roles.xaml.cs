using PRACTICA5.CINEMADataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRACTICA5
{
    /// <summary>
    /// Логика взаимодействия для Roles.xaml
    /// </summary>
    public partial class Roles : Page
    {
        RolesTableAdapter roles = new RolesTableAdapter();
        public Roles()
        {
            InitializeComponent();
            Rolesdg.ItemsSource = roles.GetData();
        }

        private void AddRolDS_Click(object sender, RoutedEventArgs e)
        {
            roles.InsertQuery(RolesboxD.Text);
            Rolesdg.ItemsSource = roles.GetData();
        }

        private void UpdateRolDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Role = (Rolesdg.SelectedItem as DataRowView).Row[0];
            roles.UpdateQuery(RolesboxD.Text, Convert.ToInt32(ID_Role));
            Rolesdg.ItemsSource = roles.GetData();
        }

        private void Rolesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Rolesdg.SelectedItem != null)
            {
                DataRowView selectedRow = Rolesdg.SelectedItem as DataRowView;
                RolesboxD.Text = selectedRow.Row[1].ToString();

            }
        }
        private void DelRolDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Role = (Rolesdg.SelectedItem as DataRowView).Row[0];
            roles.DeleteQuery(Convert.ToInt32(ID_Role));
            Rolesdg.ItemsSource = roles.GetData();
        }

        private void RolesboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(RolesboxD.Text))
            {
                RolesboxD.ToolTip = "Название роли не может быть пустым";
                AddRolDS.IsEnabled = false;
                UpdateRolDS.IsEnabled = false;
            }
            else
            {
                RolesboxD.ToolTip = null;
                AddRolDS.IsEnabled = true;
                UpdateRolDS.IsEnabled = true;
            }
        }
    }
}
