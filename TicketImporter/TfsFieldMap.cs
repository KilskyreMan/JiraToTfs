using System.Linq;

namespace TicketImporter
{
    public class TfsFieldMap
    {
        public TfsFieldMap(TfsFieldCollection fields)
        {
            this.fields = fields;

            if (this.fields != null)
            {
                var storedMap = SettingsStore.Load(key);
                foreach (var fieldName in this.fields.Names)
                {
                    this.fields[fieldName].DefaultValue = storedMap.ContainsKey(fieldName) == false ? "" : storedMap[fieldName];
                }
            }
        }

        public TfsFieldCollection Fields
        {
            get { return fields; }
        }

        public void Save()
        {
            var map = fields.Names.ToDictionary(name => name, name => fields[name].DefaultValue);
            SettingsStore.Save(key, map);
        }

        #region private class members
        private readonly TfsFieldCollection fields;
        private const string key = "TfsFieldValues";
        #endregion
    }
}