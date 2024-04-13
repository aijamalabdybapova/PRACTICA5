using System.Data.SqlClient;
using System.Data;
using System;

namespace PRACTICA5
{

    public partial class ChequesTableAdapter
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public ChequesTableAdapter()
        {
            _connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=CINEMA;Integrated Security=True;TrustServerCertificate=True");
            _command = new SqlCommand("INSERT INTO Cheques (Date) VALUES (@Date); SELECT SCOPE_IDENTITY()", _connection);
            _command.Parameters.Add("@Date", SqlDbType.DateTime);
        }

        public void InsertQuery(DateTime date)
        {
            _command.Parameters["@Date"].Value = date;

            _connection.Open();
            int insertedId = Convert.ToInt32(_command.ExecuteScalar());
            _connection.Close();

            // Set the ID of the last inserted cheque
            GetLastInsertedChequeId = insertedId;
        }

        public int GetLastInsertedChequeId
        {
            get;
            private set;
        }
    }

}