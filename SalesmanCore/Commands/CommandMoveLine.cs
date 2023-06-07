using System.Drawing;
using System.Windows.Forms;
using SalesmanCore.Enums;
using SalesmanCore.Forms;
using SalesmanCore.Graphs;

namespace SalesmanCore.Commands;

/// <summary>
/// Команда перемещения связей
/// </summary>
public class CommandMoveLine : CommandBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект для которого создается команда</param>
    public CommandMoveLine(FormMain project) : base(project)
    {
    }

    #region Поля

    /// <summary>
    /// Индекс точки, которую перемещаем
    /// </summary>
    private int _indexPoint;

    /// <summary>
    /// True, если новая точка из центра отрезка
    /// </summary>
    private bool _isNew;

    /// <summary>
    /// Линия которая перемещается
    /// </summary>
    private GraphLine _line;

    /// <summary>
    /// Координата точки, которую перемещаем (для отмены)
    /// </summary>
    private Point _pointSt;

    #endregion

    #region Методы

    /// <summary>
    /// Отмена выполнения команды
    /// </summary>
    public override void Break()
    {
        base.Break();
        _line.Status = TypeStatus.Show;
        if (_isNew)
        {
            _line.PointList.RemoveAt(_indexPoint);
        }
        else
        {
            _line.PointList[_indexPoint] = _pointSt;
        }

        _line.Merge();
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

        if (_line != null)
        {
            _line.PointList[_indexPoint] = new Point(x, y);
            _line.Merge();
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
        _line = Project.GetLine(x, y, out _indexPoint);
        if (_line != null)
        {
            base.Start(x, y);
            _isNew = false;
            _line.Status = TypeStatus.Hide;
            _pointSt = _line.PointList[_indexPoint];
            Project.PaintBox.Invalidate();
        }
        else
        {
            _line = Project.GetLineCenter(x, y, out _indexPoint);
            if (_line != null)
            {
                base.Start(x, y);
                _isNew = true;
                _line.Status = TypeStatus.Hide;
                _line.PointList.Insert(_indexPoint, ToSetka(new Point(x, y)));
                Project.PaintBox.Invalidate();
            }
            else
            {
                MessageBox.Show("В выбранной точке связь не найдена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    /// <summary>
    /// Окончание выполнения команды
    /// </summary>
    public override void Stop()
    {
        _line.Status = TypeStatus.Show;
        Project.PaintBox.Invalidate();
        base.Stop();
    }

    public override string ToString()
    {
        return "Команда: Связь → Переместить";
    }

    #endregion
}