using System;
using System.Drawing;
using SalesmanCore.Forms;

namespace SalesmanCore.Graphs;

/// <summary>
/// Базовый класс для всех графических объектов
/// </summary>
public abstract class GraphBase
{
    protected GraphBase()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект</param>
    protected GraphBase(FormMain project)
    {
        Project = project;
    }

    #region Поля

    /// <summary>
    /// Проект
    /// </summary>
    protected FormMain Project;

    #endregion

    #region Свойства

    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Координаты положения
    /// </summary>
    public Rect Gs { get; set; }

    #endregion

    #region Методы

    /// <summary>
    /// Вызывается после десериализации
    /// </summary>
    /// <param name="project">Проект</param>
    public virtual void AfterDeserialize(FormMain project)
    {
        Project = project;
    }

    /// <summary>
    /// Рисование
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public abstract void Draw(Graphics g);

    #endregion
}