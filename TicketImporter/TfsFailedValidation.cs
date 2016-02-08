using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TicketImporter.Interface;

namespace TicketImporter
{
    public class TfsFailedValidation : IFailedTicket
    {
        public TfsFailedValidation(Ticket source, ArrayList validationErrors)
        {
            Source = source;
            Issues = new List<TfsIssue>();
            Title = "";
            Type = "";

            foreach (Field field in validationErrors)
            {
                Title = field.WorkItem.Title;
                Type = field.WorkItem.Type.Name;

                var summary =
                    String.Format("{0} '{1}' flagged an error status of '{2}'.",
                        (field.IsRequired ? "Required field" : "Field"),
                        field.Name,
                        field.Status);

                var info = new List<string>();
                if (field.HasAllowedValuesList == true)
                {
                    info.Add("Allowable values: " + toCommaSeparatedString(field.AllowedValues));
                }
                if (field.ProhibitedValues != null && field.ProhibitedValues.Count > 0)
                {
                    info.Add("Prohibited values: " + toCommaSeparatedString(field.ProhibitedValues));
                }

                Issues.Add(new TfsIssue(field.Name, summary, field.Value, info));
            }

            Summary = String.Format("Found {0} issue{1} with ticket '{2}'.",
                Issues.Count,
                (Issues.Count > 1) ? "(s)" : "",
                Source.ID);
        }

        #region private helper methods

        private string toCommaSeparatedString(ValuesCollection collection)
        {
            var commaSeparated = "";
            foreach (string V in collection)
            {
                if (commaSeparated.Length > 0)
                {
                    commaSeparated += (", " + V);
                }
                else
                {
                    commaSeparated += V;
                }
            }
            return commaSeparated;
        }

        #endregion

        #region private class members

        #endregion

        #region IFailedTicket Interface

        public Ticket Source { get; private set; }

        public string Summary { get; private set; }

        public string Title { get; private set; }

        public string Type { get; private set; }

        public List<TfsIssue> Issues { get; private set; }

        #endregion
    }
}