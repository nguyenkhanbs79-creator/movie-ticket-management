using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cinema.Entities;

namespace Cinema.DAL
{
    public class AuditoriumDal
    {
        public List<Auditorium> GetAll()
        {
            var auditoriums = new List<Auditorium>();
            try
            {
                var db = new Db();
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, Name, SeatCount FROM Auditoriums", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var auditorium = new Auditorium
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            SeatCount = reader.GetInt32(reader.GetOrdinal("SeatCount"))
                        };
                        auditoriums.Add(auditorium);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }

            return auditoriums;
        }

        public Auditorium GetById(int id)
        {
            try
            {
                var db = new Db();
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, Name, SeatCount FROM Auditoriums WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            return new Auditorium
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                SeatCount = reader.GetInt32(reader.GetOrdinal("SeatCount"))
                            };
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
    }
}
