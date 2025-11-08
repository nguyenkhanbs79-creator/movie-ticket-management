namespace Cinema.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStripMain = null!;
        private System.Windows.Forms.ToolStripMenuItem menuMovies = null!;
        private System.Windows.Forms.ToolStripMenuItem menuShowtimes = null!;
        private System.Windows.Forms.ToolStripMenuItem menuSearch = null!;
        private System.Windows.Forms.ToolStripMenuItem menuReport = null!;
        private System.Windows.Forms.ToolStripMenuItem menuLogout = null!;

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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuMovies = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowtimes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMovies,
            this.menuShowtimes,
            this.menuSearch,
            this.menuReport,
            this.menuLogout});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1000, 33);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // menuMovies
            // 
            this.menuMovies.Name = "menuMovies";
            this.menuMovies.Size = new System.Drawing.Size(85, 29);
            this.menuMovies.Text = "Movies";
            this.menuMovies.Click += new System.EventHandler(this.menuMovies_Click);
            // 
            // menuShowtimes
            // 
            this.menuShowtimes.Name = "menuShowtimes";
            this.menuShowtimes.Size = new System.Drawing.Size(110, 29);
            this.menuShowtimes.Text = "Showtimes";
            this.menuShowtimes.Click += new System.EventHandler(this.menuShowtimes_Click);
            // 
            // menuSearch
            // 
            this.menuSearch.Name = "menuSearch";
            this.menuSearch.Size = new System.Drawing.Size(82, 29);
            this.menuSearch.Text = "Search";
            this.menuSearch.Click += new System.EventHandler(this.menuSearch_Click);
            // 
            // menuReport
            // 
            this.menuReport.Name = "menuReport";
            this.menuReport.Size = new System.Drawing.Size(84, 29);
            this.menuReport.Text = "Report";
            this.menuReport.Click += new System.EventHandler(this.menuReport_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(88, 29);
            this.menuLogout.Text = "Logout";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cinema — Main";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
