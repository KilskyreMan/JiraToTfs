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