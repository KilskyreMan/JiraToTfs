using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

            switch (fd.FieldType)
            {
                case FieldType.Boolean:
                    bool typedBool;
                    bool.TryParse(DefaultValue, out typedBool);
                    typedValue = typedBool;
                    break;
                case FieldType.DateTime:
                    DateTime typedDateTime;
                    DateTime.TryParse(DefaultValue, out typedDateTime);
                    typedValue = typedDateTime;
                    break;
                case FieldType.Double:
                    Double typedDouble;
                    Double.TryParse(DefaultValue, out typedDouble);
                    typedValue = typedDouble;
                    break;
                case FieldType.Integer:
                    int typedInt;
                    int.TryParse(DefaultValue, out typedInt);
                    typedValue = typedInt;
                    break;
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
