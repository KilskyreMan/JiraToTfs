using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace TicketImporter
{
    public class SettingsStore
    {
        private static string pathToStore
        {
            get
            {
                var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.Combine(location, "JiraToTfs.xml");
            }
        }

        private static Dictionary<string, string> formatKeys(IEnumerable<KeyValuePair<string, string>> toFormat)
        {
            return toFormat.ToDictionary(kv => Xml.Escape(kv.Key), kv => kv.Value);
        }

        public static Dictionary<String, String> Load(string key)
        {
            var mappings = new Dictionary<String, String>();
            if (File.Exists(pathToStore))
            {
                var xmlTree = XElement.Load(pathToStore);
                var element = xmlTree.Element(key);
                if (element != null)
                {
                    mappings = element.Elements().ToDictionary(k => k.Name.ToString(), v => v.Value);
                }
            }
            return formatKeys(mappings);
        }

        public static bool Save(string key, IEnumerable<KeyValuePair<string, string>> source)
        {
            var saved = true;
            try
            {
                var toSave = new XElement(key, formatKeys(source).Select(kv => new XElement(kv.Key, kv.Value)));
                if (toSave.HasElements)
                {
                    var xmlTree = new XElement("Mappings", toSave);
                    if (File.Exists(pathToStore))
                    {
                        xmlTree = XElement.Load(pathToStore);
                        var toReplace = xmlTree.Element(key);
                        if (toReplace == null)
                        {
                            xmlTree.Add(toSave);
                        }
                        else
                        {
                            toReplace.ReplaceWith(toSave);
                        }
                    }
                    xmlTree.Save(pathToStore);
                }
            }
            catch
            {
                saved = false;
                throw;
            }
            return saved;
        }
    }
}