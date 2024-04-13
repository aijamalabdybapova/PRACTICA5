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
    /// Логика взаимодействия для Directors.xaml
    /// </summary>
    
    public partial class Directors : Page
    {
        DirectorsTableAdapter directors = new DirectorsTableAdapter();

        public Directors()
        {
            InitializeComponent();
            Directorsdg.ItemsSource = directors.GetData();

        }

        private void AddDirDS_Click(object sender, RoutedEventArgs e)
        {
            directors.InsertQuery(DirSurnameboxD.Text, DirFirnameboxD.Text);
            Directorsdg.ItemsSource = directors.GetData();
        }

        private void UpdateDirDS_Click(object sender, RoutedEventArgs e)
        {
            
            object ID_Director = (Directorsdg.SelectedItem as DataRowView).Row[0];
            directors.UpdateQuery(DirSurnameboxD.Text, DirFirnameboxD.Text, Convert.ToInt32(ID_Director));
            Directorsdg.ItemsSource = directors.GetData();
        }

        private void DelDirDS_Click(object sender, RoutedEventArgs e)
        {
            object ID_Director = (Directorsdg.SelectedItem as DataRowView).Row[0];
            directors.DeleteQuery(Convert.ToInt32(ID_Director));
            Directorsdg.ItemsSource = directors.GetData();
        }

        private void Directorsdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Directorsdg.SelectedItem != null)
            {
                DataRowView selectedRow = Directorsdg.SelectedItem as DataRowView;
                DirSurnameboxD.Text = selectedRow.Row[1].ToString();
                DirFirnameboxD.Text = selectedRow.Row[2].ToString();
            }
        }

        private void ImportDirDS_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                string jsonText = File.ReadAllText(dialog.FileName);
                List<Director> directorList = JsonConvert.DeserializeObject<List<Director>>(jsonText);

                DirectorsTableAdapter directorsTableAdapter = new DirectorsTableAdapter();

                foreach (Director director in directorList)
                {
                    directors.InsertQuery(director.DirectorSurname, director.DirectorFirstName);
                }

                Directorsdg.ItemsSource = directors.GetData();
                Directorsdg.Columns[1].Header = "Фамилия режиссера";
                Directorsdg.Columns[2].Header = "Имя режиссера";

                MessageBox.Show("Данные успешно импортированы в таблицу");
            }
        }

        private void DirSurnameboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DirSurnameboxD.Text))
            {
                DirSurnameboxD.ToolTip = "Фамилия режиссера не может быть пустой";
                AddDirDS.IsEnabled = false;
                UpdateDirDS.IsEnabled = false;
            }
            else
            {
                DirSurnameboxD.ToolTip = null;
                AddDirDS.IsEnabled = true;
                UpdateDirDS.IsEnabled = true;
            }
        }

        private void DirFirnameboxD_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DirFirnameboxD.Text))
            {
                DirFirnameboxD.ToolTip = "Имя режиссера не может быть пустым";
                AddDirDS.IsEnabled = false;
                UpdateDirDS.IsEnabled = false;
            }
            else
            {
                DirFirnameboxD.ToolTip = null;
                AddDirDS.IsEnabled = true;
                UpdateDirDS.IsEnabled = true;
            }
        }
    }
}
