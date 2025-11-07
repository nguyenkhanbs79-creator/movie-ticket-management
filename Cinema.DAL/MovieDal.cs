using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cinema.Entities;

namespace Cinema.DAL
{
    public class MovieDal
    {
        public List<Movie> GetAll()
        {
            var movies = new List<Movie>();
            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, Title, Genre, Duration, AgeRating FROM Movies ORDER BY Title", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(MapMovie(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }

            return movies;
        }

        public Movie GetById(int id)
        {
            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("SELECT Id, Title, Genre, Duration, AgeRating FROM Movies WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapMovie(reader);
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

        public int Insert(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand(@"INSERT INTO Movies (Title, Genre, Duration, AgeRating)
VALUES (@Title, @Genre, @Duration, @AgeRating);
SELECT CAST(SCOPE_IDENTITY() AS int);", connection))
                {
                    command.Parameters.AddWithValue("@Title", movie.Title);
                    command.Parameters.AddWithValue("@Genre", (object?)movie.Genre ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Duration", movie.Duration);
                    command.Parameters.AddWithValue("@AgeRating", (object?)movie.AgeRating ?? DBNull.Value);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }
        }

        public bool Update(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand(@"UPDATE Movies
SET Title = @Title,
    Genre = @Genre,
    Duration = @Duration,
    AgeRating = @AgeRating
WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Title", movie.Title);
                    command.Parameters.AddWithValue("@Genre", (object?)movie.Genre ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Duration", movie.Duration);
                    command.Parameters.AddWithValue("@AgeRating", (object?)movie.AgeRating ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Id", movie.Id);

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
                using (var command = new SqlCommand("DELETE FROM Movies WHERE Id = @Id", connection))
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

        private static Movie MapMovie(IDataRecord record)
        {
            var movie = new Movie();
            movie.Id = record.GetInt32(record.GetOrdinal("Id"));
            movie.Title = record.GetString(record.GetOrdinal("Title"));
            if (!record.IsDBNull(record.GetOrdinal("Genre")))
            {
                movie.Genre = record.GetString(record.GetOrdinal("Genre"));
            }
            if (!record.IsDBNull(record.GetOrdinal("Duration")))
            {
                movie.Duration = record.GetInt32(record.GetOrdinal("Duration"));
            }
            if (!record.IsDBNull(record.GetOrdinal("AgeRating")))
            {
                movie.AgeRating = record.GetString(record.GetOrdinal("AgeRating"));
            }

            return movie;
        }
    }
}
