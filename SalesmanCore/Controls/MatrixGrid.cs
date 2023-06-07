using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SalesmanCore.Controls;

/// <summary>
/// Кликабельный контрол для отображения матриц (https://www.cyberforum.ru/blogs/529033/blog3296.html)
/// </summary>
public class MatrixGrid : UserControl
{
    public MatrixGrid()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
    }

    #region Делегаты

    public event EventHandler<CellClickEventArgs> CellClick;

    public event EventHandler<CellNeededEventArgs> CellNeeded;

    #endregion

    #region Поля

    public Point HoveredCell = new(-1, -1);

    #endregion

    #region Свойства

    public Size GridSize { get; set; }

    #endregion

    #region Методы

    protected virtual void OnCellClick(CellClickEventArgs cellClickEventArgs)
    {
        if (CellClick != null)
        {
            CellClick(this, cellClickEventArgs);
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            var cell = PointToCell(e.Location);
            OnCellClick(new CellClickEventArgs(cell));
            HoveredCell = cell;
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        var cell = PointToCell(e.Location);
        HoveredCell = cell;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var gr = e.Graphics;
        gr.SmoothingMode = SmoothingMode.HighQuality;
        gr.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        if (CellNeeded == null)
        {
            return;
        }

        var cw = ClientSize.Width / GridSize.Width;
        var ch = ClientSize.Height / GridSize.Height;

        for (var j = 0; j < GridSize.Height; j++)
        for (var i = 0; i < GridSize.Width; i++)
        {
            var cell = new Point(i, j);

            //получаем значение ячейки от пользователя
            var ea = new CellNeededEventArgs(cell);
            CellNeeded(this, ea);

            //рисуем ячейку
            var rect = new Rectangle(cw * i, ch * j, cw, ch);
            rect.Inflate(-1, -1);

            if (cell == HoveredCell)
            {
                gr.DrawRectangle(Pens.Red, rect);
            }

            //фон
            if (ea.BackColor != Color.Transparent)
            {
                using var brush = new SolidBrush(ea.BackColor);
                gr.FillRectangle(brush, rect);
            }

            //текст
            if (!string.IsNullOrEmpty(ea.Value))
            {
                gr.DrawString(ea.Value, Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
        }
    }

    private Point PointToCell(Point p)
    {
        var cw = ClientSize.Width / GridSize.Width;
        var ch = ClientSize.Height / GridSize.Height;
        return new Point(p.X / cw, p.Y / ch);
    }

    #endregion

    public class CellClickEventArgs : EventArgs
    {
        public CellClickEventArgs(Point cell)
        {
            Cell = cell;
        }

        #region Свойства

        public Point Cell { get; }

        #endregion
    }

    public class CellNeededEventArgs : EventArgs
    {
        public CellNeededEventArgs(Point cell)
        {
            Cell = cell;
        }

        #region Свойства

        public Point Cell { get; }

        public string Value { get; set; }

        public Color BackColor { get; set; }

        #endregion
    }
}