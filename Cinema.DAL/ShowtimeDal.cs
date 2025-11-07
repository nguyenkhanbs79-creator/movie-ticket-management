using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cinema.Entities;

namespace Cinema.DAL
{
    public class ShowtimeDal
    {
        public List<Showtime> GetAll()
        {
            var showtimes = new List<Showtime>();
            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, MovieId, AuditoriumId, StartTime, BasePrice FROM Showtimes ORDER BY StartTime", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        showtimes.Add(MapShowtime(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }

            return showtimes;
        }

        public Showtime GetById(int id)
        {
            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, MovieId, AuditoriumId, StartTime, BasePrice FROM Showtimes WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapShowtime(reader);
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

        public int Insert(Showtime showtime)
        {
            if (showtime == null)
            {
                throw new ArgumentNullException(nameof(showtime));
            }

            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand(@"INSERT INTO Showtimes (MovieId, AuditoriumId, StartTime, BasePrice)
VALUES (@MovieId, @AuditoriumId, @StartTime, @BasePrice);
SELECT CAST(SCOPE_IDENTITY() AS int);", connection))
                {
                    command.Parameters.AddWithValue("@MovieId", showtime.MovieId);
                    command.Parameters.AddWithValue("@AuditoriumId", showtime.AuditoriumId);
                    command.Parameters.AddWithValue("@StartTime", showtime.StartTime);
                    command.Parameters.AddWithValue("@BasePrice", showtime.BasePrice);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }
        }

        public bool Update(Showtime showtime)
        {
            if (showtime == null)
            {
                throw new ArgumentNullException(nameof(showtime));
            }

            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand(@"UPDATE Showtimes
SET MovieId = @MovieId,
    AuditoriumId = @AuditoriumId,
    StartTime = @StartTime,
    BasePrice = @BasePrice
WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@MovieId", showtime.MovieId);
                    command.Parameters.AddWithValue("@AuditoriumId", showtime.AuditoriumId);
                    command.Parameters.AddWithValue("@StartTime", showtime.StartTime);
                    command.Parameters.AddWithValue("@BasePrice", showtime.BasePrice);
                    command.Parameters.AddWithValue("@Id", showtime.Id);

                    var affected = command.ExecuteNonQuery();
                    return affected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }
        }

        public bool Delete(int id)
        {
            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("DELETE FROM Showtimes WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    var affected = command.ExecuteNonQuery();
                    return affected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }
        }

        public List<Showtime> Search(string titleLike, DateTime? from, DateTime? to)
        {
            var results = new List<Showtime>();
            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("sp_SearchShowtimes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TitleLike", (object)titleLike ?? DBNull.Value);
                    command.Parameters.AddWithValue("@From", (object?)from ?? DBNull.Value);
                    command.Parameters.AddWithValue("@To", (object?)to ?? DBNull.Value);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var showtimeId = reader.GetInt32(reader.GetOrdinal("Id"));
                            var fullShowtime = GetById(showtimeId);
                            if (fullShowtime != null)
                            {
                                results.Add(fullShowtime);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }

            return results;
        }

        private static Showtime MapShowtime(IDataRecord record)
        {
            var showtime = new Showtime();
            showtime.Id = record.GetInt32(record.GetOrdinal("Id"));
            showtime.MovieId = record.GetInt32(record.GetOrdinal("MovieId"));
            showtime.AuditoriumId = record.GetInt32(record.GetOrdinal("AuditoriumId"));
            showtime.StartTime = record.GetDateTime(record.GetOrdinal("StartTime"));
            showtime.BasePrice = record.GetDecimal(record.GetOrdinal("BasePrice"));
            return showtime;
        }
    }
}
