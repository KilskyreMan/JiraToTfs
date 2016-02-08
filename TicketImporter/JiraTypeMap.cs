using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketImporter
{
    public class JiraTypeMap
    {
        public JiraTypeMap(JiraProject jiraProject)
        {
            jiraTicketTypes = new List<string>();
            map = new Dictionary<string, Ticket.Type>();

            if (jiraProject != null)
            {
                var storedMap = SettingsStore.Load(key);
                jiraTicketTypes = jiraProject.GetAvailableTicketTypes();
                foreach (var jiraType in jiraTicketTypes)
                {
                    if (storedMap.ContainsKey(jiraType) == false)
                    {
                        map.Add(jiraType, defaultsTo(jiraType));
                    }
                    else
                    {
                        map.Add(jiraType, (Ticket.Type) Enum.Parse(typeof (Ticket.Type), storedMap[jiraType], true));
                    }
                }
            }
        }

        public int Count
        {
            get { return map.Count; }
        }

        public Ticket.Type this[string lookUp]
        {
            get { return map[lookUp]; }
        }

        public IEnumerable<KeyValuePair<string, string>> Mappings
        {
            get { return map.Select(kp => new KeyValuePair<string, string>(kp.Key, kp.Value.ToString())); }
        }

        public IEnumerable<string> AvailableTicketTypes
        {
            get
            {
                return from object ticketType in Enum.GetValues(typeof (Ticket.Type))
                    where (Ticket.Type) ticketType != Ticket.Type.Unknown
                    select ticketType.ToString();
            }
        }

        public void Save(IEnumerable<KeyValuePair<string, string>> source)
        {
            map.Clear();
            var keyValuePairs = source as KeyValuePair<string, string>[] ?? source.ToArray();
            foreach (var kp in keyValuePairs)
            {
                map.Add(kp.Key, (Ticket.Type) Enum.Parse(typeof (Ticket.Type), kp.Value, true));
            }
            SettingsStore.Save(key, keyValuePairs);
        }

        public void RestoreDefaults()
        {
            map = new Dictionary<string, Ticket.Type>();
            foreach (var jiraType in jiraTicketTypes)
            {
                map.Add(jiraType, defaultsTo(jiraType));
            }

            var toSave = map.ToDictionary(kp => kp.Key, kp => kp.Value.ToString());
            SettingsStore.Save(key, toSave);
        }

        #region private class members

        private Dictionary<string, Ticket.Type> map;
        private readonly List<string> jiraTicketTypes;
        private const string key = "TicketTypes";

        private Ticket.Type defaultsTo(string jiraType)
        {
            var mapped = Ticket.Type.Task;

            var toMatch = jiraType.ToUpper();
            if (toMatch.Contains("RISK"))
            {
                mapped = Ticket.Type.Risk;
            }
            else if (toMatch.Contains("BUG") || toMatch.Contains("DEFECT") ||
                     toMatch.Contains("INCIDENT") || toMatch.Contains("PROBLEM"))
            {
                mapped = Ticket.Type.Bug;
            }
            else if (toMatch.Contains("IMPEDIMENT"))
            {
                mapped = Ticket.Type.Impediment;
            }
            else if (toMatch.Contains("STORY") ||
                     toMatch.Contains("NEW FEATURE") || toMatch.Contains("IMPROVEMENT"))
            {
                mapped = Ticket.Type.Story;
            }
            else if (toMatch.Contains("DECISION"))
            {
                mapped = Ticket.Type.Decision;
            }
            else if (toMatch.Contains("EPIC"))
            {
                mapped = Ticket.Type.Epic;
            }
            else if (toMatch.Contains("TEST CASE") || toMatch.Contains("TESTCASE"))
            {
                mapped = Ticket.Type.TestCase;
            }
            return mapped;
        }

        #endregion
    }
}