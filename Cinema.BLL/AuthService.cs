using System;
using Cinema.DAL;
using Cinema.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Cinema.BLL
{
    /// <summary>
    /// Handles authentication related business logic.
    /// </summary>
    public class AuthService
    {
        private readonly UserDal _userDal = new UserDal();
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        /// <summary>
        /// Attempts to log a user in with the provided credentials.
        /// </summary>
        /// <param name="username">The username supplied by the user.</param>
        /// <param name="passwordPlain">The plaintext password supplied by the user.</param>
        public ServiceResult<User> Login(string username, string passwordPlain)
        {
            try
            {
                Guard.NotNullOrEmpty(username, "Username");
                Guard.NotNullOrEmpty(passwordPlain, "Password");

                var user = _userDal.GetByUsername(username);
                if (user == null)
                {
                    return ServiceResult<User>.Fail("Sai thông tin đăng nhập.", ErrorCode.AuthFailed);
                }

                if (!TryValidatePassword(user, passwordPlain, out var upgradedCredential))
                {
                    return ServiceResult<User>.Fail("Sai thông tin đăng nhập.", ErrorCode.AuthFailed);
                }

                if (!string.IsNullOrEmpty(upgradedCredential))
                {
                    _userDal.UpdatePasswordCredential(user.Id, upgradedCredential);
                    user.PasswordKdf = upgradedCredential;
                }

                return ServiceResult<User>.Ok(user, "Đăng nhập thành công.");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<User>.Fail(ex.Message, ErrorCode.InvalidInput);
            }
            catch (Exception)
            {
                return ServiceResult<User>.Fail("Có lỗi khi truy cập dữ liệu. Vui lòng thử lại sau.", ErrorCode.DbError);
            }
        }

        private bool TryValidatePassword(User user, string passwordPlain, out string? upgradedCredential)
        {
            upgradedCredential = null;

            if (!string.IsNullOrWhiteSpace(user.PasswordKdf))
            {
                return _passwordHasher.Verify(passwordPlain, user.PasswordKdf);
            }

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                return false;
            }

            var legacyHash = Sha256Hex(passwordPlain);
            var matches = string.Equals(user.PasswordHash, legacyHash, StringComparison.OrdinalIgnoreCase);
            if (matches)
            {
                upgradedCredential = _passwordHasher.HashPassword(passwordPlain);
            }

            return matches;
        }

        private static string Sha256Hex(string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input ?? string.Empty);
                var hash = sha.ComputeHash(bytes);
                var builder = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
