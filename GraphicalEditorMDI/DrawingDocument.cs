// DrawingDocument.cs
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GraphicalEditorMDI
{
    public partial class DrawingDocument : Form
    {
        private Bitmap drawingBitmap;
        private Graphics drawingGraphics;
        private Point startPoint, endPoint;
        private bool isDrawing = false, isSelecting = false, hasSelection = false;
        private Rectangle selectionBorder, selectionRect;
        private Bitmap copiedRegion;
        private string currentFileName;
        private ContextMenuStrip contextMenu;

        private enum DrawingMode { Line, Rectangle, Ellipse, Pentagon, Hexagon, Select, Paste }
        private DrawingMode currentMode = DrawingMode.Line;

        private Color penColor = Color.Black, fillColor = Color.White;
        private int penWidth = 2;
        private bool fillShape = false;

        public DrawingDocument(int documentNumber)
        {
            InitializeComponent();
            Text = "Документ " + documentNumber;
            InitializeDrawingSurface();
            InitializeToolbar();
            InitializeContextMenu();

            pictureBox.Paint += PictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            Resize += (s, e) => RedrawOnResize();
        }

        private void RedrawOnResize()
        {
            Bitmap old = drawingBitmap;
            InitializeDrawingSurface();
            if (old != null)
            {
                drawingGraphics.DrawImage(old, 0, 0);
                old.Dispose();
            }
            pictureBox.Invalidate();
        }

        private void InitializeDrawingSurface()
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height - toolPanel.Height;
            if (width <= 0 || height <= 0) return;

            drawingBitmap = new Bitmap(width, height);
            drawingGraphics = Graphics.FromImage(drawingBitmap);
            drawingGraphics.Clear(Color.White);
            pictureBox.Size = new Size(width, height);
            pictureBox.Image = drawingBitmap;
        }

        private void InitializeToolbar()
        {
            btnLine.Click += (s, e) => currentMode = DrawingMode.Line;
            btnRectangle.Click += (s, e) => currentMode = DrawingMode.Rectangle;
            btnEllipse.Click += (s, e) => currentMode = DrawingMode.Ellipse;
            btnPentagon.Click += (s, e) => currentMode = DrawingMode.Pentagon;
            btnHexagon.Click += (s, e) => currentMode = DrawingMode.Hexagon;
            btnSelect.Click += (s, e) => currentMode = DrawingMode.Select;
            btnPaste.Click += (s, e) => btnPaste_Click(s, e);

            btnPenColor.Click += (s, e) => { if (colorDialog1.ShowDialog() == DialogResult.OK) penColor = colorDialog1.Color; };
            btnFillColor.Click += (s, e) => { if (colorDialog2.ShowDialog() == DialogResult.OK) fillColor = colorDialog2.Color; };
            chkFill.CheckedChanged += (s, e) => fillShape = chkFill.Checked;
            trackBarPenWidth.ValueChanged += (s, e) => penWidth = trackBarPenWidth.Value;

            btnClear.Click += (s, e) => { drawingGraphics.Clear(Color.White); hasSelection = false; pictureBox.Invalidate(); };
            btnSave.Click += (s, e) => SaveImage();
            btnOpen.Click += (s, e) => OpenImage();
            btnCopy.Click += (s, e) => CopySelection();
            btnCut.Click += (s, e) => CutSelection();
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.AddRange(new ToolStripItem[] {
                new ToolStripMenuItem("Вставить", null, (s, e) => { currentMode = DrawingMode.Paste; btnPaste.PerformClick(); }),
                new ToolStripMenuItem("Копировать", null, (s, e) => CopySelection()),
                new ToolStripMenuItem("Вырезать", null, (s, e) => CutSelection()),
                new ToolStripMenuItem("Вставить в новое окно", null, (s, e) => {
                    if (copiedRegion != null && MdiParent is MainForm parent)
                        parent.PasteToNewDocument(copiedRegion);
                }) });
            pictureBox.ContextMenuStrip = contextMenu;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            startPoint = endPoint = e.Location;
            isDrawing = true;
            if (currentMode == DrawingMode.Select)
            {
                isSelecting = true;
                hasSelection = false;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || e.Button != MouseButtons.Left) return;
            endPoint = e.Location;
            if (isSelecting)
            {
                selectionRect = GetRectangle(startPoint, endPoint);
                pictureBox.Invalidate();
            }
            else
            {
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            isDrawing = false;

            if (isSelecting)
            {
                selectionRect = GetRectangle(startPoint, endPoint);
                selectionBorder = selectionRect;
                hasSelection = true;
                isSelecting = false;
                pictureBox.Invalidate();
            }
            else
            {
                using (Pen pen = new Pen(penColor, penWidth))
                using (Brush brush = new SolidBrush(fillColor))
                {
                    var r = GetRectangle(startPoint, endPoint);
                    switch (currentMode)
                    {
                        case DrawingMode.Line: drawingGraphics.DrawLine(pen, startPoint, endPoint); break;
                        case DrawingMode.Rectangle: drawingGraphics.DrawRectangle(pen, r); if (fillShape) drawingGraphics.FillRectangle(brush, r); break;
                        case DrawingMode.Ellipse: drawingGraphics.DrawEllipse(pen, r); if (fillShape) drawingGraphics.FillEllipse(brush, r); break;
                        case DrawingMode.Pentagon:
                        case DrawingMode.Hexagon:
                            var points = CalculatePolygonPoints(startPoint, endPoint, currentMode == DrawingMode.Pentagon ? 5 : 6);
                            drawingGraphics.DrawPolygon(pen, points);
                            if (fillShape) drawingGraphics.FillPolygon(brush, points);
                            break;
                        case DrawingMode.Paste:
                            if (copiedRegion != null)
                            {
                                drawingGraphics.DrawImage(copiedRegion, e.Location);
                                currentMode = DrawingMode.Line;
                            }
                            break;
                    }
                }
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing && !isSelecting)
            {
                using (Pen pen = new Pen(penColor, penWidth))
                using (Brush brush = new SolidBrush(fillColor))
                {
                    var r = GetRectangle(startPoint, endPoint);
                    switch (currentMode)
                    {
                        case DrawingMode.Line: e.Graphics.DrawLine(pen, startPoint, endPoint); break;
                        case DrawingMode.Rectangle: e.Graphics.DrawRectangle(pen, r); if (fillShape) e.Graphics.FillRectangle(brush, r); break;
                        case DrawingMode.Ellipse: e.Graphics.DrawEllipse(pen, r); if (fillShape) e.Graphics.FillEllipse(brush, r); break;
                        case DrawingMode.Pentagon:
                        case DrawingMode.Hexagon:
                            var points = CalculatePolygonPoints(startPoint, endPoint, currentMode == DrawingMode.Pentagon ? 5 : 6);
                            e.Graphics.DrawPolygon(pen, points); if (fillShape) e.Graphics.FillPolygon(brush, points); break;
                    }
                }
            }

            if (isSelecting || hasSelection)
            {
                using (Pen pen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                    e.Graphics.DrawRectangle(pen, isSelecting ? selectionRect : selectionBorder);
            }
        }

        private Rectangle GetRectangle(Point p1, Point p2) =>
            new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

        private Point[] CalculatePolygonPoints(Point center, Point radiusPoint, int sides)
        {
            Point[] points = new Point[sides];
            double radius = Math.Sqrt(Math.Pow(radiusPoint.X - center.X, 2) + Math.Pow(radiusPoint.Y - center.Y, 2));
            double angle = 2 * Math.PI / sides;
            for (int i = 0; i < sides; i++)
                points[i] = new Point(
                    center.X + (int)(radius * Math.Cos(i * angle - Math.PI / 2)),
                    center.Y + (int)(radius * Math.Sin(i * angle - Math.PI / 2)));
            return points;
        }

        private void SaveImage()
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                currentFileName = saveFileDialog.FileName;
                var ext = System.IO.Path.GetExtension(currentFileName).ToLower();
                ImageFormat format = ImageFormat.Png;
                if (ext == ".jpg" || ext == ".jpeg") format = ImageFormat.Jpeg;
                else if (ext == ".bmp") format = ImageFormat.Bmp;
                else if (ext == ".gif") format = ImageFormat.Gif;
                drawingBitmap.Save(currentFileName, format);
                Text = System.IO.Path.GetFileName(currentFileName) + " - Графический редактор";
            }
            catch (Exception ex) { MessageBox.Show("Ошибка при сохранении файла: " + ex.Message); }
        }

        private void OpenImage()
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                drawingBitmap = new Bitmap(Image.FromFile(openFileDialog.FileName));
                drawingGraphics = Graphics.FromImage(drawingBitmap);
                pictureBox.Image = drawingBitmap;
                currentFileName = openFileDialog.FileName;
                Text = System.IO.Path.GetFileName(currentFileName) + " - Графический редактор";
                hasSelection = false;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка при открытии файла: " + ex.Message); }
        }

        private void CopySelection()
        {
            if (!hasSelection) return;
            Rectangle rect = new Rectangle(selectionBorder.X + 1, selectionBorder.Y + 1,
                                           selectionBorder.Width - 2, selectionBorder.Height - 2);
            copiedRegion = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(copiedRegion))
                g.DrawImage(drawingBitmap, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            hasSelection = false;
        }

        private void CutSelection()
        {
            if (!hasSelection) return;
            CopySelection();
            Rectangle rect = new Rectangle(selectionBorder.X + 1, selectionBorder.Y + 1,
                                           selectionBorder.Width - 2, selectionBorder.Height - 2);
            drawingGraphics.FillRectangle(Brushes.White, rect);
            hasSelection = false;
            pictureBox.Invalidate();
        }

        public void PasteImage(Bitmap image)
        {
            copiedRegion = new Bitmap(image);
            currentMode = DrawingMode.Paste;
            MessageBox.Show("Готово к вставке. Кликните в нужное место.");
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (copiedRegion != null)
            {
                currentMode = DrawingMode.Paste;
                MessageBox.Show("Готово к вставке. Кликните в нужное место.");
            }
            else MessageBox.Show("Нет данных для вставки.");
        }
    }
}