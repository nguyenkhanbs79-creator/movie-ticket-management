using System;
using System.Windows.Forms;
using Cinema.BLL;
using Cinema.Entities;

namespace Cinema.WinForms.Forms
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService = new AuthService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUser.Text.Trim();
            var password = txtPass.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Mật khẩu không được để trống.", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }

            try
            {
                var result = _authService.Login(username, password);
                if (result.Success && result.Data != null)
                {
                    Hide();
                    using (var mainForm = new MainForm(result.Data))
                    {
                        mainForm.ShowDialog();
                    }
                    Close();
                }
                else
                {
                    var message = string.IsNullOrWhiteSpace(result.Message) ? "Đăng nhập thất bại." : result.Message;
                    MessageBox.Show(message, "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.SelectAll();
                    txtPass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
