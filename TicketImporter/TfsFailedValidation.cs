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
                if (field.HasAllowedValuesList)
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