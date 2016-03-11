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
using TicketImporter.Interface;

namespace TicketImporter
{
    public class JiraTypeMap
    {
        #region private class members

        private Dictionary<string, string> map;
        private readonly List<string> jiraTicketTypes;
        private readonly IAvailableTicketTypes availableTypes;
        private const string key = "TicketTypes";

        private string defaultsTo(string jiraType)
        {
            var mapped = availableTypes.Task;
            var toMatch = jiraType.ToUpper();

            if (toMatch.Contains("RISK"))
            {
                mapped = availableTypes.Risk;
            }
            else if (toMatch.Contains("BUG") || toMatch.Contains("DEFECT") || toMatch.Contains("SUPPORT"))
            {
                mapped = availableTypes.Bug;
            }
            else if (toMatch.Contains("IMPEDIMENT") || toMatch.Contains("INCIDENT") || toMatch.Contains("PROBLEM"))
            {
                mapped = availableTypes.Impediment;
            }
            else if (toMatch.Contains("STORY"))
            {
                mapped = availableTypes.Story;
            }
            else if (toMatch.Contains("FEATURE") || toMatch.Contains("IMPROVEMENT"))
            {
                mapped = availableTypes.Epic;
            }
            else if (toMatch.Contains("DECISION"))
            {
                mapped = availableTypes.Decision;
            }
            else if (toMatch.Contains("EPIC"))
            {
                mapped = availableTypes.Epic;
            }
            else if (toMatch.Contains("TEST CASE") || toMatch.Contains("TESTCASE"))
            {
                mapped = availableTypes.TestCase;
            }
            return mapped;
        }
        #endregion

        public JiraTypeMap(ITicketSource jiraProject, IAvailableTicketTypes availableTicektTypes)
        {
            map = new Dictionary<string, string>();
            jiraTicketTypes = jiraProject.GetAvailableTicketTypes();
            availableTypes = availableTicektTypes;

            if (jiraTicketTypes.Count > 0)
            {
                map = SettingsStore.Load(key);
                var updateStore = false;
                foreach (
                    var jiraType in
                        jiraTicketTypes.Where(
                            jiraType =>
                                map.ContainsKey(jiraType) == false || availableTypes.Contains(map[jiraType]) == false))
                {
                    map[jiraType] = defaultsTo(jiraType);
                    updateStore = true;
                }

                if (updateStore)
                {
                    SettingsStore.Save(key, map);
                }
            }
        }

        public int Count
        {
            get { return map.Count; }
        }

        public string this[string lookUp]
        {
            get { return map[lookUp]; }
        }

        public IEnumerable<KeyValuePair<string, string>> Mappings
        {
            get { return map.Select(kp => new KeyValuePair<string, string>(kp.Key, kp.Value)); }
        }

        public IEnumerable<string> AvailableTicketTypes
        {
            get { return availableTypes.Types; }
        }

        public void Save(IEnumerable<KeyValuePair<string, string>> source)
        {
            map.Clear();
            var keyValuePairs = source as KeyValuePair<string, string>[] ?? source.ToArray();
            foreach (var kp in keyValuePairs)
            {
                map.Add(kp.Key, kp.Value);
            }
            SettingsStore.Save(key, keyValuePairs);
        }

        public void RestoreDefaults()
        {
            map = new Dictionary<string, string>();
            foreach (var jiraType in jiraTicketTypes)
            {
                map.Add(jiraType, defaultsTo(jiraType));
            }
            SettingsStore.Save(key, map);
        }
    }
}