using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SalesmanCore.Enums;
using SalesmanCore.Forms;

namespace SalesmanCore.Graphs;

/// <summary>
/// Позиция
/// </summary>
public class GraphCircle : GraphBase
{
    public GraphCircle() : base()
    {
        Status = TypeStatus.Show;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект</param>
    /// <param name="x">Координата X центра</param>
    /// <param name="y">Координата Y центра</param>
    public GraphCircle(FormMain project, int x, int y) : base(project)
    {
        Gs = new Rect(new Point(x, y), GlobalVariables.CircleRadius);
        Number = project.Data.GetNewCircleNumber();
    }

    #region Поля

    /// <summary>
    /// Статус элемента при отрисовке
    /// </summary>
    public TypeStatus Status = TypeStatus.Hide;

    #endregion

    #region Свойства

    public int Number { get; set; }

    #endregion

    #region Методы

    /// <summary>
    /// Рисование
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public override void Draw(Graphics g)
    {
        if (Status == TypeStatus.Show)
        {
            DrawAlways(g);
        }
    }

    /// <summary>
    /// Рисование не зависимо от статуса
    /// </summary>
    /// <param name="g"></param>
    public void DrawAlways(Graphics g)
    {
        g.FillEllipse(DrawItemsInfo.CircleBrush, Gs.ToRectangle());
        g.DrawEllipse(DrawItemsInfo.CirclePen, Gs.ToRectangle());
        g.DrawString(Number.ToString(), Project.PaintBox.Font, DrawItemsInfo.TextBrush, Gs.ToRectangleF(), DrawItemsInfo.TextFormat);
    }

    /// <summary>
    /// Возвращает список подключенных позиций
    /// </summary>
    /// <returns></returns>
    public List<GraphCircle> GetCircles()
    {
        var result = new List<GraphCircle>();
        var lines = GetLines();
        foreach (var line in lines)
        {
            if (line.InCircle != this)
            {
                result.Add(line.InCircle);
            }

            if (line.OutCircle != this)
            {
                result.Add(line.OutCircle);
            }
        }

        return result;
    }

    /// <summary>
    /// Возвращает список линий
    /// </summary>
    public List<GraphLine> GetLines()
    {
        return Project.Data.Lines.Where(p => p.InCircle == this || p.OutCircle == this).ToList();
    }

    /// <summary>
    /// Находит точку пересечения окружности и отрезка выходящего из центра окружности
    /// </summary>
    /// <param name="point">Координата окончания отрезка</param>
    public Point Merge(Point point)
    {
        return Geometry.CrossingCircleLine(Gs, point);
    }

    /// <summary>
    /// Смещение положения элемента по координатам
    /// </summary>
    /// <param name="dX">Координата X</param>
    /// <param name="dY">Координата Y</param>
    public void Offset(int dX, int dY)
    {
        Gs.Offset(dX, dY);
    }

    #endregion
}