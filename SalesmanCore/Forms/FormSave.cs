using System;
using System.Linq;
using System.Windows.Forms;
using SalesmanCore.DataAccess.Models;

namespace SalesmanCore.Forms;

public partial class FormSave : Form
{
    public FormSave()
    {
        InitializeComponent();
    }

    public FormSave(User user, string fileName, string fileJson)
    {
        InitializeComponent();
        _user = user;
        textBoxFileName.Text = fileName;
        _fileJson = fileJson;
        var files = FormMain.Db.UserFiles.Where(p => p.UserId == user.Id).OrderBy(p => p.FileName).ToArray();
        listBoxFile.Items.AddRange(files);
        buttonOk.Enabled = !string.IsNullOrWhiteSpace(textBoxFileName.Text);
    }

    #region Поля

    private readonly string _fileJson;

    private readonly User _user;

    #endregion

    #region Свойства

    public UserFile UserFile { get; private set; }

    #endregion

    #region События

    private void buttonOk_Click(object sender, EventArgs e)
    {
        var userFile = FormMain.Db.UserFiles.FirstOrDefault(p => p.UserId == _user.Id && p.FileName == textBoxFileName.Text);
        if (userFile != null)
        {
            if (MessageBox.Show($"Файл: {userFile.FileName} уже существует.\nВы действительно хотите его заменить?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                userFile.FileJson = _fileJson;
                FormMain.Db.SaveChanges();
                UserFile = userFile;
                DialogResult = DialogResult.OK;
            }
        }
        else
        {
            userFile = new UserFile
            {
                UserId = _user.Id,
                FileName = textBoxFileName.Text,
                FileJson = _fileJson
            };
            FormMain.Db.UserFiles.Add(userFile);
            FormMain.Db.SaveChanges();
            UserFile = userFile;
            DialogResult = DialogResult.OK;
        }
    }

    private void listBoxFile_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        var index = listBoxFile.IndexFromPoint(e.Location);
        if (index >= 0)
        {
            var userFile = (UserFile)listBoxFile.Items[index];
            if (MessageBox.Show($"Вы действительно собираетесь заменить файл: {userFile.FileName} ?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                userFile.FileJson = _fileJson;
                FormMain.Db.SaveChanges();
                UserFile = userFile;
                DialogResult = DialogResult.OK;
            }
        }
    }

    private void textBoxFileName_TextChanged(object sender, EventArgs e)
    {
        buttonOk.Enabled = !string.IsNullOrWhiteSpace(textBoxFileName.Text);
    }

    #endregion
}