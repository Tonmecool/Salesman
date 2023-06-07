using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;
using SalesmanCore.Commands;
using SalesmanCore.DataAccess;
using SalesmanCore.DataAccess.Models;
using SalesmanCore.Graphs;

namespace SalesmanCore.Forms;

public partial class FormMain : Form
{
    public FormMain()
    {
        InitializeComponent();
        Command = null;
        User = null;

        // https://stackoverflow.com/questions/818415/how-do-i-double-buffer-a-panel
        typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, PaintBox, new object[] { true });
    }

    #region Поля

    private CommandBase _command;

    private User _user;

    #endregion

    #region Свойства

    /// <summary>
    /// Активный проект
    /// </summary>
    public ProjectData Data { get; set; } = new();

    /// <summary>
    /// Активная команда
    /// </summary>
    public CommandBase Command
    {
        get => _command;
        set
        {
            _command = value;
            statusAction.Text = _command?.ToString();
        }
    }

    /// <summary>
    /// Открытый файл
    /// </summary>
    private UserFile UserFile { get; set; }

    /// <summary>
    /// Текущий пользователь
    /// </summary>
    public User User
    {
        get => _user;
        set
        {
            _user = value;
            menuFile.Enabled = _user != null;
            menuEdit.Enabled = _user != null;
            menuCalc.Enabled = _user != null;
        }
    }

    /// <summary>
    /// Проект изменен
    /// </summary>
    public bool Modified { get; set; }

    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public static Db Db { get; } = new();

    #endregion

    #region Методы

    /// <summary>
    /// Добавление новой позиции
    /// </summary>
    /// <param name="circle">Позиция</param>
    public bool AddCircle(GraphCircle circle)
    {
        var result = SetAvailable(circle);
        if (result)
        {
            Data.Circles.Add(circle);
        }

        return result;
    }

    /// <summary>
    /// Добавляет новую линию
    /// </summary>
    /// <param name="line">Линия</param>
    public int AddLine(GraphLine line)
    {
        Data.Lines.Add(line);

        return Data.Lines.Count - 1;
    }

    /// <summary>
    /// Отмена выполнения команды
    /// </summary>
    public void BreakCommand()
    {
        if (Command != null && Command.IsStarting)
        {
            Command.Break();
            PaintBox.Invalidate();
        }
    }

    /// <summary>
    /// Создание новой команды
    /// </summary>
    /// <param name="commandBase">Команда</param>
    public void CreateCommand(CommandBase commandBase)
    {
        FreeCommand();
        Command = commandBase;
        PaintBox.Invalidate();
    }

    /// <summary>
    /// Удаляет позицию, которой принадлежит точка
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public bool DeleteCircle(int x, int y)
    {
        var circle = GetCircle(x, y);
        if (circle != null)
        {
            var lines = circle.GetLines();
            var result = Data.Circles.Remove(circle);
            if (result)
            {
                Data.Lines.RemoveAll(p => lines.Contains(p));
                for (var i = 0; i < Data.Circles.Count; i++)
                {
                    Data.Circles[i].Number = i + 1;
                }
            }

            return result;
        }

        return false;
    }

    /// <summary>
    /// Удаляет линию, которой принадлежит точка
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public bool DeleteLine(int x, int y)
    {
        var line = GetLine(x, y, out _);
        if (line != null)
        {
            return Data.Lines.Remove(line);
        }

        line = GetLineCenter(x, y, out _);
        if (line != null)
        {
            return Data.Lines.Remove(line);
        }

        return false;
    }

    /// <summary>
    /// Удаление существующей команды
    /// </summary>
    public void FreeCommand()
    {
        if (Command != null)
        {
            if (Command.IsStarting)
            {
                Command.Break();
            }

            Command = null;
        }

        PaintBox.Invalidate();
    }

    /// <summary>
    /// Поиск позиции, которой принадлежит точка
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    public GraphCircle GetCircle(int x, int y)
    {
        var point = new Point(x, y);
        foreach (var circle in Data.Circles)
        {
            if (circle.Gs.IsPtInRect(point))
            {
                return circle;
            }
        }

        return null;
    }

    /// <summary>
    /// Поиск линии, которой принадлежит точка на концах отрезка
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    /// <param name="index">Возвращает индекс в массиве точек</param>
    public GraphLine GetLine(int x, int y, out int index)
    {
        var r = new Rect(new Point(x, y), GlobalVariables.LineDelta);
        foreach (var line in Data.Lines)
        {
            for (var i = 0; i < line.PointList.Count; i++)
            {
                index = i;
                if (r.IsPtInRect(line.PointList[i]))
                {
                    if (index == 0 || index == line.PointList.Count - 1)
                    {
                        break;
                    }

                    return line;
                }
            }
        }

        index = -1;

        return null;
    }

    /// <summary>
    /// Поиск линии, которой принадлежит точка на середине отрезка
    /// </summary>
    /// <param name="x">Координата X точки</param>
    /// <param name="y">Координата Y точки</param>
    /// <param name="index">Возвращает индекс в массиве точек, которым заканчивается отрезок</param>
    public GraphLine GetLineCenter(int x, int y, out int index)
    {
        var r = new Rect(new Point(x, y), GlobalVariables.LineDelta);
        foreach (var line in Data.Lines)
        {
            for (var i = 1; i < line.PointList.Count; i++)
            {
                index = i;
                if (line.IsCenter(line.PointList[i - 1], line.PointList[i]) && r.IsPtInRect(Geometry.GetLineCenter(line.PointList[i - 1], line.PointList[i])))
                {
                    return line;
                }
            }
        }

        index = -1;

        return null;
    }

    /// <summary>
    /// Проверка на возможность применить координаты местоположения для позиции
    /// </summary>
    /// <param name="value">Позиция</param>
    public bool SetAvailable(GraphCircle value)
    {
        foreach (var circle in Data.Circles)
        {
            if (circle != value && circle.Gs.IsIntersect(value.Gs))
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region События

    private void menuAuthorization_Click(object sender, EventArgs e)
    {
        var f = new FormAuthorization();
        if (f.ShowDialog() == DialogResult.OK)
        {
            User = f.User;
        }
    }

    private void menuCalc_Click(object sender, EventArgs e)
    {
        if (Data.Circles.Count < 2)
        {
            MessageBox.Show("Количество нарисованных позиций должно быть не менее двух.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return;
        }

        var f = new FormCalc(Data.GetMatrix());
        f.ShowDialog();
    }

    private void menuCircleAdd_Click(object sender, EventArgs e)
    {
        CreateCommand(new CommandDrawCircle(this));
    }

    private void menuCircleDelete_Click(object sender, EventArgs e)
    {
        CreateCommand(new CommandDelCircle(this));
    }

    private void menuCircleMove_Click(object sender, EventArgs e)
    {
        CreateCommand(new CommandMoveCircle(this));
    }

    private void menuFileNew_Click(object sender, EventArgs e)
    {
        BreakCommand();

        if (Modified)
        {
            var dlgResult = MessageBox.Show("Файл изменен. Вы хотите сохранить перед открытием?", "Подтверждение",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                menuFileSave.PerformClick();
                if (Modified)
                {
                    return;
                }
            }

            if (dlgResult == DialogResult.Cancel)
            {
                return;
            }
        }

        Data = new ProjectData();
        Modified = false;
        UserFile = null;
        PaintBox.Invalidate();
    }

    private void menuFileOpen_Click(object sender, EventArgs e)
    {
        BreakCommand();

        if (Modified)
        {
            var dlgResult = MessageBox.Show("Файл изменен. Вы хотите сохранить перед открытием?", "Подтверждение",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                menuFileSave.PerformClick();
                if (Modified)
                {
                    return;
                }
            }

            if (dlgResult == DialogResult.Cancel)
            {
                return;
            }
        }

        var f = new FormOpen(_user);
        if (f.ShowDialog() == DialogResult.OK)
        {
            var userFile = f.UserFile;
            Data = JsonSerializer.Deserialize<ProjectData>(userFile.FileJson);
            Data.AfterDeserialize(this);
            Modified = false;
            UserFile = userFile;
            PaintBox.Invalidate();
        }
    }

    private void menuFileSave_Click(object sender, EventArgs e)
    {
        BreakCommand();
        if (UserFile != null)
        {
            UserFile.FileJson = JsonSerializer.Serialize(Data);
            Db.SaveChanges();
            Modified = false;
            MessageBox.Show($"Файл: {UserFile.FileName}\nУспешно сохранен в БД", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            menuFileSaveAs.PerformClick();
        }
    }

    private void menuFileSaveAs_Click(object sender, EventArgs e)
    {
        BreakCommand();

        var f = new FormSave(_user, UserFile?.FileName, JsonSerializer.Serialize(Data));
        if (f.ShowDialog() == DialogResult.OK)
        {
            UserFile = f.UserFile;
            Modified = false;
            MessageBox.Show($"Файл: {UserFile.FileName}\nУспешно сохранен в БД", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void menuLineAdd_Click(object sender, EventArgs e)
    {
        CreateCommand(new CommandDrawLine(this));
    }

    private void menuLineDelete_Click(object sender, EventArgs e)
    {
        CreateCommand(new CommandDelLine(this));
    }

    private void menuLineMove_Click(object sender, EventArgs e)
    {
        CreateCommand(new CommandMoveLine(this));
    }

    private void menuRegister_Click(object sender, EventArgs e)
    {
        var f = new FormRegister();
        if (f.ShowDialog() == DialogResult.OK)
        {
            User = f.User;
        }
    }

    private void menuReset_Click(object sender, EventArgs e)
    {
        CreateCommand(null);
    }

    private void PaintBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (Command != null && Command.IsStarting)
        {
            if (Command.Move(e.Location.X, e.Location.Y))
            {
                PaintBox.Invalidate();
            }
        }
    }

    private void PaintBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (Command != null)
            {
                if (Command.IsStarting)
                {
                    Command.Stop();
                }
                else
                {
                    Command.Start(e.X, e.Y);
                }
            }

            return;
        }

        if (e.Button == MouseButtons.Right)
        {
            if (Command != null && Command.IsStarting)
            {
                Command.Break();
            }
        }
    }

    private void PaintBox_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        DrawItemsInfo.DrawSetka(e.Graphics, PaintBox.Size, GlobalVariables.SetkaStep);
        foreach (var circle in Data.Circles)
        {
            circle.Draw(e.Graphics);
        }

        foreach (var line in Data.Lines)
        {
            line.Draw(e.Graphics);
        }

        if (Command is CommandDelLine || Command is CommandMoveLine)
        {
            foreach (var line in Data.Lines)
            {
                line.DrawPoints(e.Graphics);
            }
        }

        if (Command != null && Command.IsStarting)
        {
            Command.Draw(e.Graphics);
        }
    }

    #endregion
}