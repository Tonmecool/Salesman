using System;
using System.Drawing;
using SalesmanCore.Forms;

namespace SalesmanCore.Commands;

/// <summary>
/// Базовый класс для выполнения команд
/// </summary>
public abstract class CommandBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект для которого создается команда</param>
    protected CommandBase(FormMain project)
    {
        Project = project;
        Project.FreeCommand();
    }

    #region Поля

    /// <summary>
    /// Проект
    /// </summary>
    protected FormMain Project;

    /// <summary>
    /// Координата X откуда началось рисование (для отмены)
    /// </summary>
    protected int StX;

    /// <summary>
    /// Координата Y откуда началось рисование (для отмены)
    /// </summary>
    protected int StY;

    /// <summary>
    /// Текущая координата X
    /// </summary>
    protected int X;

    /// <summary>
    /// Текущая координата Y
    /// </summary>
    protected int Y;

    #endregion

    #region Свойства

    /// <summary>
    /// True, если в режиме работы
    /// </summary>
    public bool IsStarting { get; protected set; }

    #endregion

    #region Методы

    /// <summary>
    /// Отмена выполнения команды
    /// </summary>
    public virtual void Break()
    {
        IsStarting = false;
    }

    /// <summary>
    /// Рисование
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public abstract void Draw(Graphics g);

    /// <summary>
    /// Рисует крест на всю ширину и высоту
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    /// <param name="x">Координата X центра креста</param>
    /// <param name="y">Координата Y центра креста</param>
    public void DrawCross(Graphics g, int x, int y)
    {
        using var p = new Pen(Color.FromArgb(70, 0, 255, 0));
        g.DrawLine(p, x, 0, x, Project.PaintBox.Height);
        g.DrawLine(p, 0, y, Project.PaintBox.Width, y);
    }

    /// <summary>
    /// Рисует крест на всю ширину и высоту
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    /// <param name="point">Координата центра креста</param>
    public void DrawCross(Graphics g, Point point)
    {
        DrawCross(g, point.X, point.Y);
    }

    /// <summary>
    /// Перемещение координаты точки
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public virtual bool Move(int x, int y)
    {
        return false;
    }

    /// <summary>
    /// Начало выполнения команды
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public virtual void Start(int x, int y)
    {
        IsStarting = true;
        X = StX = ToSetka(x);
        Y = StY = ToSetka(y);
    }

    /// <summary>
    /// Окончание выполнения команды
    /// </summary>
    public virtual void Stop()
    {
        IsStarting = false;
        Project.Modified = true;
    }

    /// <summary>
    /// Привязывает значение к ближайшему узлу сетки
    /// </summary>
    /// <param name="value">Значение для привязки</param>
    protected int ToSetka(int value)
    {
        return (int)Math.Round((double)value / GlobalVariables.SetkaStep) * GlobalVariables.SetkaStep;
    }

    /// <summary>
    /// Привязывает точку к ближайшему узлу сетки
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    protected Point ToSetka(int x, int y)
    {
        return new Point(ToSetka(x), ToSetka(y));
    }

    /// <summary>
    /// Привязывает точку к ближайшему узлу сетки
    /// </summary>
    /// <param name="point">Координата точки</param>
    protected Point ToSetka(Point point)
    {
        return new Point(ToSetka(point.X), ToSetka(point.Y));
    }

    #endregion
}