using System.Collections.Generic;
using System.Linq;

namespace TicketImporter
{
    public class TfsFieldMap
    {
        public TfsFieldMap(TfsProject tfsProject)
        {
            map = new Dictionary<string, string>();
            this.tfsProject = tfsProject;

            if (this.tfsProject != null)
            {
                var storedMap = SettingsStore.Load(key);
                foreach (var fieldName in this.tfsProject.WorkItemFields)
                {
                    if (storedMap.ContainsKey(fieldName) == false)
                    {
                        map.Add(fieldName, "");
                    }
                    else
                    {
                        map.Add(fieldName, storedMap[fieldName]);
                    }
                }
            }
        }

        public int Count
        {
            get { return map.Count; }
        }

        public IEnumerable<KeyValuePair<string, string>> Fields
        {
            get { return map; }
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

        #region private class members

        private readonly Dictionary<string, string> map;
        private readonly TfsProject tfsProject;
        private const string key = "TfsFieldValues";

        #endregion
    }
}