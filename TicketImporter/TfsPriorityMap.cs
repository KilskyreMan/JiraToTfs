/*================================================================================================================================
Copyright (c) 2015 Ian Montgomery

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:
    
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
================================================================================================================================*/

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
                {"Minor", "3"},
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