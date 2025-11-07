using System;
using System.Windows.Forms;
using Cinema.BLL;

namespace Cinema.WinForms
{
    public partial class MainForm : Form
    {
        private readonly DiagnosticsService _diagnosticsService = new DiagnosticsService();

        public MainForm()
        {
            InitializeComponent();
            Load += MainForm_Load;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            try
            {
                if (_diagnosticsService.TryTestConnection(out var message))
                {
                    MessageBox.Show(message, "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(message, "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
