using System.Drawing;
using System.Windows.Forms;
using SalesmanCore.Enums;
using SalesmanCore.Forms;
using SalesmanCore.Graphs;

namespace SalesmanCore.Commands;

/// <summary>
/// Команда перемещения позиций
/// </summary>
public class CommandMoveCircle : CommandBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект для которого создается команда</param>
    public CommandMoveCircle(FormMain project) : base(project)
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
        Move(StX, StY);
        _circle.Status = TypeStatus.Show;
        foreach (var line in _circle.GetLines())
        {
            line.Merge();
            line.Status = TypeStatus.Show;
        }

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
            foreach (var line in _circle.GetLines())
            {
                line.DrawAlways(g);
            }

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

        var dx = x - X;
        var dy = y - Y;
        _circle.Offset(dx, dy);
        foreach (var line in _circle.GetLines())
        {
            line.Merge();
        }

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
        _circle = Project.GetCircle(x, y);
        if (_circle != null)
        {
            base.Start(x, y);
            _circle.Status = TypeStatus.Hide;
            foreach (var line in _circle.GetLines())
            {
                line.Status = TypeStatus.Hide;
            }

            Project.PaintBox.Invalidate();
        }
        else
        {
            MessageBox.Show("В выбранной точке элемент не найден!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    /// Окончание выполнения команды
    /// </summary>
    public override void Stop()
    {
        if (Project.SetAvailable(_circle))
        {
            _circle.Status = TypeStatus.Show;
            foreach (var line in _circle.GetLines())
            {
                line.Status = TypeStatus.Show;
            }

            base.Stop();
            _circle = null;
            Project.PaintBox.Invalidate();
        }
        else
        {
            MessageBox.Show("В данной области уже существует элемент!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public override string ToString()
    {
        return "Команда: Позиция → Переместить";
    }

    #endregion
}