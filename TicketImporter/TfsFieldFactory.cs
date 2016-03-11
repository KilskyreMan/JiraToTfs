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

using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TicketImporter
{
    public sealed class TfsFieldFactory
    {
        #region private class members
        private static string lastRequest = "";
        private static TfsFieldCollection fields;
        #endregion

        public static TfsFieldCollection GetFieldsFor(TfsTeamProjectCollection tfs, string project)
        {
            if (tfs != null)
            {
                if (lastRequest != project)
                {
                    fields = new TfsFieldCollection();
                    var workItemStore = tfs.GetService<WorkItemStore>();

                    var wits = workItemStore.Projects[project].WorkItemTypes;
                    var required = new HashSet<string>();
                    foreach (var failedFields in
                        (from WorkItemType wit in wits select new WorkItem(wit)).Select(workItem => workItem.Validate())
                        )
                    {
                        foreach (
                            var failedField in
                                failedFields.Cast<Field>().Where(failedField => failedField.Status != FieldStatus.Valid)
                            )
                        {
                            required.Add(failedField.Name);
                        }
                    }

                    var fds = workItemStore.Projects[project].Store.FieldDefinitions;
                    foreach (FieldDefinition fd in fds)
                    {
                        fields.Add(new TfsField(fd, required.Contains(fd.Name)));
                    }

                    lastRequest = project;
                }
            }
            else
            {
                fields = new TfsFieldCollection();
            }

            return fields;
        }
    }
}
