using PRACTICA5.CINEMADataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : Page
    {
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        RolesTableAdapter roles = new RolesTableAdapter();
        public Employees()
        {
            InitializeComponent();
            Employeesdg.ItemsSource = employees.GetData();
            EmpIDcomboboxD.ItemsSource = roles.GetData();
            EmpIDcomboboxD.DisplayMemberPath = "RoleName";
        }

        private void AdEmpDS_Click(object sender, RoutedEventArgs e)
        {
            var ID_Role = (int)(EmpIDcomboboxD.SelectedItem as DataRowView).Row[0];
            employees.InsertQuery(EmpSurnameboxD.Text, EmpFirnameboxD.Text, EmpMidnameboxD.Text,ID_Role);
            Employeesdg.ItemsSource = employees.GetData();
        }

        private void UpdateEmplDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Employee = (Employeesdg.SelectedItem as DataRowView).Row[0];
            var ID_Role = (int)(EmpIDcomboboxD.SelectedItem as DataRowView).Row[0];
            employees.UpdateQuery(EmpSurnameboxD.Text, EmpFirnameboxD.Text, EmpMidnameboxD.Text,ID_Role, Convert.ToInt32(ID_Employee));
            Employeesdg.ItemsSource = employees.GetData();
        }

        private void DelEmplDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Employee = (Employeesdg.SelectedItem as DataRowView).Row[0];
            employees.DeleteQuery(Convert.ToInt32(ID_Employee));
            Employeesdg.ItemsSource = employees.GetData();
        }

        private void Employeesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Employeesdg.SelectedItem != null)
            {
                DataRowView selectedRow = Employeesdg.SelectedItem as DataRowView;
                EmpSurnameboxD.Text = selectedRow.Row[1].ToString();
                EmpFirnameboxD.Text = selectedRow.Row[2].ToString();
                EmpMidnameboxD.Text = selectedRow.Row[3].ToString();
                int roleid = Convert.ToInt32(selectedRow.Row["Role_ID"]);

                foreach (DataRowView item in EmpIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_Role"]) == roleid)
                    {
                        EmpIDcomboboxD.SelectedItem = item;
                        break;
                    }  
                }

            }
        }

        private void EmpIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmpIDcomboboxD.SelectedItem != null)
            {
                var ID_Roles = (int)(EmpIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }

        private void EmpSurnameboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmpSurnameboxD.Text))
            {
                EmpSurnameboxD.ToolTip = "Фамилия сотрудника не может быть пустой";
                AdEmpDS.IsEnabled = false;
                UpdateEmplDS.IsEnabled = false;
            }
            else
            {
                EmpSurnameboxD.ToolTip = null;
                AdEmpDS.IsEnabled = true;
                UpdateEmplDS.IsEnabled = true;
            }
        }

        private void EmpFirnameboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmpFirnameboxD.Text))
            {
                EmpFirnameboxD.ToolTip = "Имя сотрудника не может быть пустым";
                AdEmpDS.IsEnabled = false;
                UpdateEmplDS.IsEnabled = false;
            }
            else
            {
                EmpFirnameboxD.ToolTip = null;
                AdEmpDS.IsEnabled = true;
                UpdateEmplDS.IsEnabled = true;
            }
        }

        private void EmpMidnameboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmpMidnameboxD.Text))
            {
                EmpMidnameboxD.ToolTip = "Отчество сотрудника не может быть пустым";
                AdEmpDS.IsEnabled = false;
                UpdateEmplDS.IsEnabled = false;
            }
            else
            {
                EmpMidnameboxD.ToolTip = null;
                AdEmpDS.IsEnabled = true;
                UpdateEmplDS.IsEnabled = true;
            }
        }
    }
}
