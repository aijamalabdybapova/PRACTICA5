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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        OrdersTableAdapter orders = new OrdersTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        public Orders()
        {
            InitializeComponent();
            Ordersdg.ItemsSource = orders.GetData();
            EmpIDcomboboxD.ItemsSource = employees.GetData();
            EmpIDcomboboxD.DisplayMemberPath = "SurName";
        }

        private void AdOrDS_Click(object sender, RoutedEventArgs e)
        {

            if (OrderDateboxD.Text == "" || QuantityboxD.Text == "" || TotalCostboxD.Text == "" || PayMentboxD.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            try
            {
                var ID_Employee = (int)(EmpIDcomboboxD.SelectedItem as DataRowView).Row[0];
                orders.InsertQuery(DateTime.Parse(OrderDateboxD.Text), int.Parse(QuantityboxD.Text), decimal.Parse(TotalCostboxD.Text), PayMentboxD.Text, ID_Employee);
                Ordersdg.ItemsSource = employees.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateOrlDS_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDateboxD.Text == "" || QuantityboxD.Text == "" || TotalCostboxD.Text == "" || PayMentboxD.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            try
            {
                object ID_Order = (Ordersdg.SelectedItem as DataRowView).Row[0];
                var ID_Employee = (int)(EmpIDcomboboxD.SelectedItem as DataRowView).Row[0];
                orders.UpdateQuery(DateTime.Parse(OrderDateboxD.Text), int.Parse(QuantityboxD.Text), decimal.Parse(TotalCostboxD.Text), PayMentboxD.Text, ID_Employee, Convert.ToInt32(ID_Order));
                Ordersdg.ItemsSource = orders.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DelOrlDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Order = (Ordersdg.SelectedItem as DataRowView).Row[0];
            orders.DeleteQuery(Convert.ToInt32(ID_Order));
            Ordersdg.ItemsSource = orders.GetData();
        }

        private void EmpIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmpIDcomboboxD.SelectedItem != null)
            {
                var ID_Employee = (int)(EmpIDcomboboxD.SelectedItem as DataRowView).Row[0];
                MessageBox.Show("Выберите сотрудника!");
                return;
            }
        }

        private void Ordersdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ordersdg.SelectedItem != null)
            {
                DataRowView selectedRow = Ordersdg.SelectedItem as DataRowView;
                OrderDateboxD.Text = selectedRow.Row[1].ToString();
                QuantityboxD.Text = selectedRow.Row[2].ToString();
                TotalCostboxD.Text = selectedRow.Row[3].ToString();
                PayMentboxD.Text = selectedRow.Row[4].ToString();
                int employeeid = Convert.ToInt32(selectedRow.Row["Employee_ID"]);

                foreach (DataRowView item in EmpIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_Employee"]) == employeeid)
                    {
                        EmpIDcomboboxD.SelectedItem = item;
                        break;
                    }
                }

            }
        }

        
    }
}
