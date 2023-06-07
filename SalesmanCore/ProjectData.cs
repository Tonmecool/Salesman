using System.Collections.Generic;
using SalesmanCore.Forms;
using SalesmanCore.Graphs;

namespace SalesmanCore;

/// <summary>
/// Данные проекта
/// </summary>
public class ProjectData
{
    #region Свойства

    /// <summary>
    /// Список позиций
    /// </summary>
    public List<GraphCircle> Circles { get; set; } = new();

    /// <summary>
    /// Список линий
    /// </summary>
    public List<GraphLine> Lines { get; set; } = new();

    #endregion

    #region Методы

    /// <summary>
    /// Вызывается после десериализации
    /// </summary>
    /// <param name="project">Проект</param>
    public virtual void AfterDeserialize(FormMain project)
    {
        foreach (var circle in Circles)
        {
            circle.AfterDeserialize(project);
        }

        foreach (var line in Lines)
        {
            line.AfterDeserialize(project);
        }
    }

    /// <summary>
    /// Возвращает матрицу связности
    /// </summary>
    public int?[,] GetMatrix()
    {
        var result = new int?[Circles.Count, Circles.Count];
        foreach (var line in Lines)
        {
            var length = line.GetLength();
            result[line.InCircle.Number - 1, line.OutCircle.Number - 1] = length;
            result[line.OutCircle.Number - 1, line.InCircle.Number - 1] = length;
        }

        return result;
    }

    /// <summary>
    /// Возвращает номер для новой позиции
    /// </summary>
    public int GetNewCircleNumber()
    {
        for (var i = 1; i < int.MaxValue; i++)
        {
            if (!Circles.Exists(p => p.Number == i))
            {
                return i;
            }
        }

        return 0;
    }

    #endregion
}