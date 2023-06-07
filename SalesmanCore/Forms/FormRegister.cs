using System;
using System.Linq;
using System.Windows.Forms;
using SalesmanCore.DataAccess.Models;
using SalesmanCore.Helpers;

namespace SalesmanCore.Forms;

public partial class FormRegister : Form
{
    public FormRegister()
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
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                MessageBox.Show("Логин не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxPass.Text))
            {
                MessageBox.Show("Пароль не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (textBoxPass.Text != textBoxPass2.Text)
            {
                MessageBox.Show("Пароли должны совпадать!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var user = FormMain.Db.Users.FirstOrDefault(p => p.Login == textBoxLogin.Text);
            if (user != null)
            {
                MessageBox.Show("Пользователь с указанным логином уже существует в БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            user = new User
            {
                Login = textBoxLogin.Text,
                Password = HashHelper.GetHash(textBoxPass.Text)
            };
            FormMain.Db.Users.Add(user);
            FormMain.Db.SaveChanges();

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

    private void textBoxPass2_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter or Keys.Return)
        {
            SelectNextControl((Control)sender, true, true, true, true);
        }
    }

    #endregion
}