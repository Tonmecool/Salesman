using System;
using System.Linq;
using System.Windows.Forms;
using SalesmanCore.DataAccess.Models;
using SalesmanCore.Helpers;

namespace SalesmanCore.Forms;

public partial class FormAuthorization : Form
{
    public FormAuthorization()
    {
        InitializeComponent();
    }

    #region Свойства

    public User User { get; private set; }

    #endregion

    #region События

    private void buttonOk_Click(object sender, EventArgs e)
    {
        Cursor.Current = Cursors.WaitCursor;
        try
        {
            var user = FormMain.Db.Users.FirstOrDefault(p => p.Login == textBoxLogin.Text);
            if (user == null)
            {
                MessageBox.Show("Ошибка авторизации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!HashHelper.Compare(textBoxPass.Text, user.Password))
            {
                MessageBox.Show("Ошибка авторизации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            User = user;
            DialogResult = DialogResult.OK;
        }
        finally
        {
            Cursor.Current = Cursors.Default;
        }
    }

    private void textBoxLogin_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter or Keys.Return)
        {
            SelectNextControl((Control)sender, true, true, true, true);
        }
    }

    private void textBoxPass_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter or Keys.Return)
        {
            SelectNextControl((Control)sender, true, true, true, true);
        }
    }

    #endregion
}