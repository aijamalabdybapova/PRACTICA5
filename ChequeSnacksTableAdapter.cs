using System.Data.SqlClient;
using System.Data;

namespace PRACTICA5
{
    public class ChequeSnacksTableAdapter
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public ChequeSnacksTableAdapter()
        {
            _connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=CINEMA;Integrated Security=True;TrustServerCertificate=True");
            _command = new SqlCommand("INSERT INTO ChequeSnacks (Cheque_ID, Snack_ID, Quantity) VALUES (@Cheque_ID, @Snack_ID, @Quantity)", _connection);
            _command.Parameters.Add("@Cheque_ID", SqlDbType.Int);
            _command.Parameters.Add("@Snack_ID", SqlDbType.Int);
            _command.Parameters.Add("@Quantity", SqlDbType.Int);
        }

        public void InsertQuery(int chequeId, int snackId)
        {
            _command.Parameters["@Cheque_ID"].Value = chequeId;
            _command.Parameters["@Snack_ID"].Value = snackId;
            _command.Parameters["@Quantity"].Value = 1;

            _connection.Open();
            _command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}