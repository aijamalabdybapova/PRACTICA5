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
    /// Логика взаимодействия для ScreenTypes.xaml
    /// </summary>
    public partial class ScreenTypes : Page
    {
        ScreensTypesTableAdapter types = new ScreensTypesTableAdapter();
        public ScreenTypes()
        {
            InitializeComponent();
            Typesdg.ItemsSource = types.GetData();
        }

        private void AddTypeDS_Click(object sender, RoutedEventArgs e)
        {
            types.InsertQuery(ScreenTypesboxD.Text, decimal.Parse(CostboxD.Text));
            Typesdg.ItemsSource = types.GetData();
        }

        private void UpdateTypeDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Genre = (Typesdg.SelectedItem as DataRowView).Row[0];
            types.UpdateQuery(ScreenTypesboxD.Text, decimal.Parse(CostboxD.Text), Convert.ToInt32(ID_Genre));
            Typesdg.ItemsSource = types.GetData();
        }

        private void DelTypeDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Type = (Typesdg.SelectedItem as DataRowView).Row[0];
            types.DeleteQuery(Convert.ToInt32(ID_Type));
            Typesdg.ItemsSource = types.GetData();
        }

        private void Typesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Typesdg.SelectedItem != null)
            {
                DataRowView selectedRow = Typesdg.SelectedItem as DataRowView;
                ScreenTypesboxD.Text = selectedRow.Row[1].ToString();
                CostboxD.Text = selectedRow.Row[2].ToString();

            }
        }

        private void ScreenTypesboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ScreenTypesboxD.Text))
            {
                ScreenTypesboxD.ToolTip = "Название типа экрана не может быть пустым";
                AddTypeDS.IsEnabled = false;
                UpdateTypeDS.IsEnabled = false;
            }
            else
            {
                ScreenTypesboxD.ToolTip = null;
                AddTypeDS.IsEnabled = true;
                UpdateTypeDS.IsEnabled = true;
            }
        }

        private void CostboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(CostboxD.Text))
            {
                CostboxD.ToolTip = "Стоимость типа экрана не может быть пустой";
                AddTypeDS.IsEnabled = false;
                UpdateTypeDS.IsEnabled = false;
            }
            else if (!decimal.TryParse(CostboxD.Text, out decimal cost))
            {
                CostboxD.ToolTip = "Стоимость типа экрана должна быть числом";
                AddTypeDS.IsEnabled = false;
                UpdateTypeDS.IsEnabled = false;
            }
            else
            {
                CostboxD.ToolTip = null;
                AddTypeDS.IsEnabled = true;
                UpdateTypeDS.IsEnabled = true;
            }
        }
    }
}
