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
using System.Data.SqlClient;
using System.IO;

namespace PRACTICA5
{
    /// <summary>
    /// Логика взаимодействия для Kassa.xaml
    /// </summary>
    public partial class Kassa : Page
    {
        SnacksTableAdapter snacksAdapter = new SnacksTableAdapter();
        DataTable selectedSnacksTable = new DataTable();
        decimal totalPrice = 0;

        public Kassa()
        {
            InitializeComponent();
            InitializeSelectedSnacksTable();
            ProductsDgr.ItemsSource = snacksAdapter.GetData();
        }
        private void InitializeSelectedSnacksTable()
        {
            selectedSnacksTable.Columns.Add("Snack_ID", typeof(int));
            selectedSnacksTable.Columns.Add("Name", typeof(string));
            selectedSnacksTable.Columns.Add("Price", typeof(decimal));

            DataDgr.ItemsSource = selectedSnacksTable.DefaultView;
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void Plus(object sender, RoutedEventArgs e)
        {
            if (ProductsDgr.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ProductsDgr.SelectedItem;
                DataRow newRow = selectedSnacksTable.NewRow();
                newRow["Snack_ID"] = selectedRow["ID_Snack"];
                newRow["Name"] = selectedRow["Name"];
                newRow["Price"] = selectedRow["Price"];
                selectedSnacksTable.Rows.Add(newRow);
                totalPrice += (decimal)selectedRow["Price"];
                Money.Text = totalPrice.ToString();
            }
        }

        private void Minus(object sender, RoutedEventArgs e)
        {
            if (DataDgr.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)DataDgr.SelectedItem;
                totalPrice -= (decimal)selectedRow["Price"];
                Money.Text = totalPrice.ToString();
                selectedRow.Delete();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSnacksTable.Rows.Count == 0)
            {
                MessageBox.Show("Вы не выбрали ни одного товара");
                return;
            }

            /// Создание нового чека 
            ChequesTableAdapter chequesAdapter = new ChequesTableAdapter(); 
            chequesAdapter.InsertQuery(DateTime.Now);

            // Получение ID нового чека
            int chequeId = (int)chequesAdapter.GetLastInsertedChequeId;

            // Добавление выбранных товаров в чек
            ChequeSnacksTableAdapter chequeSnacksAdapter = new ChequeSnacksTableAdapter();
            foreach (DataRow row in selectedSnacksTable.Rows)
            {
                chequeSnacksAdapter.InsertQuery(chequeId, (int)row["Snack_ID"]);
            }

            // Выгрузка чека в файл
            string chequeText = $"Чек №{chequeId}\n\n";
            chequeText += $"Дата: {DateTime.Now}\n";
            chequeText += $"________________________________________\n";
            chequeText += $"| Наименование | Цена | Количество | Сумма |\n";
            chequeText += $"|---|---|---|---|";
            decimal subtotalPrice = 0;
            foreach (DataRow row in selectedSnacksTable.Rows)
            {
                subtotalPrice += (decimal)row["Price"];
                chequeText += $"\n| {row["Name"]} | {row["Price"]} | 1 | {row["Price"]} |";
            }
            chequeText += $"\n________________________________________\n";
            chequeText += $"Итого: {subtotalPrice}\n";

            try
            {
                // Сохранение чека в файл
                string fileName = $"Cheque_{chequeId}.txt";
                File.WriteAllText(fileName, chequeText);

                // Очистка таблицы выбранных товаров
                selectedSnacksTable.Rows.Clear();
                totalPrice = 0;
                Money.Text = "0";

                // Вывод сообщения об успешной выгрузке чека
                MessageBox.Show($"Чек успешно выгружен в файл {fileName}");

                // Закрытие окна кассы
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the file write operation
                MessageBox.Show($"Ошибка при выгрузке чека: {ex.Message}");
            }
        }

    }
}
