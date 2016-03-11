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

using System.Collections.Generic;

namespace TicketImporter
{
    public class TfsIssue
    {
        #region private class members

        private readonly object value;

        #endregion

        public TfsIssue(string name, string problem, object value, List<string> info)
        {
            Name = name;
            Problem = problem;
            this.value = value;
            Info = new List<string>(info);
        }

        public string Name { get; private set; }
        public string Problem { get; private set; }

        public string Value
        {
            get { return (value != null ? value.ToString() : "<no value>"); }
        }

        public List<string> Info { get; private set; }
    }
}