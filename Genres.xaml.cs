using PRACTICA5.CINEMADataSetTableAdapters;
using Microsoft.WindowsAPICodePack.Dialogs;
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
using Newtonsoft.Json;
using System.IO;

namespace PRACTICA5
{
    /// <summary>
    /// Логика взаимодействия для Genres.xaml
    /// </summary>
    public partial class Genres : Page
    {
        GenresTableAdapter genres = new GenresTableAdapter();
        public Genres()
        {
            InitializeComponent();
            Genresdg.ItemsSource = genres.GetData();
        }
        private void AddGenDS_Click(object sender, RoutedEventArgs e)
        {
            genres.InsertQuery(GenresboxD.Text);
            Genresdg.ItemsSource = genres.GetData();
        }

        private void DelGenDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Genre = (Genresdg.SelectedItem as DataRowView).Row[0];
            genres.DeleteQuery(Convert.ToInt32(ID_Genre));
            Genresdg.ItemsSource = genres.GetData();

        }
        private void UpdateGenDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Genre = (Genresdg.SelectedItem as DataRowView).Row[0];
            genres.UpdateQuery(GenresboxD.Text, Convert.ToInt32(ID_Genre));
            Genresdg.ItemsSource = genres.GetData();
        }

        private void Genresdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Genresdg.SelectedItem != null)
            {
                DataRowView selectedRow = Genresdg.SelectedItem as DataRowView;
                GenresboxD.Text = selectedRow.Row[1].ToString();

            }
        }

        private void ImportGenDS_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                string jsonText = File.ReadAllText(dialog.FileName);
                List<Genre> genreList = JsonConvert.DeserializeObject<List<Genre>>(jsonText);

                GenresTableAdapter directorsTableAdapter = new GenresTableAdapter();

                foreach (Genre genre in genreList)
                {
                    genres.InsertQuery(genre.GenreName);
                }

                Genresdg.ItemsSource = genres.GetData();
                Genresdg.Columns[1].Header = "Жанр";

                MessageBox.Show("Данные успешно импортированы в таблицу");
            }
        }

        private void GenresboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(GenresboxD.Text))
            {
                GenresboxD.ToolTip = "Название жанра не может быть пустым";
                AddGenDS.IsEnabled = false;
                UpdateGenDS.IsEnabled = false;
            }
            else
            {
                GenresboxD.ToolTip = null;
                AddGenDS.IsEnabled = true;
                UpdateGenDS.IsEnabled = true;
            }
        }
    }
}
