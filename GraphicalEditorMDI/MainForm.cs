using System;
using System.Windows.Forms;

namespace GraphicalEditorMDI
{
    public partial class MainForm : Form
    {
        private int documentCounter = 1;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Графический редактор MDI";
            this.WindowState = FormWindowState.Maximized;
        }

        public void PasteToNewDocument(Bitmap image)
        {
            DrawingDocument doc = new DrawingDocument(documentCounter++);
            doc.MdiParent = this;
            doc.Size = new Size(1300, 544);
            doc.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawingDocument doc = new DrawingDocument(documentCounter++);
            doc.Size = new Size(1300, 544);
            doc.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
                childForm.Close();
            documentCounter = 1;
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}