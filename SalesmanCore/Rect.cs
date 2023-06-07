using System.Drawing;
using System.Text.Json.Serialization;

namespace SalesmanCore;

/// <summary>
/// Прямоугольник
/// </summary>
public class Rect
{
    public Rect()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="topLeft">Координата левого верхнего угла</param>
    /// <param name="bottomRight">Координата правого нижнего угла</param>
    public Rect(Point topLeft, Point bottomRight)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="left">Координата X левого верхнего угла</param>
    /// <param name="top">Координата Y левого верхнего угла</param>
    /// <param name="right">Координата X правого нижнего угла</param>
    /// <param name="bottom">Координата Y правого нижнего угла</param>
    public Rect(int left, int top, int right, int bottom)
    {
        TopLeft = new Point(left, top);
        BottomRight = new Point(right, bottom);
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="point">Координата точки, относительно которой строится прямоугольник</param>
    /// <param name="offset">Смещение по координатам X и Y. Ширина и высота полученного прямоугольника равна offset * 2 + 1</param>
    public Rect(Point point, int offset)
    {
        TopLeft = new Point(point.X - offset, point.Y - offset);
        BottomRight = new Point(point.X + offset, point.Y + offset);
    }

    #region Поля

    /// <summary>
    /// Координата правого нижнего угла
    /// </summary>
    public Point BottomRight;

    /// <summary>
    /// Координата левого верхнего угла
    /// </summary>
    public Point TopLeft;

    #endregion

    #region Свойства

    /// <summary>
    /// Координата X левого верхнего угла
    /// </summary>
    public int Left
    {
        get => TopLeft.X;
        set => TopLeft.X = value;
    }

    /// <summary>
    /// Координата Y левого верхнего угла
    /// </summary>
    public int Top
    {
        get => TopLeft.Y;
        set => TopLeft.Y = value;
    }

    /// <summary>
    /// Координата X правого нижнего угла
    /// </summary>
    public int Right
    {
        get => BottomRight.X;
        set => BottomRight.X = value;
    }

    /// <summary>
    /// Координата Y правого нижнего угла
    /// </summary>
    public int Bottom
    {
        get => BottomRight.Y;
        set => BottomRight.Y = value;
    }

    /// <summary>
    /// Центр прямоугольника
    /// </summary>
    [JsonIgnore]
    public Point Center => new(Left + Width / 2, Top + Height / 2);

    /// <summary>
    /// Высота прямоугольника
    /// </summary>
    [JsonIgnore]
    public int Height => Bottom - Top;

    /// <summary>
    /// Ширина прямоугольника
    /// </summary>
    [JsonIgnore]
    public int Width => Right - Left;

    #endregion

    #region Методы

    /// <summary>
    /// Проверяет на наличие пересечения двух прямоугольников
    /// </summary>
    /// <param name="rect">Прямоугольник для проверки пересечения</param>
    public bool IsIntersect(Rect rect)
    {
        return !Rectangle.Intersect(ToRectangle(), rect.ToRectangle()).IsEmpty;
    }

    /// <summary>
    /// Проверяет на принадлежность точки прямоугольнику
    /// </summary>
    /// <param name="point">Координаты точки для проверки принадлежности прямоугольнику</param>
    public bool IsPtInRect(Point point)
    {
        return Left <= point.X && Top <= point.Y && Right >= point.X && Bottom >= point.Y;
    }

    /// <summary>
    /// Смещение прямоугольника
    /// </summary>
    /// <param name="dX">Смещение по оси X</param>
    /// <param name="dY">Смещение по оси Y</param>
    public void Offset(int dX, int dY)
    {
        TopLeft.X += dX;
        TopLeft.Y += dY;
        BottomRight.X += dX;
        BottomRight.Y += dY;
    }

    /// <summary>
    /// Преобразует класс к Rectangle
    /// </summary>
    public Rectangle ToRectangle()
    {
        return new Rectangle(TopLeft.X, TopLeft.Y, BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y);
    }

    /// <summary>
    /// Преобразует класс к RectangleF
    /// </summary>
    public RectangleF ToRectangleF()
    {
        return new RectangleF(TopLeft.X, TopLeft.Y, BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y);
    }

    /// <summary>
    /// Преобразует атрибуты этого прямоугольника в удобную для восприятия строку 
    /// </summary>
    public override string ToString()
    {
        return $"Left = {Left} Top = {Top} Right = {Right} Bottom = {Bottom}";
    }

    #endregion
}