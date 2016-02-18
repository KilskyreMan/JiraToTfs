using System;
using System.Collections.Generic;

namespace TicketImporter
{
    public class ImportSummary
    {
        public DateTime End;
        public List<string> Errors;
        public int Imported;
        public List<string> Notes;
        public Dictionary<string, long> OpenTickets;
        public int PreviouslyImported;
        public DateTime Start;
        public List<string> TargetDetails;
        public List<string> Warnings;

        public ImportSummary()
        {
            OpenTickets = new Dictionary<string, long>();
            Errors = new List<string>();
            Warnings = new List<string>();
            Notes = new List<string>();
            TargetDetails = new List<string>();
        }

        public void Clear()
        {
            Imported = 0;
            PreviouslyImported = 0;
            Errors.Clear();
            Warnings.Clear();
            Notes.Clear();
            TargetDetails.Clear();
        }
    }
}