using System;
using System.Windows.Forms;
using Cinema.BLL;
using Cinema.Entities;

namespace Cinema.WinForms.Forms
{
    public partial class MoviesForm : Form
    {
        private readonly MovieService _movieService = new MovieService();
        private int? _selectedMovieId;

        public MoviesForm()
        {
            InitializeComponent();
            Load += MoviesForm_Load;
        }

        private void MoviesForm_Load(object sender, EventArgs e)
        {
            LoadMovies();
            ClearInputs();
        }

        private void LoadMovies()
        {
            var result = _movieService.GetAll();
            if (result.Success)
            {
                dgvMovies.DataSource = null;
                dgvMovies.DataSource = result.Data ?? Array.Empty<Movie>();
                ConfigureGridColumns();
                dgvMovies.ClearSelection();
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigureGridColumns()
        {
            if (dgvMovies.Columns["Id"] != null)
            {
                dgvMovies.Columns["Id"].HeaderText = "ID";
                dgvMovies.Columns["Id"].Width = 60;
            }

            if (dgvMovies.Columns["Title"] != null)
            {
                dgvMovies.Columns["Title"].HeaderText = "Tiêu đề";
                dgvMovies.Columns["Title"].Width = 220;
            }

            if (dgvMovies.Columns["Genre"] != null)
            {
                dgvMovies.Columns["Genre"].HeaderText = "Thể loại";
                dgvMovies.Columns["Genre"].Width = 150;
            }

            if (dgvMovies.Columns["Duration"] != null)
            {
                dgvMovies.Columns["Duration"].HeaderText = "Thời lượng";
                dgvMovies.Columns["Duration"].Width = 100;
            }

            if (dgvMovies.Columns["AgeRating"] != null)
            {
                dgvMovies.Columns["AgeRating"].HeaderText = "Độ tuổi";
                dgvMovies.Columns["AgeRating"].Width = 100;
            }
        }

        private void ClearInputs()
        {
            txtTitle.Clear();
            txtGenre.Clear();
            txtDuration.Clear();
            txtAgeRating.Clear();
            _selectedMovieId = null;
            if (dgvMovies.Rows.Count > 0)
            {
                dgvMovies.ClearSelection();
            }
            txtTitle.Focus();
        }

        private bool TryGetMovieFromInputs(out Movie movie)
        {
            movie = new Movie();

            var title = txtTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Title không được rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (!int.TryParse(txtDuration.Text.Trim(), out var duration) || duration <= 0)
            {
                MessageBox.Show("Duration phải là số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuration.Focus();
                return false;
            }

            movie.Id = _selectedMovieId.GetValueOrDefault();
            movie.Title = title;
            movie.Genre = txtGenre.Text.Trim();
            movie.Duration = duration;
            movie.AgeRating = txtAgeRating.Text.Trim();

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _selectedMovieId = null;
            if (!TryGetMovieFromInputs(out var movie))
            {
                return;
            }

            var result = _movieService.Insert(movie);
            if (result.Success)
            {
                MessageBox.Show("Thêm phim thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadMovies();
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_selectedMovieId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn phim cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryGetMovieFromInputs(out var movie))
            {
                return;
            }

            movie.Id = _selectedMovieId.Value;
            var result = _movieService.Update(movie);
            if (result.Success)
            {
                MessageBox.Show("Cập nhật phim thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadMovies();
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_selectedMovieId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn phim cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa phim này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            var result = _movieService.Delete(_selectedMovieId.Value);
            if (result.Success)
            {
                MessageBox.Show("Xóa phim thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadMovies();
            }
            else
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadMovies();
        }

        private void dgvMovies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMovies.Rows.Count)
            {
                return;
            }

            var row = dgvMovies.Rows[e.RowIndex];
            if (row.DataBoundItem is Movie movie)
            {
                _selectedMovieId = movie.Id;
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtDuration.Text = movie.Duration.ToString();
                txtAgeRating.Text = movie.AgeRating;
            }
        }
    }
}
