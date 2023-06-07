using System;
using System.Windows.Forms;
using SalesmanCore.Forms;

namespace SalesmanCore;

internal static class Program
{
    #region Ìועמהû

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormMain());
    }

    #endregion
}