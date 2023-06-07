using System.Drawing;
using System.Windows.Forms;
using SalesmanCore.Forms;

namespace SalesmanCore.Commands;

/// <summary>
/// Команда удаления позиций и переходов
/// </summary>
public class CommandDelCircle : CommandBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="project">Проект для которого создается команда</param>
    public CommandDelCircle(FormMain project) : base(project)
    {
    }

    #region Методы

    /// <summary>
    /// Отмена выполнения команды
    /// </summary>
    public override void Break()
    {
    }

    /// <summary>
    /// Рисование
    /// </summary>
    /// <param name="g">Поверхность рисования</param>
    public override void Draw(Graphics g)
    {
    }

    /// <summary>
    /// Начало выполнения команды
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public override void Start(int x, int y)
    {
        if (Project.DeleteCircle(x, y))
        {
            Project.Modified = true;
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
    }

    public override string ToString()
    {
        return "Команда: Позиция → Удалить";
    }

    #endregion
}