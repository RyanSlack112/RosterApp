using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosterApp.Models
{
   internal class DBFunctions
    {
        DatabaseService _databaseService;

        public DBFunctions(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public bool Login(string user, string pass)
        {
            SqlConnection connection = _databaseService.ConnectToDB();
            using (SqlCommand cmd = new SqlCommand("SELECT userName, password FROM Users WHERE userName = @username", connection))
            {
                SqlParameter param = new SqlParameter("@username", SqlDbType.VarChar, 50);
                param.Value = user;
                cmd.Parameters.Add(param);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string username = reader.GetString(0); //Username from Database
                        string password = reader.GetString(1); //Password from Database

                        if (username == user && password == pass) //Username and Password Match Check
                        {
                            DatabaseService.CloseConnection(connection);
                            return true; //Successful Login
                        }
                    }
                }
                DatabaseService.CloseConnection(connection);
                return false; //Unsuccessful Login
            }
        }
    }
}
