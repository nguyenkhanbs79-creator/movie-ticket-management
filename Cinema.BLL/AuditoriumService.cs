using System;
using System.Collections.Generic;
using Cinema.DAL;
using Cinema.Entities;

namespace Cinema.BLL
{
    /// <summary>
    /// Provides business logic for auditorium lookups.
    /// </summary>
    public class AuditoriumService
    {
        private readonly AuditoriumDal _auditoriumDal = new AuditoriumDal();

        /// <summary>
        /// Retrieves all auditoriums.
        /// </summary>
        public ServiceResult<List<Auditorium>> GetAll()
        {
            try
            {
                var auditoriums = _auditoriumDal.GetAll();
                var message = auditoriums.Count == 0 ? "Không có phòng chiếu." : "Lấy danh sách phòng chiếu thành công.";
                return ServiceResult<List<Auditorium>>.Ok(auditoriums, message);
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<List<Auditorium>>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<List<Auditorium>>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }
    }
}
