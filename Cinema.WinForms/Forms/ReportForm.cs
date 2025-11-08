using System;
using System.Data;
using System.Windows.Forms;
using Cinema.BLL;

namespace Cinema.WinForms.Forms
{
    public partial class ReportForm : Form
    {
        private readonly ReportService _reportService = new ReportService();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date;
            var result = _reportService.RevenueByDate(from, to);
            if (result.Success)
            {
                dgvReport.DataSource = null;
                dgvReport.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
