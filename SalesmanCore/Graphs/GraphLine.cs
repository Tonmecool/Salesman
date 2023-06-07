using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using SalesmanCore.Enums;
using SalesmanCore.Forms;

namespace SalesmanCore.Graphs;

/// <summary>
/// Линия состоящая из отрезков
/// </summary>
public class GraphLine : GraphBase
{
    public GraphLine() : base()
    {
        Status = TypeStatus.Show;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект</param>
    public GraphLine(FormMain project) : base(project)
    {
        Status = TypeStatus.Hide;
    }

    #region Поля

    private GraphCircle _inCircle;

    private GraphCircle _outCircle;

    /// <summary>
    /// Статус элемента при отрисовке
    /// </summary>
    public TypeStatus Status;

    #endregion

    #region Свойства

    /// <summary>
    /// Входная позиция
    /// </summary>
    [JsonIgnore]
    public GraphCircle InCircle
    {
        get => _inCircle;
        set
        {
            _inCircle = value;
            InCircleId = _inCircle == null ? Guid.Empty : _inCircle.Id;
        }
    }

    /// <summary>
    /// Выходная позиция
    /// </summary>
    [JsonIgnore]
    public GraphCircle OutCircle
    {
        get => _outCircle;
        set
        {
            _outCircle = value;
            OutCircleId = _outCircle == null ? Guid.Empty : _outCircle.Id;
        }
    }

    /// <summary>
    /// Список координат точек из которых соостоит линия
    /// </summary>
    public List<Point> PointList { get; set; } = new();

    /// <summary>
    /// Идентификатор входной позиции
    /// </summary>
    public Guid InCircleId { get; set; }

    /// <summary>
    /// Идентификатор выходной позиции
    /// </summary>
    public Guid OutCircleId { get; set; }

    #endregion

    #region Методы

    /// <summary>
    /// Вызывается после десериализации
    /// </summary>
    /// <param name="project">Проект</param>
    public override void AfterDeserialize(FormMain project)
    {
        base.AfterDeserialize(project);

        InCircle = project.Data.Circles.First(p => p.Id == InCircleId);
        OutCircle = project.Data.Circles.First(p => p.Id == OutCircleId);
    }

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
    /// <param name="g">Поверхность рисования</param>
    public void DrawAlways(Graphics g)
    {
        g.DrawLines(DrawItemsInfo.LinePen, PointList.ToArray());
    }

    /// <summary>
    /// Рисование точек на линии
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public void DrawPoints(Graphics g)
    {
        if (Status == TypeStatus.Show)
        {
            DrawPointsAlways(g);
        }
    }

    /// <summary>
    /// Рисование точек на линии не зависимо от статуса
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public void DrawPointsAlways(Graphics g)
    {
        using var b = new SolidBrush(Color.FromArgb(70, 0, 128, 0));
        using var p = new Pen(Color.FromArgb(70, 0, 0, 0));
        for (var i = 1; i < PointList.Count; i++)
        {
            if (i < PointList.Count - 1)
            {
                var point = PointList[i];
                var rect = new Rect(point, GlobalVariables.LineDelta).ToRectangle();
                g.DrawRectangle(p, rect);
                g.FillRectangle(b, rect);
            }

            if (IsCenter(PointList[i - 1], PointList[i]))
            {
                var pointF = Geometry.GetLineCenterF(PointList[i - 1], PointList[i]);
                var rectF = new RectangleF(pointF.X - GlobalVariables.LineDelta, pointF.Y - GlobalVariables.LineDelta,
                    GlobalVariables.LineDelta * 2, GlobalVariables.LineDelta * 2);
                g.DrawEllipse(p, rectF);
                g.FillEllipse(b, rectF);
            }
        }
    }

    /// <summary>
    /// Возвращает длину линии
    /// </summary>
    public int GetLength()
    {
        double result = 0;
        for (var i = 0; i < PointList.Count - 1; i++)
        {
            result += Geometry.GetLineLength(PointList[i], PointList[i + 1]);
        }

        return Convert.ToInt32(Math.Round(result));
    }

    /// <summary>
    /// Возвращает True, если возможно рисование центра отрезка
    /// </summary>
    /// <param name="start">Координата начала отрезка</param>
    /// <param name="stop">Координата окончания отрезка</param>
    public bool IsCenter(Point start, Point stop)
    {
        var len = Geometry.GetLineLength(start, stop);

        return len >= GlobalVariables.SetkaStep * 4;
    }

    /// <summary>
    /// Пересечение линии и входного и выходного элементов
    /// </summary>
    public void Merge()
    {
        if (PointList.Count < 2)
        {
            return;
        }

        if (InCircle != null)
        {
            PointList[0] = InCircle.Merge(PointList[1]);
            if (OutCircle != null)
            {
                PointList[PointList.Count - 1] = OutCircle.Merge(PointList[PointList.Count - 2]);
            }
        }
        else
        {
            if (OutCircle != null)
            {
                PointList[PointList.Count - 1] = OutCircle.Merge(PointList[PointList.Count - 2]);
            }
        }
    }

    #endregion
}