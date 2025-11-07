using System;
using System.Collections.Generic;
using System.Data;
using Cinema.DAL;
using Cinema.Entities;

namespace Cinema.BLL
{
    /// <summary>
    /// Provides reporting services for cinema statistics.
    /// </summary>
    public class ReportService
    {
        private readonly ReportDal _reportDal = new ReportDal();

        /// <summary>
        /// Retrieves revenue data grouped by date within the provided range.
        /// </summary>
        public ServiceResult<DataTable> RevenueByDate(DateTime from, DateTime to)
        {
            try
            {
                Guard.ValidDateRange(from, to, "From", "To");
                var table = _reportDal.RevenueByDate(from, to);
                return ServiceResult<DataTable>.Ok(table, "Lấy báo cáo thành công.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<DataTable>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<DataTable>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }
    }
}
