using MySql.Data.MySqlClient;

namespace UniversityManager.Utils
{
    public class DataBaseConnector
    {
        private const string _connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=root";
        public MySqlConnection Connection { get; }

        public DataBaseConnector()
        {
            Connection = new MySqlConnection(_connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                Connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }

        }
    }
}
