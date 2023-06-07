using System.Drawing;
using System.Windows.Forms;
using SalesmanCore.Enums;
using SalesmanCore.Forms;
using SalesmanCore.Graphs;

namespace SalesmanCore.Commands;

/// <summary>
/// Команда рисования связей
/// </summary>
public class CommandDrawLine : CommandBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект для которого создается команда</param>
    public CommandDrawLine(FormMain project) : base(project)
    {
    }

    #region Поля

    /// <summary>
    /// Позиция откуда начинается рисование линии
    /// </summary>
    private GraphCircle _circle;

    /// <summary>
    /// Линия
    /// </summary>
    private GraphLine _line;

    #endregion

    #region Методы

    /// <summary>
    /// Отмена выполнения команды
    /// </summary>
    public override void Break()
    {
        base.Break();
        _line = null;
        Project.PaintBox.Invalidate();
    }

    /// <summary>
    /// Рисование
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public override void Draw(Graphics g)
    {
        if (_line != null)
        {
            _line.DrawAlways(g);
            _line.DrawPointsAlways(g);
            DrawCross(g, X, Y);
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

        _line.PointList[_line.PointList.Count - 1] = new Point(x, y);
        _line.Merge();
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
            var center = _circle.Gs.Center;
            base.Start(center.X, center.Y);
            _line = new GraphLine(Project) { InCircle = _circle };
            _line.PointList.Add(new Point(X, Y));
            _line.PointList.Add(new Point(X, Y));
            _line.Merge();
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
        var circle = Project.GetCircle(X, Y);
        if (circle == null)
        {
            _line.PointList.Add(new Point(X, Y));
            Project.PaintBox.Invalidate();

            return;
        }

        if (circle == _circle)
        {
            MessageBox.Show("Связываемые элементы должны быть различны!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }

        if (_circle.GetCircles().Contains(circle))
        {
            MessageBox.Show("Элементы уже связаны!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;
        }

        _line.Status = TypeStatus.Show;
        _line.OutCircle = circle;
        _line.Merge();
        Project.AddLine(_line);
        _line = null;
        Project.PaintBox.Invalidate();
        base.Stop();
    }

    public override string ToString()
    {
        return "Команда: Связь → Добавить";
    }

    #endregion
}