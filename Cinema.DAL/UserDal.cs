using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cinema.Entities;

namespace Cinema.DAL
{
    public class UserDal
    {
        public User Login(string username, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username is required.", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password hash is required.", nameof(passwordHash));
            }

            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, Username, PasswordHash, FullName, Email, Phone FROM Users WHERE Username = @u AND PasswordHash = @p", connection))
                {
                    command.Parameters.AddWithValue("@u", username);
                    command.Parameters.AddWithValue("@p", passwordHash);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUser(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }

            return null;
        }

        private static User MapUser(IDataRecord record)
        {
            var user = new User();
            user.Id = record.GetInt32(record.GetOrdinal("Id"));
            user.Username = record.GetString(record.GetOrdinal("Username"));
            user.PasswordHash = record.GetString(record.GetOrdinal("PasswordHash"));
            if (!record.IsDBNull(record.GetOrdinal("FullName")))
            {
                user.FullName = record.GetString(record.GetOrdinal("FullName"));
            }
            else
            {
                user.FullName = "Unknown";
            }
            if (!record.IsDBNull(record.GetOrdinal("Email")))
            {
                user.Email = record.GetString(record.GetOrdinal("Email"));
            }
            if (!record.IsDBNull(record.GetOrdinal("Phone")))
            {
                user.Phone = record.GetString(record.GetOrdinal("Phone"));
            }

            return user;
        }
    }
}
