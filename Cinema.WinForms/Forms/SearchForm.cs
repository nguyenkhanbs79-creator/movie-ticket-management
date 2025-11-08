using System;
using System.Windows.Forms;
using Cinema.BLL;
using Cinema.Entities;

namespace Cinema.WinForms.Forms
{
    public partial class SearchForm : Form
    {
        private readonly ShowtimeService _showtimeService = new ShowtimeService();

        public SearchForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string? titleLike = string.IsNullOrWhiteSpace(txtTitleLike.Text) ? null : txtTitleLike.Text.Trim();
            DateTime? from = null;
            DateTime? to = null;

            if (chkEnableDateFilter.Checked)
            {
                from = dtpFrom.Value;
                to = dtpTo.Value;
            }

            var result = _showtimeService.Search(titleLike, from, to);
            if (result.Success)
            {
                dgvResults.DataSource = null;
                dgvResults.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkEnableDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkEnableDateFilter.Checked;
            dtpFrom.Enabled = enabled;
            dtpTo.Enabled = enabled;
        }
    }
}
