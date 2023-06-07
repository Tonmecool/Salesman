using System;
using System.Drawing;

namespace SalesmanCore;

/// <summary>
/// Класс геометрических функций
/// </summary>
public class Geometry
{
    #region Методы

    /// <summary>
    /// Пересечение окружности и отрезка выходящего из центра окружности
    /// </summary>
    /// <param name="circleRect">Координаты окружности</param>
    /// <param name="point">Координата отрезка, начало которого находится в цетре окружности</param>
    public static Point CrossingCircleLine(Rect circleRect, Point point)
    {
        double newX, newY;
        var radius = (double)circleRect.Width / 2;
        var xx1 = circleRect.Left + radius;
        var yy1 = circleRect.Top + radius;
        var xx2 = point.X - xx1;
        var yy2 = point.Y - yy1;
        if (xx2 != 0)
        {
            newY = yy2 / xx2;
            newX = Math.Sqrt(radius * radius / (1 + newY * newY));
            if (xx2 < 0)
            {
                newX = -newX;
            }

            newY = newX * newY;
        }
        else
        {
            newX = 0;
            if (yy2 < 0)
            {
                newY = -radius;
            }
            else
            {
                newY = radius;
            }
        }

        return new Point((int)Math.Round(newX + xx1), (int)Math.Round(newY + yy1));
    }

    /// <summary>
    /// Возвращает координаты середины отрезка
    /// </summary>
    /// <param name="xStart">Координата начала линии по оси X</param>
    /// <param name="yStart">Координата начала линии по оси Y</param>
    /// <param name="xEnd">Координата окончания линии по оси X</param>
    /// <param name="yEnd">Координата окончания линии по оси Y</param>
    public static Point GetLineCenter(int xStart, int yStart, int xEnd, int yEnd)
    {
        return new Point((xStart + xEnd) / 2, (yStart + yEnd) / 2);
    }

    /// <summary>
    /// Возвращает координаты середины отрезка
    /// </summary>
    /// <param name="start">Координата начала линии</param>
    /// <param name="end">Координата окончания линии</param>
    public static Point GetLineCenter(Point start, Point end)
    {
        return GetLineCenter(start.X, start.Y, end.X, end.Y);
    }

    /// <summary>
    /// Возвращает координаты середины отрезка
    /// </summary>
    /// <param name="xStart">Координата начала линии по оси X</param>
    /// <param name="yStart">Координата начала линии по оси Y</param>
    /// <param name="xEnd">Координата окончания линии по оси X</param>
    /// <param name="yEnd">Координата окончания линии по оси Y</param>
    public static PointF GetLineCenterF(int xStart, int yStart, int xEnd, int yEnd)
    {
        return new PointF((xStart + xEnd) / 2f, (yStart + yEnd) / 2f);
    }

    /// <summary>
    /// Возвращает координаты середины отрезка
    /// </summary>
    /// <param name="start">Координата начала линии</param>
    /// <param name="end">Координата окончания линии</param>
    public static PointF GetLineCenterF(Point start, Point end)
    {
        return GetLineCenterF(start.X, start.Y, end.X, end.Y);
    }

    /// <summary>
    /// Возвращает длину отрезка
    /// </summary>
    /// <param name="xStart">Координата начала линии по оси X</param>
    /// <param name="yStart">Координата начала линии по оси Y</param>
    /// <param name="xEnd">Координата окончания линии по оси X</param>
    /// <param name="yEnd">Координата окончания линии по оси Y</param>
    public static double GetLineLength(int xStart, int yStart, int xEnd, int yEnd)
    {
        var xy = (xStart - xEnd) * (xStart - xEnd) + (yStart - yEnd) * (yStart - yEnd);

        return xy == 0 ? 0 : Math.Sqrt(xy);
    }

    /// <summary>
    /// Возвращает длину отрезка
    /// </summary>
    /// <param name="start">Координата начала линии</param>
    /// <param name="end">Координата окончания линии</param>
    public static double GetLineLength(Point start, Point end)
    {
        return GetLineLength(start.X, start.Y, end.X, end.Y);
    }

    #endregion
}