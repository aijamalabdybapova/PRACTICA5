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
    /// Логика взаимодействия для Snacks.xaml
    /// </summary>
    public partial class Snacks : Page
    {
        SnacksTableAdapter snacks = new SnacksTableAdapter();
        public Snacks()
        {
            InitializeComponent();
            Snacksdg.ItemsSource = snacks.GetData();
        }

        private void AddSnDS_Click(object sender, RoutedEventArgs e)
        {
            snacks.InsertQuery(SnakeboxD.Text, decimal.Parse(PriceboxD.Text));
            Snacksdg.ItemsSource = snacks.GetData();
        }

        private void UpdateSnDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Snack = (Snacksdg.SelectedItem as DataRowView).Row[0];
            snacks.UpdateQuery(SnakeboxD.Text, decimal.Parse(PriceboxD.Text), Convert.ToInt32(ID_Snack));
            Snacksdg.ItemsSource = snacks.GetData();
        }

        private void DelSnDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Snack = (Snacksdg.SelectedItem as DataRowView).Row[0];
            snacks.DeleteQuery(Convert.ToInt32(ID_Snack));
            Snacksdg.ItemsSource = snacks.GetData();
            
        }

        private void Snacksdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Snacksdg.SelectedItem != null)
            {
                DataRowView selectedRow = Snacksdg.SelectedItem as DataRowView;
                SnakeboxD.Text = selectedRow.Row[1].ToString();
                PriceboxD.Text = selectedRow.Row[2].ToString();
            }
        }

        private void SnakeboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SnakeboxD.Text))
            {
                SnakeboxD.ToolTip = "Название закуски не может быть пустым";
                AddSnDS.IsEnabled = false;
                UpdateSnDS.IsEnabled = false;
            }
            else
            {
                SnakeboxD.ToolTip = null;
                AddSnDS.IsEnabled = true;
                UpdateSnDS.IsEnabled = true;
            }
        }

        private void PriceboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(PriceboxD.Text))
            {
                PriceboxD.ToolTip = "Цена закуски не может быть пустой";
                AddSnDS.IsEnabled = false;
                UpdateSnDS.IsEnabled = false;
            }
            else if (!decimal.TryParse(PriceboxD.Text, out decimal price))
            {
                PriceboxD.ToolTip = "Цена закуски должна быть числом";
                AddSnDS.IsEnabled = false;
                UpdateSnDS.IsEnabled = false;
            }
            else
            {
                PriceboxD.ToolTip = null;
                AddSnDS.IsEnabled = true;
                UpdateSnDS.IsEnabled = true;
            }
        }
    }
}
