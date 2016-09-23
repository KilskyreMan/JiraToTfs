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
using System.Linq;

namespace TicketImporter
{
    public delegate void PriorityMapFailure(string failure);

    public class TfsPriorityMap
    {
        public TfsPriorityMap()
        {
            map = SettingsStore.Load(key);
            if (map.Count == 0)
            {
                RestoreDefaults();
            }
        }

        public string PriorityField
        {
            get { return map["Field"]; }
        }

        public List<string> JiraPriorities
        {
            get { return (map.Keys.Where(k => !k.Equals("Field")).ToList()); }
        }

        public string this[string lookUp]
        {
            get
            {
                var priority = "";
                if (string.IsNullOrWhiteSpace(lookUp) == false)
                {
                    priority = map[lookUp];
                }
                return priority;
            }
        }

        public void Save(string priorityField, IEnumerable<KeyValuePair<string, string>> source)
        {
            var toSave = source as IList<KeyValuePair<string, string>> ?? source.ToList();
            toSave.Add(new KeyValuePair<string, string>("Field", priorityField));
            SettingsStore.Save(key, toSave);
        }

        public void RestoreDefaults()
        {
            map = new Dictionary<string, string>
            {
                {"Field", "Priority"},
                /*Microsoft.VSTS.Common.Priority*/
                {"Blocker", "1"},
                {"Critical", "1"},
                {"Major", "2"},
                {"Medium", "3" },
                {"Minor", "4"},
                {"Trivial", "4"}
            };
            SettingsStore.Save(key, map);
        }

        #region private class members

        private const string key = "WorkItemPriority";
        private Dictionary<string, string> map;

        #endregion
    }
}