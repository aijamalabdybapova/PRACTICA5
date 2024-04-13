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
    /// Логика взаимодействия для Screens.xaml
    /// </summary>
    public partial class Screens : Page
        
    {
        ScreensTableAdapter screens = new ScreensTableAdapter();
        ScreensTypesTableAdapter types= new ScreensTypesTableAdapter();
        public Screens()
        {
            InitializeComponent();
            Screensdg.ItemsSource = screens.GetData();
            TypeIDcomboboxD.ItemsSource = types.GetData();
            TypeIDcomboboxD.DisplayMemberPath = "ScreenTypes";
        }

        private void AddScreenDS_Click(object sender, RoutedEventArgs e)
        {
            var ID_Type = (int)(TypeIDcomboboxD.SelectedItem as DataRowView).Row[0];
            screens.InsertQuery(int.Parse(NumberboxD.Text), int.Parse(CapacityboxD.Text), int.Parse(FreeSpacesboxD.Text), ID_Type);
            Screensdg.ItemsSource = screens.GetData();
        }

        private void UpdateScreenDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Screen = (Screensdg.SelectedItem as DataRowView).Row[0];
            var ID_Type = (int)(TypeIDcomboboxD.SelectedItem as DataRowView).Row[0];
            screens.UpdateQuery(int.Parse(NumberboxD.Text), int.Parse(CapacityboxD.Text), int.Parse(FreeSpacesboxD.Text), ID_Type, Convert.ToInt32(ID_Screen));
            Screensdg.ItemsSource = screens.GetData();
        }

        private void DelScreenDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Screen = (Screensdg.SelectedItem as DataRowView).Row[0];
            screens.DeleteQuery(Convert.ToInt32(ID_Screen));
            Screensdg.ItemsSource = screens.GetData();
        }

        private void Screensdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Screensdg.SelectedItem != null)
            {
                DataRowView selectedRow = Screensdg.SelectedItem as DataRowView;
                NumberboxD.Text = selectedRow.Row[1].ToString();
                CapacityboxD.Text = selectedRow.Row[2].ToString();
                FreeSpacesboxD.Text = selectedRow.Row[3].ToString();
                int screentypeid = Convert.ToInt32(selectedRow.Row["ScreenType_ID"]);

                foreach (DataRowView item in TypeIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_ScreenType"]) == screentypeid)
                    {
                        TypeIDcomboboxD.SelectedItem = item;
                        break;
                    }
                }

            }
        }

        private void TypeIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeIDcomboboxD.SelectedItem != null)
            {
                var ID_Screen = (int)(TypeIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }
    }
}
