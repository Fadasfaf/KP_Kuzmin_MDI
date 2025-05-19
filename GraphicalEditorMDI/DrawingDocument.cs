using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GraphicalEditorMDI
{
    public partial class DrawingDocument : Form
    {
        private Bitmap drawingBitmap;
        private Bitmap bufferBitmap;
        private Graphics drawingGraphics;
        private Graphics bufferGraphics;
        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;
        private bool isSelecting = false;
        private Rectangle selectionRect;
        private Bitmap copiedRegion;
        private string currentFileName;
        
        private enum DrawingMode { Line, Rectangle, Ellipse, Pentagon, Hexagon, Select, Paste }
        private DrawingMode currentMode = DrawingMode.Line;
        
        private Color penColor = Color.Black;
        private Color fillColor = Color.White;
        private int penWidth = 2;
        private bool fillShape = false;

        public DrawingDocument(int documentNumber)
        {
            InitializeComponent();
            this.Text = "Документ " + documentNumber;
            InitializeDrawingSurface();
            InitializeToolbar();
        }

        private void InitializeDrawingSurface()
        {
            drawingBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            bufferBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            drawingGraphics = Graphics.FromImage(drawingBitmap);
            bufferGraphics = Graphics.FromImage(bufferBitmap);
            
            drawingGraphics.Clear(Color.White);
            bufferGraphics.Clear(Color.White);
            
            pictureBox.Image = drawingBitmap;
        }

        private void InitializeToolbar()
        {
            // Инициализация обработчиков событий для кнопок и элементов управления
            btnLine.Click += (s, e) => { currentMode = DrawingMode.Line; };
            btnRectangle.Click += (s, e) => { currentMode = DrawingMode.Rectangle; };
            btnEllipse.Click += (s, e) => { currentMode = DrawingMode.Ellipse; };
            btnPentagon.Click += (s, e) => { currentMode = DrawingMode.Pentagon; };
            btnHexagon.Click += (s, e) => { currentMode = DrawingMode.Hexagon; };
            btnSelect.Click += (s, e) => { currentMode = DrawingMode.Select; };
            btnPaste.Click += (s, e) => { currentMode = DrawingMode.Paste; };
            
            btnPenColor.Click += (s, e) => 
            { 
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    penColor = colorDialog1.Color;
            };
            
            btnFillColor.Click += (s, e) => 
            { 
                if (colorDialog2.ShowDialog() == DialogResult.OK)
                    fillColor = colorDialog2.Color;
            };
            
            chkFill.CheckedChanged += (s, e) => { fillShape = chkFill.Checked; };
            trackBarPenWidth.ValueChanged += (s, e) => { penWidth = trackBarPenWidth.Value; };
            
            btnClear.Click += (s, e) => 
            {
                drawingGraphics.Clear(Color.White);
                pictureBox.Invalidate();
            };
            
            btnSave.Click += (s, e) => SaveImage();
            btnOpen.Click += (s, e) => OpenImage();
            btnCopy.Click += (s, e) => CopySelection();
            btnCut.Click += (s, e) => CutSelection();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                endPoint = e.Location;
                isDrawing = true;
                
                if (currentMode == DrawingMode.Select)
                {
                    isSelecting = true;
                    selectionRect = new Rectangle(e.Location, new Size(0, 0));
                }
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            endPoint = e.Location;
            
            // Очищаем буфер и копируем основное изображение
            bufferGraphics.Clear(Color.White);
            bufferGraphics.DrawImage(drawingBitmap, 0, 0);
            
            using (Pen pen = new Pen(penColor, penWidth))
            using (Brush brush = new SolidBrush(fillColor))
            {
                if (isSelecting)
                {
                    selectionRect = GetRectangle(startPoint, endPoint);
                    bufferGraphics.DrawRectangle(new Pen(Color.Blue, 1), selectionRect);
                }
                else
                {
                    switch (currentMode)
                    {
                        case DrawingMode.Line:
                            bufferGraphics.DrawLine(pen, startPoint, endPoint);
                            break;
                            
                        case DrawingMode.Rectangle:
                            Rectangle rect = GetRectangle(startPoint, endPoint);
                            bufferGraphics.DrawRectangle(pen, rect);
                            if (fillShape) bufferGraphics.FillRectangle(brush, rect);
                            break;
                            
                        case DrawingMode.Ellipse:
                            Rectangle ellipseRect = GetRectangle(startPoint, endPoint);
                            bufferGraphics.DrawEllipse(pen, ellipseRect);
                            if (fillShape) bufferGraphics.FillEllipse(brush, ellipseRect);
                            break;
                            
                        case DrawingMode.Pentagon:
                            Point[] pentagonPoints = CalculatePolygonPoints(startPoint, endPoint, 5);
                            bufferGraphics.DrawPolygon(pen, pentagonPoints);
                            if (fillShape) bufferGraphics.FillPolygon(brush, pentagonPoints);
                            break;
                            
                        case DrawingMode.Hexagon:
                            Point[] hexagonPoints = CalculatePolygonPoints(startPoint, endPoint, 6);
                            bufferGraphics.DrawPolygon(pen, hexagonPoints);
                            if (fillShape) bufferGraphics.FillPolygon(brush, hexagonPoints);
                            break;
                            
                        case DrawingMode.Paste:
                            if (copiedRegion != null)
                            {
                                bufferGraphics.DrawImage(copiedRegion, e.Location);
                            }
                            break;
                    }
                }
            }
            
            pictureBox.Image = bufferBitmap;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            isDrawing = false;
            isSelecting = false;
            
            // Рисуем окончательную фигуру на основном изображении
            using (Pen pen = new Pen(penColor, penWidth))
            using (Brush brush = new SolidBrush(fillColor))
            {
                switch (currentMode)
                {
                    case DrawingMode.Line:
                        drawingGraphics.DrawLine(pen, startPoint, endPoint);
                        break;
                        
                    case DrawingMode.Rectangle:
                        Rectangle rect = GetRectangle(startPoint, endPoint);
                        drawingGraphics.DrawRectangle(pen, rect);
                        if (fillShape) drawingGraphics.FillRectangle(brush, rect);
                        break;
                        
                    case DrawingMode.Ellipse:
                        Rectangle ellipseRect = GetRectangle(startPoint, endPoint);
                        drawingGraphics.DrawEllipse(pen, ellipseRect);
                        if (fillShape) drawingGraphics.FillEllipse(brush, ellipseRect);
                        break;
                        
                    case DrawingMode.Pentagon:
                        Point[] pentagonPoints = CalculatePolygonPoints(startPoint, endPoint, 5);
                        drawingGraphics.DrawPolygon(pen, pentagonPoints);
                        if (fillShape) drawingGraphics.FillPolygon(brush, pentagonPoints);
                        break;
                        
                    case DrawingMode.Hexagon:
                        Point[] hexagonPoints = CalculatePolygonPoints(startPoint, endPoint, 6);
                        drawingGraphics.DrawPolygon(pen, hexagonPoints);
                        if (fillShape) drawingGraphics.FillPolygon(brush, hexagonPoints);
                        break;
                        
                    case DrawingMode.Paste:
                        if (copiedRegion != null)
                        {
                            drawingGraphics.DrawImage(copiedRegion, e.Location);
                        }
                        break;
                }
            }
            
            pictureBox.Image = drawingBitmap;
        }

        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }

        private Point[] CalculatePolygonPoints(Point center, Point radiusPoint, int sides)
        {
            Point[] points = new Point[sides];
            double radius = Math.Sqrt(Math.Pow(radiusPoint.X - center.X, 2) + Math.Pow(radiusPoint.Y - center.Y, 2));
            double angle = 2 * Math.PI / sides;
            
            for (int i = 0; i < sides; i++)
            {
                points[i] = new Point(
                    center.X + (int)(radius * Math.Cos(i * angle - Math.PI / 2)),
                    center.Y + (int)(radius * Math.Sin(i * angle - Math.PI / 2)));
            }
            
            return points;
        }

        private void SaveImage()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentFileName = saveFileDialog.FileName;
                    string extension = System.IO.Path.GetExtension(currentFileName).ToLower();
                    ImageFormat format = ImageFormat.Png;
                    
                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                        case ".gif":
                            format = ImageFormat.Gif;
                            break;
                    }
                    
                    drawingBitmap.Save(currentFileName, format);
                    this.Text = System.IO.Path.GetFileName(currentFileName) + " - Графический редактор";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenImage()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image img = Image.FromFile(openFileDialog.FileName);
                    drawingBitmap = new Bitmap(img);
                    drawingGraphics = Graphics.FromImage(drawingBitmap);
                    pictureBox.Image = drawingBitmap;
                    currentFileName = openFileDialog.FileName;
                    this.Text = System.IO.Path.GetFileName(currentFileName) + " - Графический редактор";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CopySelection()
        {
            if (selectionRect.Width > 0 && selectionRect.Height > 0)
            {
                copiedRegion = new Bitmap(selectionRect.Width, selectionRect.Height);
                using (Graphics g = Graphics.FromImage(copiedRegion))
                {
                    g.DrawImage(drawingBitmap, new Rectangle(0, 0, copiedRegion.Width, copiedRegion.Height), 
                        selectionRect, GraphicsUnit.Pixel);
                }
            }
        }

        private void CutSelection()
        {
            if (selectionRect.Width > 0 && selectionRect.Height > 0)
            {
                CopySelection();
                drawingGraphics.FillRectangle(Brushes.White, selectionRect);
                pictureBox.Invalidate();
            }
        }
    }
}