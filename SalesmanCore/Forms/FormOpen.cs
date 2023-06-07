using System;
using System.Linq;
using System.Windows.Forms;
using SalesmanCore.DataAccess.Models;

namespace SalesmanCore.Forms;

public partial class FormOpen : Form
{
    public FormOpen()
    {
        InitializeComponent();
    }

    public FormOpen(User user)
    {
        InitializeComponent();
        var files = FormMain.Db.UserFiles.Where(p => p.UserId == user.Id).OrderBy(p => p.FileName).ToArray();
        listBoxFile.Items.AddRange(files);
        buttonOk.Enabled = listBoxFile.SelectedIndex >= 0;
    }

    #region Свойства

    public UserFile UserFile { get; private set; }

    #endregion

    #region События

    private void buttonOk_Click(object sender, EventArgs e)
    {
        UserFile = (UserFile)listBoxFile.SelectedItem;
        DialogResult = DialogResult.OK;
    }

    private void listBoxFile_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        var index = listBoxFile.IndexFromPoint(e.Location);
        if (index >= 0)
        {
            UserFile = (UserFile)listBoxFile.Items[index];
            DialogResult = DialogResult.OK;
        }
    }

    private void listBoxFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        buttonOk.Enabled = listBoxFile.SelectedIndex >= 0;
        textBoxFileName.Text = ((UserFile)listBoxFile.SelectedItem)?.FileName;
    }

    #endregion
}