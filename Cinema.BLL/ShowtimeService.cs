using System;
using System.Collections.Generic;
using System.Data;
using Cinema.DAL;
using Cinema.Entities;

namespace Cinema.BLL
{
    /// <summary>
    /// Provides business logic for working with showtimes, including validation and search helpers.
    /// </summary>
    public class ShowtimeService
    {
        private readonly ShowtimeDal _showtimeDal = new ShowtimeDal();

        /// <summary>
        /// Retrieves all showtimes.
        /// </summary>
        public ServiceResult<List<Showtime>> GetAll()
        {
            try
            {
                var showtimes = _showtimeDal.GetAll();
                var message = showtimes.Count == 0 ? "Không có suất chiếu nào." : "Lấy danh sách suất chiếu thành công.";
                return ServiceResult<List<Showtime>>.Ok(showtimes, message);
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<List<Showtime>>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<List<Showtime>>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }

        /// <summary>
        /// Retrieves a showtime by identifier.
        /// </summary>
        public ServiceResult<Showtime> GetById(int id)
        {
            try
            {
                Guard.ValidId(id, "Id");
                var showtime = _showtimeDal.GetById(id);
                if (showtime == null)
                {
                    return ServiceResult<Showtime>.Fail("Không tìm thấy suất chiếu.");
                }

                return ServiceResult<Showtime>.Ok(showtime, "Lấy suất chiếu thành công.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<Showtime>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<Showtime>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }

        /// <summary>
        /// Inserts a new showtime.
        /// </summary>
        public ServiceResult<int> Insert(Showtime showtime)
        {
            try
            {
                if (showtime == null)
                {
                    throw new ArgumentException("Showtime không được null.", nameof(showtime));
                }

                Guard.ValidId(showtime.MovieId, "MovieId");
                Guard.ValidId(showtime.AuditoriumId, "AuditoriumId");
                Guard.PositiveDecimal(showtime.BasePrice, "BasePrice");
                if (showtime.StartTime == default(DateTime))
                {
                    throw new ArgumentException("StartTime không hợp lệ.", nameof(showtime.StartTime));
                }

                var newId = _showtimeDal.Insert(showtime);
                if (newId > 0)
                {
                    return ServiceResult<int>.Ok(newId, "Thêm suất chiếu thành công.");
                }

                return ServiceResult<int>.Fail("Không thể thêm suất chiếu.");
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
        /// Updates an existing showtime.
        /// </summary>
        public ServiceResult<bool> Update(Showtime showtime)
        {
            try
            {
                if (showtime == null)
                {
                    throw new ArgumentException("Showtime không được null.", nameof(showtime));
                }

                Guard.ValidId(showtime.Id, "Id");
                Guard.ValidId(showtime.MovieId, "MovieId");
                Guard.ValidId(showtime.AuditoriumId, "AuditoriumId");
                Guard.PositiveDecimal(showtime.BasePrice, "BasePrice");
                if (showtime.StartTime == default(DateTime))
                {
                    throw new ArgumentException("StartTime không hợp lệ.", nameof(showtime.StartTime));
                }

                var updated = _showtimeDal.Update(showtime);
                if (updated)
                {
                    return ServiceResult<bool>.Ok(true, "Cập nhật suất chiếu thành công.");
                }

                return ServiceResult<bool>.Fail("Không tìm thấy suất chiếu để cập nhật.");
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
        /// Deletes a showtime by identifier.
        /// </summary>
        public ServiceResult<bool> Delete(int id)
        {
            try
            {
                Guard.ValidId(id, "Id");
                var deleted = _showtimeDal.Delete(id);
                if (deleted)
                {
                    return ServiceResult<bool>.Ok(true, "Xóa suất chiếu thành công.");
                }

                return ServiceResult<bool>.Fail("Không tìm thấy suất chiếu để xóa.");
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
        /// Searches showtimes using optional filters.
        /// </summary>
        public ServiceResult<List<Showtime>> Search(string titleLike, DateTime? from, DateTime? to)
        {
            try
            {
                Guard.ValidDateRange(from, to, "From", "To");
                var showtimes = _showtimeDal.Search(titleLike, from, to);
                var message = showtimes.Count == 0 ? "Không tìm thấy suất chiếu phù hợp." : "Tìm kiếm suất chiếu thành công.";
                return ServiceResult<List<Showtime>>.Ok(showtimes, message);
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<List<Showtime>>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<List<Showtime>>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }
    }
}
