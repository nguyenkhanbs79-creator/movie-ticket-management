using System;
using System.Collections.Generic;
using System.Data;
using Cinema.DAL;
using Cinema.Entities;

namespace Cinema.BLL
{
    /// <summary>
    /// Provides business operations for working with movies while applying validation and user-friendly messaging.
    /// </summary>
    public class MovieService
    {
        private readonly MovieDal _movieDal = new MovieDal();

        /// <summary>
        /// Retrieves all movies.
        /// </summary>
        public ServiceResult<List<Movie>> GetAll()
        {
            try
            {
                var movies = _movieDal.GetAll();
                var message = movies.Count == 0 ? "Không có phim nào." : "Lấy danh sách phim thành công.";
                return ServiceResult<List<Movie>>.Ok(movies, message);
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<List<Movie>>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<List<Movie>>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }

        /// <summary>
        /// Retrieves a movie by its identifier.
        /// </summary>
        public ServiceResult<Movie> GetById(int id)
        {
            try
            {
                Guard.ValidId(id, "Id");
                var movie = _movieDal.GetById(id);
                if (movie == null)
                {
                    return ServiceResult<Movie>.Fail("Không tìm thấy phim.");
                }

                return ServiceResult<Movie>.Ok(movie, "Lấy phim thành công.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<Movie>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<Movie>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }

        /// <summary>
        /// Inserts a new movie.
        /// </summary>
        public ServiceResult<int> Insert(Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    throw new ArgumentException("Movie không được null.", nameof(movie));
                }

                Guard.NotNullOrEmpty(movie.Title, "Title");
                Guard.PositiveInt(movie.Duration, "Duration");

                var newId = _movieDal.Insert(movie);
                if (newId > 0)
                {
                    return ServiceResult<int>.Ok(newId, "Thêm phim thành công.");
                }

                return ServiceResult<int>.Fail("Không thể thêm phim.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<int>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<int>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        public ServiceResult<bool> Update(Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    throw new ArgumentException("Movie không được null.", nameof(movie));
                }

                Guard.ValidId(movie.Id, "Id");
                Guard.NotNullOrEmpty(movie.Title, "Title");
                Guard.PositiveInt(movie.Duration, "Duration");

                var updated = _movieDal.Update(movie);
                if (updated)
                {
                    return ServiceResult<bool>.Ok(true, "Cập nhật phim thành công.");
                }

                return ServiceResult<bool>.Fail("Không tìm thấy phim để cập nhật.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<bool>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<bool>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }

        /// <summary>
        /// Deletes a movie by identifier.
        /// </summary>
        public ServiceResult<bool> Delete(int id)
        {
            try
            {
                Guard.ValidId(id, "Id");
                var deleted = _movieDal.Delete(id);
                if (deleted)
                {
                    return ServiceResult<bool>.Ok(true, "Xóa phim thành công.");
                }

                return ServiceResult<bool>.Fail("Không tìm thấy phim để xóa.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<bool>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<bool>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }
    }
}
