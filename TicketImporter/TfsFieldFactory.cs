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
            if (lastRequest != project)
            {
                fields = new TfsFieldCollection();
                var workItemStore = tfs.GetService<WorkItemStore>();

                var wits = workItemStore.Projects[project].WorkItemTypes;
                var required = new HashSet<string>();
                foreach (var failedFields in
                    (from WorkItemType wit in wits select new WorkItem(wit)).Select(workItem => workItem.Validate()))
                {
                    foreach (var failedField in failedFields.Cast<Field>().Where(failedField => failedField.Status != FieldStatus.Valid))
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

            return fields;
        }
    }
}
