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