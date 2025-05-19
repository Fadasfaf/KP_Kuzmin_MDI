namespace GraphicalEditorMDI
{
    partial class DrawingDocument
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnPentagon;
        private System.Windows.Forms.Button btnHexagon;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnPenColor;
        private System.Windows.Forms.Button btnFillColor;
        private System.Windows.Forms.CheckBox chkFill;
        private System.Windows.Forms.TrackBar trackBarPenWidth;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnPentagon = new System.Windows.Forms.Button();
            this.btnHexagon = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnPenColor = new System.Windows.Forms.Button();
            this.btnFillColor = new System.Windows.Forms.Button();
            this.chkFill = new System.Windows.Forms.CheckBox();
            this.trackBarPenWidth = new System.Windows.Forms.TrackBar();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.toolPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPenWidth)).BeginInit();
            this.SuspendLayout();

            // pictureBox
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 50);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 550);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);

            // toolPanel
            this.toolPanel.Controls.Add(this.btnLine);
            this.toolPanel.Controls.Add(this.btnRectangle);
            this.toolPanel.Controls.Add(this.btnEllipse);
            this.toolPanel.Controls.Add(this.btnPentagon);
            this.toolPanel.Controls.Add(this.btnHexagon);
            this.toolPanel.Controls.Add(this.btnSelect);
            this.toolPanel.Controls.Add(this.btnPaste);
            this.toolPanel.Controls.Add(this.btnPenColor);
            this.toolPanel.Controls.Add(this.btnFillColor);
            this.toolPanel.Controls.Add(this.chkFill);
            this.toolPanel.Controls.Add(this.trackBarPenWidth);
            this.toolPanel.Controls.Add(this.btnClear);
            this.toolPanel.Controls.Add(this.btnSave);
            this.toolPanel.Controls.Add(this.btnOpen);
            this.toolPanel.Controls.Add(this.btnCopy);
            this.toolPanel.Controls.Add(this.btnCut);
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolPanel.Location = new System.Drawing.Point(0, 0);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(800, 50);
            this.toolPanel.TabIndex = 1;

            // btnLine
            this.btnLine.Location = new System.Drawing.Point(5, 5);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(50, 40);
            this.btnLine.TabIndex = 0;
            this.btnLine.Text = "Линия";

            // btnRectangle
            this.btnRectangle.Location = new System.Drawing.Point(60, 5);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(50, 40);
            this.btnRectangle.TabIndex = 1;
            this.btnRectangle.Text = "Прямоуг.";

            // btnEllipse
            this.btnEllipse.Location = new System.Drawing.Point(115, 5);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(50, 40);
            this.btnEllipse.TabIndex = 2;
            this.btnEllipse.Text = "Эллипс";

            // btnPentagon
            this.btnPentagon.Location = new System.Drawing.Point(170, 5);
            this.btnPentagon.Name = "btnPentagon";
            this.btnPentagon.Size = new System.Drawing.Size(50, 40);
            this.btnPentagon.TabIndex = 3;
            this.btnPentagon.Text = "Пятиуг.";

            // btnHexagon
            this.btnHexagon.Location = new System.Drawing.Point(225, 5);
            this.btnHexagon.Name = "btnHexagon";
            this.btnHexagon.Size = new System.Drawing.Size(50, 40);
            this.btnHexagon.TabIndex = 4;
            this.btnHexagon.Text = "Шестиуг.";

            // btnSelect
            this.btnSelect.Location = new System.Drawing.Point(280, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(50, 40);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Выделить";

            // btnPaste
            this.btnPaste.Location = new System.Drawing.Point(335, 5);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(50, 40);
            this.btnPaste.TabIndex = 6;
            this.btnPaste.Text = "Вставить";

            // btnPenColor
            this.btnPenColor.Location = new System.Drawing.Point(390, 5);
            this.btnPenColor.Name = "btnPenColor";
            this.btnPenColor.Size = new System.Drawing.Size(50, 40);
            this.btnPenColor.TabIndex = 7;
            this.btnPenColor.Text = "Цвет линии";

            // btnFillColor
            this.btnFillColor.Location = new System.Drawing.Point(445, 5);
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.Size = new System.Drawing.Size(50, 40);
            this.btnFillColor.TabIndex = 8;
            this.btnFillColor.Text = "Цвет заливки";

            // chkFill
            this.chkFill.AutoSize = true;
            this.chkFill.Location = new System.Drawing.Point(500, 15);
            this.chkFill.Name = "chkFill";
            this.chkFill.Size = new System.Drawing.Size(80, 17);
            this.chkFill.TabIndex = 9;
            this.chkFill.Text = "Заливка";

            // trackBarPenWidth
            this.trackBarPenWidth.Location = new System.Drawing.Point(585, 5);
            this.trackBarPenWidth.Minimum = 1;
            this.trackBarPenWidth.Maximum = 20;
            this.trackBarPenWidth.Value = 2;
            this.trackBarPenWidth.Name = "trackBarPenWidth";
            this.trackBarPenWidth.Size = new System.Drawing.Size(100, 45);
            this.trackBarPenWidth.TabIndex = 10;

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(690, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 40);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Очистить";

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(745, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 40);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Сохранить";

            // btnOpen
            this.btnOpen.Location = new System.Drawing.Point(800, 5);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(50, 40);
            this.btnOpen.TabIndex = 13;
            this.btnOpen.Text = "Открыть";

            // btnCopy
            this.btnCopy.Location = new System.Drawing.Point(855, 5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(50, 40);
            this.btnCopy.TabIndex = 14;
            this.btnCopy.Text = "Копировать";

            // btnCut
            this.btnCut.Location = new System.Drawing.Point(910, 5);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(50, 40);
            this.btnCut.TabIndex = 15;
            this.btnCut.Text = "Вырезать";

            // saveFileDialog
            this.saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp|GIF Image|*.gif";

            // openFileDialog
            this.openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            // DrawingDocument
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.toolPanel);
            this.Name = "DrawingDocument";
            this.Text = "Документ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.toolPanel.ResumeLayout(false);
            this.toolPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPenWidth)).EndInit();
            this.ResumeLayout(false);
        }
    }
}