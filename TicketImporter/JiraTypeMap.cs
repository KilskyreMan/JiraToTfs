using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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