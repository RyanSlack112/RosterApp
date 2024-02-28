using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosterApp.Models
{
    class DatabaseService
    {
        string connectionString;

        public DatabaseService() 
        {
            connectionString = "Server=192.168.50.110;Database=RosterApp;User Id=Test;Password=P@ssword121;Encrypt=false";
        }

        /*
         * Connects to the database and returns an object of that connection
         */
        public SqlConnection ConnectToDB()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {

            }

            return connection;
        }

        /*
         * Used for closing a connection to the database
         */
        public void CloseConnection(SqlConnection connection)
        {
            if(connection != null && connection.State != System.Data.ConnectionState.Closed) 
            {
                connection.Close();
            }
        }

        /*
         * Function to test whether the database is correctly connected
         */
        public bool CheckDatabaseConnection(SqlConnection connection)
        {
            if(connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            return false;
        }
    }
}
