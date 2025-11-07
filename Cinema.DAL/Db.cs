using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Cinema.DAL
{
    public class Db
    {
        private readonly string _connStr;

        public Db()
        {
            var settings = ConfigurationManager.ConnectionStrings["CinemaDb"];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new InvalidOperationException("Connection string 'CinemaDb' is not configured.");
            }

            _connStr = settings.ConnectionString;
        }

        public SqlConnection Open()
        {
            try
            {
                var conn = new SqlConnection(_connStr);
                conn.Open();
                return conn;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Cannot open SQL connection: " + ex.Message, ex);
            }
        }

        public bool TestConnection(out string message)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                }

                message = "Connection successful.";
                return true;
            }
            catch (SqlException ex)
            {
                message = "Connection failed: " + ex.Message;
                return false;
            }
        }
    }
}
