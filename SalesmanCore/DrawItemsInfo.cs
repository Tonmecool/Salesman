using System.Drawing;
using System.Drawing.Drawing2D;

namespace SalesmanCore;

/// <summary>
/// Класс элементов для рисования
/// </summary>
public static class DrawItemsInfo
{
    /// <summary>
    /// Кисть для рисования позиции
    /// </summary>
    public static Brush CircleBrush = Brushes.White;

    /// <summary>
    /// Карандаш для рисования позиции
    /// </summary>
    public static Pen CirclePen = new(GlobalVariables.CircleColor);

    /// <summary>
    /// Карандаш для рисования линии
    /// </summary>
    public static Pen LinePen = new(GlobalVariables.LineColor);

    /// <summary>
    /// Кисть для рисования текста
    /// </summary>
    public static Brush TextBrush = new SolidBrush(GlobalVariables.TextColor);

    /// <summary>
    /// Формат для рисования текста
    /// </summary>
    public static StringFormat TextFormat = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

    #region Методы

    /// <summary>
    /// Рисование сетки
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    /// <param name="size">Размер сетки</param>
    /// <param name="setkaStep">Шаг сетки</param>
    public static void DrawSetka(Graphics g, Size size, int setkaStep)
    {
        using var drawSetkaBrushHorizontal = new LinearGradientBrush(new Point(0, 0), new Point(setkaStep / 2, 0),
            Color.FromArgb(155, 211, 211, 211),
            Color.FromArgb(155, 240, 240, 240)) { WrapMode = WrapMode.TileFlipX };
        using var drawSetkaPenHorizontal = new Pen(drawSetkaBrushHorizontal);
        using var drawSetkaBrushVertical = new LinearGradientBrush(new Point(0, 0), new Point(0, setkaStep / 2),
            Color.FromArgb(155, 211, 211, 211),
            Color.FromArgb(155, 240, 240, 240)) { WrapMode = WrapMode.TileFlipX };
        using var drawSetkaPenVertical = new Pen(drawSetkaBrushVertical);

        for (var i = 0; i <= size.Width; i += setkaStep)
        {
            g.DrawLine(drawSetkaPenVertical, i, 0, i, size.Height);
        }

        for (var i = 0; i <= size.Height; i += setkaStep)
        {
            g.DrawLine(drawSetkaPenHorizontal, 0, i, size.Width, i);
        }
    }

    #endregion
}