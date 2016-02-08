using System;
using System.Reflection;
using System.Windows.Forms;
using JiraToTfs.View;
using log4net;

namespace JiraToTfs
{
    internal static class Program
    {
        private static readonly ILog log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new JiraToTfsView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "JiraToTfs threw an exception and needs to shut down.\nCheck log in Application folder for further details.");
                log.Error(ex.ToString());
            }
        }
    }
}