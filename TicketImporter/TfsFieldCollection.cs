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
