using PRACTICA5.CINEMADataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ShowTimes.xaml
    /// </summary>
    public partial class ShowTimes : Page
    {
        ShowtimesTableAdapter showtimes = new ShowtimesTableAdapter();
        MoviesTableAdapter movies = new MoviesTableAdapter();
        ScreensTableAdapter screens = new ScreensTableAdapter();
        public ShowTimes()
        {
            InitializeComponent();
            Showtimedg.ItemsSource = showtimes.GetData();
            MovIDcomboboxD.ItemsSource = movies.GetData();
            MovIDcomboboxD.DisplayMemberPath = "MovieName";
            ScrIDcomboboxD.ItemsSource = screens.GetData();
            ScrIDcomboboxD.DisplayMemberPath = "ScreenNumber";
        }

        

        private void MovIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MovIDcomboboxD.SelectedItem != null)
            {
                var ID_Movie = (int)(MovIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }

        private void ScrIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScrIDcomboboxD.SelectedItem != null)
            {
                var ID_Screen = (int)(ScrIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }

        private void AddSTDS_Click(object sender, RoutedEventArgs e)
        {
            var ID_Movie = (int)(MovIDcomboboxD.SelectedItem as DataRowView).Row[0];
            var ID_Screen = (int)(ScrIDcomboboxD.SelectedItem as DataRowView).Row[0];
            string showtime = TimeboxD.Text;
            if (showtime == "")
            {
                MessageBox.Show("Не все заполнено ");
            }
            showtimes.InsertQuery(DateboxD.Text, showtime, ID_Movie, ID_Screen);
            Showtimedg.ItemsSource = showtimes.GetData();
        }

        private void UpdateSTDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Showtime = (Showtimedg.SelectedItem as DataRowView).Row[0];
            var ID_Movie = (int)(MovIDcomboboxD.SelectedItem as DataRowView).Row[0];
            var ID_Screen = (int)(ScrIDcomboboxD.SelectedItem as DataRowView).Row[0];
            showtimes.UpdateQuery(DateboxD.Text, TimeboxD.Text, ID_Movie, ID_Screen, Convert.ToInt32(ID_Movie));
            Showtimedg.ItemsSource = showtimes.GetData();
        }

        private void DelSTDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Showtime = (Showtimedg.SelectedItem as DataRowView).Row[0];
            showtimes.DeleteQuery(Convert.ToInt32(ID_Showtime));
            Showtimedg.ItemsSource = movies.GetData();
        }

        private void Showtimedg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Showtimedg.SelectedItem != null)
            {
                DataRowView selectedRow = Showtimedg.SelectedItem as DataRowView;
                DateboxD.Text = selectedRow.Row[1].ToString();
                TimeboxD.Text = selectedRow.Row[2].ToString();
                int movieid = Convert.ToInt32(selectedRow.Row["Movie_ID"]);

                foreach (DataRowView item in MovIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_Movie"]) == movieid)
                    {
                        MovIDcomboboxD.SelectedItem = item;
                        break;
                    }
                }

                int screenid = Convert.ToInt32(selectedRow.Row["Screen_ID"]);

                foreach (DataRowView item in ScrIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_Screen"]) == screenid)
                    {
                        ScrIDcomboboxD.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void DateboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DateboxD.Text))
            {
                DateboxD.ToolTip = "Дата показа не может быть пустой";
                AddSTDS.IsEnabled = false;
                UpdateSTDS.IsEnabled = false;
            }
           
            else
            {
                DateboxD.ToolTip = null;
                AddSTDS.IsEnabled = true;
                UpdateSTDS.IsEnabled = true;
            }
        }

        private void TimeboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TimeboxD.Text))
            {
                TimeboxD.ToolTip = "Время показа не может быть пустым";
                AddSTDS.IsEnabled = false;
                UpdateSTDS.IsEnabled = false;
            }
            
            else
            {
                TimeboxD.ToolTip = null;
                AddSTDS.IsEnabled = true;
                UpdateSTDS.IsEnabled = true;
            }

        }
       
    }
}
