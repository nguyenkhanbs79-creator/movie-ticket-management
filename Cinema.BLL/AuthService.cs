using System;
using System.Collections.Generic;
using System.Data;
using Cinema.DAL;
using Cinema.Entities;

namespace Cinema.BLL
{
    /// <summary>
    /// Handles authentication related business logic.
    /// </summary>
    public class AuthService
    {
        private readonly UserDal _userDal = new UserDal();

        /// <summary>
        /// Attempts to log a user in with the provided credentials.
        /// </summary>
        public ServiceResult<User> Login(string username, string passwordHash)
        {
            try
            {
                Guard.NotNullOrEmpty(username, "Username");
                Guard.NotNullOrEmpty(passwordHash, "Password");

                var user = _userDal.Login(username, passwordHash);
                if (user == null)
                {
                    return ServiceResult<User>.Fail("Sai tên đăng nhập hoặc mật khẩu.");
                }

                return ServiceResult<User>.Ok(user, "Đăng nhập thành công.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<User>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ServiceResult<User>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.");
            }
        }
    }
}
