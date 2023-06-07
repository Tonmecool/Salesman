using System.Drawing;

namespace SalesmanCore;

/// <summary>
/// Глобальные переменные и константы
/// </summary>
public static class GlobalVariables
{
    /// <summary>
    /// Цвет перехода
    /// </summary>
    public static Color CircleColor = Color.RoyalBlue;

    /// <summary>
    /// Радиус позиции
    /// </summary>
    public static int CircleRadius = 16;

    /// <summary>
    /// Цвет линии
    /// </summary>
    public static Color LineColor = Color.Black;

    /// <summary>
    /// Дельта для формирования прямоугольника на линии
    /// </summary>
    public const int LineDelta = 3;

    /// <summary>
    /// Шаг для рисования сетки
    /// </summary>
    public static int SetkaStep = 8;

    /// <summary>
    /// Цвет текста
    /// </summary>
    public static Color TextColor = Color.FromArgb(255, 128, 0, 0);
}