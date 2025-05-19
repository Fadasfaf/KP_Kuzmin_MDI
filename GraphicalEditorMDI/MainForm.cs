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

        private void NewDocument()
        {
            DrawingDocument doc = new DrawingDocument(documentCounter++);
            doc.MdiParent = this;
            doc.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Графический редактор MDI by Кузьмин С.Ю. ЗЦИС-26\nВерсия 1.0", "О программе");
        }
    }
}