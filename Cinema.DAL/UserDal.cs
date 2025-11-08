using System;
using System.Data;
using System.Data.SqlClient;
using Cinema.Entities;

namespace Cinema.DAL
{
    public class UserDal
    {
        public User? GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username is required.", nameof(username));
            }

            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, Username, PasswordHash, PasswordKdf, FullName, Email, Phone FROM Users WHERE Username = @u", connection))
                {
                    command.Parameters.AddWithValue("@u", username);

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

        public void UpdatePasswordCredential(int userId, string passwordCredential)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(passwordCredential))
            {
                throw new ArgumentException("Password credential is required.", nameof(passwordCredential));
            }

            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("UPDATE Users SET PasswordKdf = @kdf WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@kdf", passwordCredential);
                    command.Parameters.AddWithValue("@id", userId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }
        }

        private static User MapUser(IDataRecord record)
        {
            var user = new User
            {
                Id = record.GetInt32(record.GetOrdinal("Id")),
                Username = record.GetString(record.GetOrdinal("Username")),
                FullName = record.IsDBNull(record.GetOrdinal("FullName")) ? "Unknown" : record.GetString(record.GetOrdinal("FullName")),
                Email = record.IsDBNull(record.GetOrdinal("Email")) ? null : record.GetString(record.GetOrdinal("Email")),
                Phone = record.IsDBNull(record.GetOrdinal("Phone")) ? null : record.GetString(record.GetOrdinal("Phone"))
            };

            if (!record.IsDBNull(record.GetOrdinal("PasswordHash")))
            {
                user.PasswordHash = record.GetString(record.GetOrdinal("PasswordHash"));
            }

            int passwordKdfOrdinal;
            try
            {
                passwordKdfOrdinal = record.GetOrdinal("PasswordKdf");
                if (!record.IsDBNull(passwordKdfOrdinal))
                {
                    user.PasswordKdf = record.GetString(passwordKdfOrdinal);
                }
            }
            catch (IndexOutOfRangeException)
            {
                user.PasswordKdf = null;
            }

            return user;
        }
    }
}
