namespace Cinema.WinForms.Forms
{
    partial class ShowtimesForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvShowtimes = null!;
        private System.Windows.Forms.Label lblMovie = null!;
        private System.Windows.Forms.Label lblAuditorium = null!;
        private System.Windows.Forms.Label lblStartTime = null!;
        private System.Windows.Forms.Label lblBasePrice = null!;
        private System.Windows.Forms.ComboBox cboMovie = null!;
        private System.Windows.Forms.ComboBox cboAuditorium = null!;
        private System.Windows.Forms.DateTimePicker dtpStartTime = null!;
        private System.Windows.Forms.TextBox txtBasePrice = null!;
        private System.Windows.Forms.Button btnAdd = null!;
        private System.Windows.Forms.Button btnUpdate = null!;
        private System.Windows.Forms.Button btnDelete = null!;
        private System.Windows.Forms.Button btnRefresh = null!;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvShowtimes = new System.Windows.Forms.DataGridView();
            this.lblMovie = new System.Windows.Forms.Label();
            this.lblAuditorium = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblBasePrice = new System.Windows.Forms.Label();
            this.cboMovie = new System.Windows.Forms.ComboBox();
            this.cboAuditorium = new System.Windows.Forms.ComboBox();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.txtBasePrice = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowtimes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShowtimes
            // 
            this.dgvShowtimes.AllowUserToAddRows = false;
            this.dgvShowtimes.AllowUserToDeleteRows = false;
            this.dgvShowtimes.AllowUserToOrderColumns = true;
            this.dgvShowtimes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowtimes.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvShowtimes.Location = new System.Drawing.Point(0, 0);
            this.dgvShowtimes.MultiSelect = false;
            this.dgvShowtimes.Name = "dgvShowtimes";
            this.dgvShowtimes.ReadOnly = true;
            this.dgvShowtimes.RowHeadersVisible = false;
            this.dgvShowtimes.RowTemplate.Height = 28;
            this.dgvShowtimes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShowtimes.Size = new System.Drawing.Size(900, 320);
            this.dgvShowtimes.TabIndex = 0;
            this.dgvShowtimes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowtimes_CellClick);
            // 
            // lblMovie
            // 
            this.lblMovie.AutoSize = true;
            this.lblMovie.Location = new System.Drawing.Point(24, 344);
            this.lblMovie.Name = "lblMovie";
            this.lblMovie.Size = new System.Drawing.Size(55, 20);
            this.lblMovie.TabIndex = 1;
            this.lblMovie.Text = "Movie";
            // 
            // lblAuditorium
            // 
            this.lblAuditorium.AutoSize = true;
            this.lblAuditorium.Location = new System.Drawing.Point(24, 392);
            this.lblAuditorium.Name = "lblAuditorium";
            this.lblAuditorium.Size = new System.Drawing.Size(88, 20);
            this.lblAuditorium.TabIndex = 2;
            this.lblAuditorium.Text = "Auditorium";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(24, 440);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(82, 20);
            this.lblStartTime.TabIndex = 3;
            this.lblStartTime.Text = "Start Time";
            // 
            // lblBasePrice
            // 
            this.lblBasePrice.AutoSize = true;
            this.lblBasePrice.Location = new System.Drawing.Point(24, 488);
            this.lblBasePrice.Name = "lblBasePrice";
            this.lblBasePrice.Size = new System.Drawing.Size(84, 20);
            this.lblBasePrice.TabIndex = 4;
            this.lblBasePrice.Text = "Base Price";
            // 
            // cboMovie
            // 
            this.cboMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMovie.FormattingEnabled = true;
            this.cboMovie.Location = new System.Drawing.Point(144, 340);
            this.cboMovie.Name = "cboMovie";
            this.cboMovie.Size = new System.Drawing.Size(320, 28);
            this.cboMovie.TabIndex = 5;
            // 
            // cboAuditorium
            // 
            this.cboAuditorium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAuditorium.FormattingEnabled = true;
            this.cboAuditorium.Location = new System.Drawing.Point(144, 388);
            this.cboAuditorium.Name = "cboAuditorium";
            this.cboAuditorium.Size = new System.Drawing.Size(320, 28);
            this.cboAuditorium.TabIndex = 6;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(144, 436);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(320, 26);
            this.dtpStartTime.TabIndex = 7;
            // 
            // txtBasePrice
            // 
            this.txtBasePrice.Location = new System.Drawing.Point(144, 484);
            this.txtBasePrice.Name = "txtBasePrice";
            this.txtBasePrice.Size = new System.Drawing.Size(320, 26);
            this.txtBasePrice.TabIndex = 8;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(520, 340);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 36);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(520, 388);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 36);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(520, 436);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 36);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(520, 484);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 36);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ShowtimesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtBasePrice);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.cboAuditorium);
            this.Controls.Add(this.cboMovie);
            this.Controls.Add(this.lblBasePrice);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.lblAuditorium);
            this.Controls.Add(this.lblMovie);
            this.Controls.Add(this.dgvShowtimes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowtimesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Suất chiếu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowtimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
