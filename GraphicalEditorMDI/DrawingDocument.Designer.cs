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
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Уменьшенные размеры кнопок (50x30 вместо 70x45)
            const int buttonWidth = 50;
            const int buttonHeight = 30;
            const int buttonSpacing = 5;
            const int verticalPadding = 13; // Для центрирования по вертикали
            int currentLeft = buttonSpacing; // Текущая позиция по X

            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolPanel = new System.Windows.Forms.Panel();

            // Создаем кнопки с уменьшенными размерами
            this.btnLine = CreateToolButton("Линия", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnRectangle = CreateToolButton("Прям.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnEllipse = CreateToolButton("Эллипс", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnPentagon = CreateToolButton("5-уг.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnHexagon = CreateToolButton("6-уг.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnSelect = CreateToolButton("Выдел.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnPaste = CreateToolButton("Встав.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnPenColor = CreateToolButton("Цвет", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnFillColor = CreateToolButton("Залив.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            // Чекбокс "Заливка"
            this.chkFill = new System.Windows.Forms.CheckBox();
            this.chkFill.AutoSize = true;
            this.chkFill.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.chkFill.Location = new System.Drawing.Point(currentLeft, verticalPadding + 5);
            this.chkFill.Name = "chkFill";
            this.chkFill.Size = new System.Drawing.Size(50, 17);
            this.chkFill.TabIndex = 9;
            this.chkFill.Text = "Залив";
            currentLeft += 55;

            // Трекбар толщины пера
            this.trackBarPenWidth = new System.Windows.Forms.TrackBar();
            this.trackBarPenWidth.Location = new System.Drawing.Point(currentLeft, verticalPadding - 5);
            this.trackBarPenWidth.Minimum = 1;
            this.trackBarPenWidth.Maximum = 20;
            this.trackBarPenWidth.Value = 2;
            this.trackBarPenWidth.Size = new System.Drawing.Size(80, 30);
            currentLeft += 85;

            // Остальные кнопки
            this.btnClear = CreateToolButton("Очист.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnSave = CreateToolButton("Сохр.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnOpen = CreateToolButton("Откр.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnCopy = CreateToolButton("Коп.", currentLeft, verticalPadding, buttonWidth, buttonHeight);
            currentLeft += buttonWidth + buttonSpacing;

            this.btnCut = CreateToolButton("Вырез.", currentLeft, verticalPadding, buttonWidth, buttonHeight);

            // Диалоги
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp|GIF Image|*.gif";
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            // PictureBox
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 56);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1300, 488);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);

            // ToolPanel
            this.toolPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnLine, this.btnRectangle, this.btnEllipse, this.btnPentagon,
                this.btnHexagon, this.btnSelect, this.btnPaste, this.btnPenColor,
                this.btnFillColor, this.chkFill, this.trackBarPenWidth, this.btnClear,
                this.btnSave, this.btnOpen, this.btnCopy, this.btnCut});
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolPanel.Location = new System.Drawing.Point(0, 0);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(1300, 56);
            this.toolPanel.TabIndex = 1;

            // DrawingDocument
            this.ClientSize = new System.Drawing.Size(1300, 544);
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

        // Вспомогательный метод для создания кнопок с единообразными настройками
        private Button CreateToolButton(string text, int left, int top, int width, int height)
        {
            Button button = new Button();
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            button.Location = new System.Drawing.Point(left, top);
            button.Size = new System.Drawing.Size(width, height);
            button.Text = text;
            return button;
        }
    }
}