using System;
using System.Windows.Forms;
using Cinema.BLL;
using Cinema.Entities;
using Cinema.WinForms.Forms;

namespace Cinema.WinForms
{
    public partial class MainForm : Form
    {
        private readonly DiagnosticsService _diagnosticsService = new DiagnosticsService();
        private readonly User? _currentUser;

        public MainForm() : this(null)
        {
        }

        public MainForm(User? currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();
            Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateTitle();

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

        private void UpdateTitle()
        {
            if (_currentUser != null)
            {
                Text = $"Cinema — Main — {_currentUser.Display()}";
            }
            else
            {
                Text = "Cinema — Main";
            }
        }

        private void menuMovies_Click(object sender, EventArgs e)
        {
            using (var form = new MoviesForm())
            {
                form.ShowDialog(this);
            }
        }

        private void menuShowtimes_Click(object sender, EventArgs e)
        {
            using (var form = new ShowtimesForm())
            {
                form.ShowDialog(this);
            }
        }

        private void menuSearch_Click(object sender, EventArgs e)
        {
            using (var form = new SearchForm())
            {
                form.ShowDialog(this);
            }
        }

        private void menuReport_Click(object sender, EventArgs e)
        {
            using (var form = new ReportForm())
            {
                form.ShowDialog(this);
            }
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
