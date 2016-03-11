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

using System.Diagnostics;
using System.Windows.Forms;

namespace JiraToTfs.View
{
    public partial class MissingTfsDependenciesView : Form
    {
        public MissingTfsDependenciesView()
        {
            InitializeComponent();
        }

        private void OnClickTellMeMore(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://visualstudiogallery.msdn.microsoft.com/f30e5cc7-036e-449c-a541-d522299445aa");
            Close();
        }
    }
}
