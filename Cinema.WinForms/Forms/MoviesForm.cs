using System.Drawing;
using System.Windows.Forms;

namespace Cinema.WinForms.Forms
{
    public class MoviesForm : Form
    {
        public MoviesForm()
        {
            Text = "Movies";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(800, 600);
        }
    }
}
