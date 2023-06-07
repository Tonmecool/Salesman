using System.Drawing;
using System.Windows.Forms;
using SalesmanCore.Enums;
using SalesmanCore.Forms;
using SalesmanCore.Graphs;

namespace SalesmanCore.Commands;

/// <summary>
/// Команда рисования позиции
/// </summary>
public class CommandDrawCircle : CommandBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект для которого создается команда</param>
    public CommandDrawCircle(FormMain project) : base(project)
    {
    }

    #region Поля

    /// <summary>
    /// Позиция
    /// </summary>
    private GraphCircle _circle;

    #endregion

    #region Методы

    /// <summary>
    /// Отмена выполнения команды
    /// </summary>
    public override void Break()
    {
        base.Break();
        _circle = null;
        Project.PaintBox.Invalidate();
    }

    /// <summary>
    /// Рисование
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public override void Draw(Graphics g)
    {
        if (_circle != null)
        {
            _circle.DrawAlways(g);
            DrawCross(g, ToSetka(_circle.Gs.Center));
        }
    }

    /// <summary>
    /// Перемещение координаты точки
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public override bool Move(int x, int y)
    {
        x = ToSetka(x);
        y = ToSetka(y);
        if (X == x && Y == y)
        {
            return false;
        }

        _circle.Offset(x - X, y - Y);
        X = x;
        Y = y;

        return true;
    }

    /// <summary>
    /// Начало выполнения команды
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public override void Start(int x, int y)
    {
        base.Start(x, y);
        _circle = new GraphCircle(Project, X, Y);
        Project.PaintBox.Invalidate();
    }

    /// <summary>
    /// Окончание выполнения команды
    /// </summary>
    public override void Stop()
    {
        if (Project.AddCircle(_circle))
        {
            _circle.Status = TypeStatus.Show;
            _circle = null;
            Project.PaintBox.Invalidate();
            base.Stop();
        }
        else
        {
            MessageBox.Show("В данной области уже существует элемент!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public override string ToString()
    {
        return "Команда: Позиция → Добавить";
    }

    #endregion
}