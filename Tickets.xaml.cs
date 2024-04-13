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
    /// Логика взаимодействия для Tickets.xaml
    /// </summary>
    public partial class Tickets : Page
    {
        TicketsTableAdapter tickets = new TicketsTableAdapter();
        ShowtimesTableAdapter shows = new ShowtimesTableAdapter();
        public Tickets()
        {
            InitializeComponent();
            Ticketsdg.ItemsSource = tickets.GetData();
            ShowTimeIDcomboboxD.ItemsSource = shows.GetData();
            ShowTimeIDcomboboxD.DisplayMemberPath = "ShowDate";
            

        }

        private void AddTicDS_Click(object sender, RoutedEventArgs e)
        {
            var ID_Showtime = (int)(ShowTimeIDcomboboxD.SelectedItem as DataRowView).Row[0];
            tickets.InsertQuery(decimal.Parse(PriceboxD.Text),int.Parse(NumberboxD.Text), int.Parse(RowboxD.Text),ID_Showtime);
            Ticketsdg.ItemsSource = tickets.GetData();
        }

        private void UpdateTicDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Ticket = (Ticketsdg.SelectedItem as DataRowView).Row[0];
            var ID_Showtime = (int)(ShowTimeIDcomboboxD.SelectedItem as DataRowView).Row[0];
            tickets.UpdateQuery(decimal.Parse(PriceboxD.Text), int.Parse(NumberboxD.Text), int.Parse(RowboxD.Text), ID_Showtime, Convert.ToInt32(ID_Ticket));
            Ticketsdg.ItemsSource = tickets.GetData();
        }

        private void DelTicDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Screen = (Ticketsdg.SelectedItem as DataRowView).Row[0];
            tickets.DeleteQuery(Convert.ToInt32(ID_Screen));
            Ticketsdg.ItemsSource = tickets.GetData();
        }

        private void Ticketsdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ticketsdg.SelectedItem != null)
            {
                DataRowView selectedRow = Ticketsdg.SelectedItem as DataRowView;
                PriceboxD.Text = selectedRow.Row[1].ToString();
                NumberboxD.Text = selectedRow.Row[2].ToString();
                RowboxD.Text = selectedRow.Row[3].ToString();

                int showtimeid = Convert.ToInt32(selectedRow.Row["Showtime_ID"]);

                foreach (DataRowView item in ShowTimeIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_Showtime"]) == showtimeid)
                    {
                        ShowTimeIDcomboboxD.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void ShowTimeIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShowTimeIDcomboboxD.SelectedItem != null)
            {
                var ID_Ticket = (int)(ShowTimeIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }

        private void PriceboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(PriceboxD.Text))
            {
                PriceboxD.ToolTip = "Цена билета не может быть пустой";
                AddTicDS.IsEnabled = false;
                UpdateTicDS.IsEnabled = false;
            }
            else if (!decimal.TryParse(PriceboxD.Text, out decimal price))
            {
                PriceboxD.ToolTip = "Цена билета должна быть числом";
                AddTicDS.IsEnabled = false;
                UpdateTicDS.IsEnabled = false;
            }
            else
            {
                PriceboxD.ToolTip = null;
                AddTicDS.IsEnabled = true;
                UpdateTicDS.IsEnabled = true;
            }
        }

        private void NumberboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(NumberboxD.Text))
            {
                NumberboxD.ToolTip = "Номер билета не может быть пустым";
                AddTicDS.IsEnabled = false;
                UpdateTicDS.IsEnabled = false;
            }
            else if (!int.TryParse(NumberboxD.Text, out int number))
            {
                NumberboxD.ToolTip = "Номер билета должен быть целым числом";
                AddTicDS.IsEnabled = false;
                UpdateTicDS.IsEnabled = false;
            }
            else
            {
                NumberboxD.ToolTip = null;
                AddTicDS.IsEnabled = true;
                UpdateTicDS.IsEnabled = true;
            }
        }

        private void RowboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(RowboxD.Text))
            {
                RowboxD.ToolTip = "Ряд билета не может быть пустым";
                AddTicDS.IsEnabled = false;
                UpdateTicDS.IsEnabled = false;
            }
            else if (!int.TryParse(RowboxD.Text, out int row))
            {
                RowboxD.ToolTip = "Ряд билета должен быть целым числом";
                AddTicDS.IsEnabled = false;
                UpdateTicDS.IsEnabled = false;
            }
            else
            {
                RowboxD.ToolTip = null;
                AddTicDS.IsEnabled = true;
                UpdateTicDS.IsEnabled = true;
            }
        }
    }
}
