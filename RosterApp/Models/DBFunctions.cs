using Microsoft.Data.SqlClient;
using RosterApp.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosterApp.Models
{
    class DBFunctions
    {
        DatabaseService _databaseService; //Object of Database Service Class

        //Constructor
        public DBFunctions(DatabaseService databaseService)
        {
            _databaseService = databaseService; //Instantiate Database Service Object
        }

        /*
         * Login Function
         * Takes a Username and Password string and checks for an entry in the database matching the username.
         * If user is found, password string is checked if it matches the password in the database.
         * If the check is successful, return a true boolean. Otherwise, return false.
         */
        public bool Login(string user, string pass)
        {
            using (SqlConnection connection = _databaseService.ConnectToDB())
            {
                string query = "SELECT username, password FROM Users WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", user);
                    using (SqlDataReader reader = cmd.ExecuteReader()) //Retrieve entries from Database
                    {
                        if (reader.Read())
                        {
                            string username = reader.GetString(0); //Username from Database
                            string password = reader.GetString(1); //Password from Database

                            if (SecureLogin.VerifyPassword(pass, password)) //Username and Password Match Check
                            {
                                return true; //Successful Login
                            }
                        }
                    }
                }
                return false; //Unsuccessful Login
            }
        }

        public bool Register(string username, string firstname, string lastname, string email, string password)
        {
            using (SqlConnection connection = _databaseService.ConnectToDB())
            {
                string query = "INSERT INTO Users(username, firstName, lastName, email, password) VALUES (@username, @firstName, @lastName, @email, @password)";
                string hashedPassword = SecureLogin.HashPassword(password);
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@firstName", firstname);
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool AddToRoster(string username, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            using (SqlConnection connection = _databaseService.ConnectToDB())
            {
                string query = "INSERT INTO RosterData(username, day, startTime, endTime) VALUES (@username, @day, @startTIme, @endTime)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@day", date);
                    cmd.Parameters.AddWithValue("@startTime", startTime);
                    cmd.Parameters.AddWithValue("@endTime", endTime);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public List<RosterData> GetRosterEntries(string username, DateTime date)
        {
            DateTime day;
            TimeSpan startTime;
            TimeSpan endTime;
            List<RosterData> rosterDataList = new List<RosterData>(); //List of RosterData Objects>

            using (SqlConnection connection = _databaseService.ConnectToDB())
            {
                day = date;
                string query = "SELECT day, startTime, endTime FROM RosterData WHERE username = @username AND day = @day";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@day", day);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            startTime = reader.GetTimeSpan(1);
                            endTime = reader.GetTimeSpan(2);

                            RosterData rosterData = new RosterData(username, day, startTime, endTime);
                            rosterDataList.Add(rosterData);
                        }
                    }
                }
            }
            return rosterDataList;
        }
    }
}
