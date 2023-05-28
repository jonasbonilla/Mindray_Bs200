using Data.Properties;
using Npgsql; 
using System.Data; 

namespace Data
{
    public class PgConnection
    {
        private readonly NpgsqlConnection _generalConnection;
        public PgConnection()
        {
            _generalConnection = new NpgsqlConnection(Resources.Localhost); 
        }
        public NpgsqlConnection OpenNpgsqlConnection()
        {
            try
            {
                if (_generalConnection.State == ConnectionState.Closed)
                    _generalConnection.Open();
            }
            catch (NpgsqlException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return _generalConnection;
        }
        public void CloseNpgsqlConnection()
        {
            try
            {
                if (_generalConnection != null && _generalConnection.State == ConnectionState.Open)
                    _generalConnection.Close();
            }
            catch (NpgsqlException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        } 
    }
}
