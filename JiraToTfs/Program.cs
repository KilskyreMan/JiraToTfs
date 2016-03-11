#region License
/*
    This source makes up part of JiraToTfs, a utility for migrating Jira
    tickets to Microsoft TFS.

    Copyright(C) 2016  Ian Montgomery

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/
#endregion

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