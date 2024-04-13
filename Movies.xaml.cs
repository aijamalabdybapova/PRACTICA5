using PRACTICA5.CINEMADataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Movies.xaml
    /// </summary>
    public partial class Movies : Page
    {
        MoviesTableAdapter movies = new MoviesTableAdapter();
        GenresTableAdapter genres = new GenresTableAdapter();
        DirectorsTableAdapter directors = new DirectorsTableAdapter();
        public Movies()
        {
            InitializeComponent();
            Moviesdg.ItemsSource = movies.GetData();
            DirIDcomboboxD.ItemsSource = directors.GetData();
            DirIDcomboboxD.DisplayMemberPath = "DirectorSurName";
            GenIDcomboboxD.ItemsSource = genres.GetData();
            GenIDcomboboxD.DisplayMemberPath = "GenreName";
        }


        private void DirIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DirIDcomboboxD.SelectedItem != null)
            {
                var ID_Director = (int)(DirIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }

        private void GenIDcomboboxD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenIDcomboboxD.SelectedItem != null)
            {
                var ID_Genre = (int)(GenIDcomboboxD.SelectedItem as DataRowView).Row[0];
            }
        }
        private void AddMovDS_Click(object sender, RoutedEventArgs e)
        {
            var ID_Genre = (int)(GenIDcomboboxD.SelectedItem as DataRowView).Row[0];
            var ID_Director = (int)(DirIDcomboboxD.SelectedItem as DataRowView).Row[0];
            string movietime = TimesboxD.Text;
            if (movietime == "")
            {
                MessageBox.Show("Не все заполнено ");
            }
            movies.InsertQuery(MoviesboxD.Text, movietime, ID_Director, ID_Genre);
            Moviesdg.ItemsSource = movies.GetData();
        }

        private void DelMovDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Movie = (Moviesdg.SelectedItem as DataRowView).Row[0];
            movies.DeleteQuery(Convert.ToInt32(ID_Movie));
            Moviesdg.ItemsSource = movies.GetData();
        }

        private void UpdateMovDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Movie = (Moviesdg.SelectedItem as DataRowView).Row[0];
            var ID_Genre = (int)(GenIDcomboboxD.SelectedItem as DataRowView).Row[0];
            var ID_Director = (int)(DirIDcomboboxD.SelectedItem as DataRowView).Row[0];
            movies.UpdateQuery(MoviesboxD.Text, TimesboxD.Text, ID_Director, ID_Genre, Convert.ToInt32(ID_Movie));
            Moviesdg.ItemsSource = movies.GetData();
        }

        private void Moviesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Moviesdg.SelectedItem != null)
            {
                DataRowView selectedRow = Moviesdg.SelectedItem as DataRowView;
                MoviesboxD.Text = selectedRow.Row[1].ToString();
                TimesboxD.Text = selectedRow.Row[2].ToString();
                int directorid = Convert.ToInt32(selectedRow.Row["Director_ID"]);

                foreach (DataRowView item in DirIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_Director"]) == directorid)
                    {
                        DirIDcomboboxD.SelectedItem = item;
                        break;
                    }
                }

                int genreid = Convert.ToInt32(selectedRow.Row["Genre_ID"]);

                foreach (DataRowView item in GenIDcomboboxD.Items)
                {
                    if (Convert.ToInt32(item["ID_Genre"]) == genreid)
                    {
                        GenIDcomboboxD.SelectedItem = item;
                        break;
                    }
                }

            }
        }

        private void MoviesboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(MoviesboxD.Text))
            {
                MoviesboxD.ToolTip = "Название фильма не может быть пустым";
                AddMovDS.IsEnabled = false;
                UpdateMovDS.IsEnabled = false;
            }
            else
            {
                MoviesboxD.ToolTip = null;
                AddMovDS.IsEnabled = true;
                UpdateMovDS.IsEnabled = true;
            }
        }

        private void TimesboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TimesboxD.Text))
            {
                TimesboxD.ToolTip = "Продолжительность фильма не может быть пустой";
                AddMovDS.IsEnabled = false;
                UpdateMovDS.IsEnabled = false;
            }
            else if (!IsValidTimeFormat(TimesboxD.Text))
            {
                TimesboxD.ToolTip = "Продолжительность фильма должна быть в формате чч:мм:сс";
                AddMovDS.IsEnabled = false;
                UpdateMovDS.IsEnabled = false;
            }
            else
            {
                TimesboxD.ToolTip = null;
                AddMovDS.IsEnabled = true;
                UpdateMovDS.IsEnabled = true;
            }
        }
        private bool IsValidTimeFormat(string time)
        {
            return Regex.Match(time, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$").Success;
        }
    }
}
