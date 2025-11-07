using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Cinema.BLL;
using Cinema.Entities;

namespace Cinema.WinForms.Forms
{
    public partial class ShowtimesForm : Form
    {
        private readonly ShowtimeService _showtimeService = new ShowtimeService();
        private readonly MovieService _movieService = new MovieService();
        private readonly AuditoriumService _auditoriumService = new AuditoriumService();
        private List<Movie> _movies = new List<Movie>();
        private List<Auditorium> _auditoriums = new List<Auditorium>();
        private List<Showtime> _showtimes = new List<Showtime>();
        private int? _selectedShowtimeId;

        public ShowtimesForm()
        {
            InitializeComponent();

            // Cấu hình cơ bản của Form (giữ từ nhánh main)
            Text = "Showtimes";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(800, 600);

            Load += ShowtimesForm_Load;
        }

        private void ShowtimesForm_Load(object sender, EventArgs e)
        {
            LoadMovies();
            LoadAuditoriums();
            LoadShowtimes();
            ClearInputs();
        }

        private void LoadMovies()
        {
            var result = _movieService.GetAll();
            if (result.Success)
            {
                _movies = result.Data ?? new List<Movie>();
                cboMovie.DataSource = null;
                cboMovie.DataSource = _movies;
                cboMovie.DisplayMember = nameof(Movie.Title);
                cboMovie.ValueMember = nameof(Movie.Id);
                cboMovie.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadAuditoriums()
        {
            var result = _auditoriumService.GetAll();
            if (result.Success)
            {
                _auditoriums = result.Data ?? new List<Auditorium>();
                cboAuditorium.DataSource = null;
                cboAuditorium.DataSource = _auditoriums;
                cboAuditorium.DisplayMember = nameof(Auditorium.Name);
                cboAuditorium.ValueMember = nameof(Auditorium.Id);
                cboAuditorium.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadShowtimes()
        {
            var listResult = _showtimeService.GetAll();
            if (listResult.Success)
            {
                _showtimes = listResult.Data ?? new List<Showtime>();
            }
            else
            {
                MessageBox.Show(listResult.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _showtimes = new List<Showtime>();
            }

            var viewResult = _showtimeService.GetDetails();
            if (viewResult.Success)
            {
                dgvShowtimes.DataSource = null;
                dgvShowtimes.DataSource = viewResult.Data ?? new DataTable();
                ConfigureGridColumns();
                dgvShowtimes.ClearSelection();
            }
            else
            {
                MessageBox.Show(viewResult.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvShowtimes.DataSource = null;
            }
        }

        private void ConfigureGridColumns()
        {
            if (dgvShowtimes.Columns["Id"] != null)
            {
                dgvShowtimes.Columns["Id"].HeaderText = "ID";
                dgvShowtimes.Columns["Id"].Width = 60;
            }

            if (dgvShowtimes.Columns["Title"] != null)
            {
                dgvShowtimes.Columns["Title"].HeaderText = "Phim";
                dgvShowtimes.Columns["Title"].Width = 220;
            }

            if (dgvShowtimes.Columns["Auditorium"] != null)
            {
                dgvShowtimes.Columns["Auditorium"].HeaderText = "Phòng";
                dgvShowtimes.Columns["Auditorium"].Width = 160;
            }

            if (dgvShowtimes.Columns["StartTime"] != null)
            {
                dgvShowtimes.Columns["StartTime"].HeaderText = "Giờ chiếu";
                dgvShowtimes.Columns["StartTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvShowtimes.Columns["StartTime"].Width = 180;
            }

            if (dgvShowtimes.Columns["BasePrice"] != null)
            {
                dgvShowtimes.Columns["BasePrice"].HeaderText = "Giá cơ bản";
                dgvShowtimes.Columns["BasePrice"].DefaultCellStyle.Format = "N2";
                dgvShowtimes.Columns["BasePrice"].Width = 120;
            }
        }

        private void ClearInputs()
        {
            _selectedShowtimeId = null;
            cboMovie.SelectedIndex = -1;
            cboAuditorium.SelectedIndex = -1;
            dtpStartTime.Value = DateTime.Now;
            txtBasePrice.Clear();
            if (dgvShowtimes.Rows.Count > 0)
            {
                dgvShowtimes.ClearSelection();
            }
        }

        private bool TryGetShowtimeFromInputs(out Showtime showtime)
        {
            showtime = new Showtime();

            if (cboMovie.SelectedValue == null || !(cboMovie.SelectedValue is int movieId) || movieId <= 0)
            {
                MessageBox.Show("Vui lòng chọn phim.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMovie.Focus();
                return false;
            }

            if (cboAuditorium.SelectedValue == null || !(cboAuditorium.SelectedValue is int auditoriumId) || auditoriumId <= 0)
            {
                MessageBox.Show("Vui lòng chọn phòng chiếu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboAuditorium.Focus();
                return false;
            }

            var basePriceText = txtBasePrice.Text.Trim();
            if (!decimal.TryParse(basePriceText, NumberStyles.Number, CultureInfo.InvariantCulture, out var basePrice) || basePrice <= 0)
            {
                if (!decimal.TryParse(basePriceText, out basePrice) || basePrice <= 0)
                {
                    MessageBox.Show("Giá phải là số dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBasePrice.Focus();
                    return false;
                }
            }

            var startTime = dtpStartTime.Value;
            if (startTime == default(DateTime))
            {
                MessageBox.Show("Giờ chiếu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpStartTime.Focus();
                return false;
            }

            showtime.Id = _selectedShowtimeId.GetValueOrDefault();
            showtime.MovieId = movieId;
            showtime.AuditoriumId = auditoriumId;
            showtime.StartTime = startTime;
            showtime.BasePrice = basePrice;

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _selectedShowtimeId = null;
            if (!TryGetShowtimeFromInputs(out var showtime))
            {
                return;
            }

            var result = _showtimeService.Insert(showtime);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadShowtimes();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_selectedShowtimeId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn suất chiếu cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryGetShowtimeFromInputs(out var showtime))
            {
                return;
            }

            showtime.Id = _selectedShowtimeId.Value;
            var result = _showtimeService.Update(showtime);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadShowtimes();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_selectedShowtimeId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn suất chiếu cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa suất chiếu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            var result = _showtimeService.Delete(_selectedShowtimeId.Value);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadShowtimes();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadShowtimes();
        }

        private void dgvShowtimes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvShowtimes.Rows.Count)
            {
                return;
            }

            var row = dgvShowtimes.Rows[e.RowIndex];
            if (row.Cells["Id"]?.Value == null)
            {
                return;
            }

            if (!int.TryParse(row.Cells["Id"].Value.ToString(), out var id))
            {
                return;
            }

            var showtime = _showtimes.FirstOrDefault(s => s.Id == id);
            if (showtime == null)
            {
                MessageBox.Show("Không thể tải dữ liệu suất chiếu được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedShowtimeId = showtime.Id;
            cboMovie.SelectedValue = showtime.MovieId;
            cboAuditorium.SelectedValue = showtime.AuditoriumId;
            dtpStartTime.Value = showtime.StartTime;
            txtBasePrice.Text = showtime.BasePrice.ToString(CultureInfo.InvariantCulture);
        }
    }
}
