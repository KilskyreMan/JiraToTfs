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
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TicketImporter
{
    public class TfsField
    {
        #region private class members
        private readonly FieldDefinition fd;
        #endregion

        public string Name
        {
            get { return fd.Name; }
        }

        public bool SupportsHtml
        {
            get { return fd.FieldType == FieldType.Html; }
        }

        public List<string> AllowedValues
        {
            get
            {
                var allowedValues = new List<string>();
                if (fd.AllowedValues != null)
                {
                    allowedValues.AddRange(fd.AllowedValues.Cast<string>());
                }
                return allowedValues;
            }
        }

        public bool IsRequired { get; private set; }

        public bool IsCore
        {
            get { return fd.IsCoreField; }
        }

        public string Help
        {
            get { return fd.HelpText; }
        }

        public object ToFieldValue(object value)
        {
            object typedValue = value;
            if (value != null)
            {
                string stringValue = value.ToString();
                switch (fd.FieldType)
                {
                    case FieldType.Boolean:
                        bool typedBool;
                        bool.TryParse(stringValue, out typedBool);
                        typedValue = typedBool;
                        break;
                    case FieldType.DateTime:
                        DateTime typedDateTime;
                        DateTime.TryParse(stringValue, out typedDateTime);
                        typedValue = typedDateTime;
                        break;
                    case FieldType.Double:
                        Double typedDouble;
                        Double.TryParse(stringValue, out typedDouble);
                        typedValue = typedDouble;
                        break;
                    case FieldType.Integer:
                        int typedInt;
                        int.TryParse(stringValue, out typedInt);
                        typedValue = typedInt;
                        break;
                }
            }
            return typedValue;
        }

        public string DefaultValue { get; set; }

        public bool IsEditable
        {
            get
            {
                return (fd.IsEditable && fd.IsComputed == false && fd.IsCoreField == false && fd.IsInternal == false);
            }
        }

        public TfsField(FieldDefinition fd, bool isRequired)
        {
            this.fd = fd;
            DefaultValue = "";
            IsRequired = isRequired;
        }

        public override bool Equals(object obj)
        {
            var isEqual = false;
            var toCompare = obj as TfsField;
            if (toCompare != null)
            {
                isEqual = string.Equals(fd.Name, toCompare.fd.Name, StringComparison.OrdinalIgnoreCase) ||
                           string.Equals(fd.ReferenceName, toCompare.fd.ReferenceName, StringComparison.OrdinalIgnoreCase);
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return fd.Name.GetHashCode() + fd.ReferenceName.GetHashCode();
        }
    }
}
