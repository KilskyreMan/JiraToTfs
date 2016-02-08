using System.Collections.Generic;

namespace TicketImporter
{
    public class TfsIssue
    {
        #region private class members

        private readonly object value;

        #endregion

        public TfsIssue(string name, string problem, object value, List<string> info)
        {
            Name = name;
            Problem = problem;
            this.value = value;
            Info = new List<string>(info);
        }

        public string Name { get; private set; }
        public string Problem { get; private set; }

        public string Value
        {
            get { return (value != null ? value.ToString() : "<no value>"); }
        }

        public List<string> Info { get; private set; }
    }
}