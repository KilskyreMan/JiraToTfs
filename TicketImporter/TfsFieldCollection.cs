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
using System.Linq;

namespace TicketImporter
{
    public class TfsFieldCollection
    {
        #region private class members
        private readonly List<TfsField> fields;
        #endregion

        public TfsFieldCollection()
        {
            fields = new List<TfsField>();
        }

        public void Add(TfsField field)
        {
            if (fields.Contains(field) == false)
            {
                fields.Add(field);
            }
        }

        public int Count
        {
            get { return fields.Count; }
        }

        public IEnumerable<string> Names
        {
            get
            {
                return fields.Select(field => field.Name);
            }
        } 
 
        public TfsField this[string name]
        {
            get
            {
                return fields.FirstOrDefault(field => string.Equals(field.Name, name, StringComparison.OrdinalIgnoreCase));
            }
        }

        public IEnumerable<string> EditableFields
        {
            get
            {
                return from field in fields where field.IsEditable select field.Name;
            }
        }

    }
}
