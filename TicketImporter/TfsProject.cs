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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TicketImporter.Interface;
using TrackProgress;

namespace TicketImporter
{
    public class TfsProject : ITicketTarget, IAvailableTicketTypes
    {
        #region private class variables
        private readonly TfsTeamProjectCollection tfs;
        private readonly string project;
        private readonly string serverUri;
        private string assignedTeam;
        private TfsFieldMap tfsFieldMap;
        private TfsStateMap tfsStateMap;
        private TfsPriorityMap tfsPriorityMap;
        private Dictionary<Ticket, WorkItem> newlyImported;
        private Dictionary<string, WorkItem> previouslyImported;
        private string externalReferenceTag;
        private readonly TfsUsers tfsUsers;
        private readonly bool supportsHtml;
        private bool failedAttachments;
        private readonly List<string> areaPaths;
        private string assignedAreaPath;
        private readonly string processTemplateName;
        private readonly ImportSummary importSummary;
        private readonly TfsFieldCollection fields;
        private const int max_Description_length = 32000;
        #endregion

        public TfsProject(string serverUri, string project)
        {
            this.serverUri = serverUri;
            tfs = null;
            this.project = project;
            supportsHtml = false;
            failedAttachments = false;
            importSummary = new ImportSummary();

            if (string.IsNullOrWhiteSpace(serverUri) == false)
            {
                tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(serverUri));
            }

            fields = TfsFieldFactory.GetFieldsFor(tfs, project);
            tfsUsers = new TfsUsers(this);

            if (tfs != null)
            {
                var workItemStore = tfs.GetService<WorkItemStore>();

                TfsField descriptionField = fields["Description"];
                if (descriptionField != null)
                {
                    supportsHtml = descriptionField.SupportsHtml;
                }
                tfsUsers.OnFailedToImpersonate += OnWarn;

                areaPaths = new List<string> { this.project };
                foreach (Node area in workItemStore.Projects[this.project].AreaRootNodes)
                {
                    areaPaths.Add(area.Path);
                    foreach (Node item in area.ChildNodes)
                    {
                        areaPaths.Add(item.Path);
                    }
                }

                processTemplateName = getProcessTemplateName(this.project);
            }
        }
        public static bool DependenciesInPlace
        {
            get
            {
                bool inPlace = true;
                try
                {
                    using (var tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("")))
                    {
                        // Do nothing, TFS (core dependencies) in place...
                    }
                }
                catch (FileNotFoundException)
                {
                    inPlace = false;
                }
                catch
                {
                    // Do nothing. only interested in capturing FileNotFoundException.
                }
                return inPlace;
            }
        }

        public List<string> Teams
        {
            get
            {
                var teams = new List<string>();
                TfsField teamField = fields["Team"];
                if (teamField != null)
                {
                    teams = teamField.AllowedValues;
                }
                return teams;
            }
        }

        public string Project
        {
            get { return project; }
        }

        public TfsTeamProjectCollection Tfs
        {
            get { return tfs; }
        }

        public WorkItemStore Store
        {
            get
            {
                var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));
                return workItemStore.Projects[project].Store;
            }
        }

        public WorkItemTypeCollection WorkItemTypes
        {
            get { return Store.Projects[project].WorkItemTypes; }
        }

        public string ServerUri
        {
            get { return serverUri; }
        }

        public List<string> AreaPaths
        {
            get { return areaPaths; }
        }

        public TfsFieldCollection Fields
        {
            get { return this.fields; }
        }

        public TfsUsers Users
        {
            get { return tfsUsers; }
        }

        public string UsingTemplate { get { return processTemplateName; } }

        private void OnWarn(string failure)
        {
            importSummary.Warnings.Add(failure);
        }

        public void AssignTicketsToTeam(string teamName)
        {
            assignedTeam = teamName;
        }

        public void AssignTicketsToAreaPath(string areaPath)
        {
            assignedAreaPath = areaPath;
        }

        public List<string> AllowedValuesForField(string fieldName)
        {
            TfsField field = fields[fieldName];
            return (field != null? field.AllowedValues : new List<String>());
        }

        #region Progress utility methods

        private void onPercentComplete(int percentComplete)
        {
            if (this.OnPercentComplete != null)
            {
                this.OnPercentComplete(percentComplete);
            }
        }

        private void onDetailedProcessing(string ticket)
        {
            if (OnDetailedProcessing != null)
            {
                OnDetailedProcessing(ticket);
            }
        }

        #endregion

        #region TFS Utility methods

        private string getProcessTemplateName(string project)
        {
            var ics = tfs.GetService<ICommonStructureService>();

            var projectInfo = ics.GetProjectFromName(project);

            if (projectInfo != null)
            {
                string projectName, projectState;
                int templateId;
                ProjectProperty[] projectProperties;

                ics.GetProjectProperties(projectInfo.Uri, out projectName, out projectState, out templateId,
                    out projectProperties);

                return projectProperties.Where(pt => pt.Name == "Process Template").Select(pt => pt.Value).FirstOrDefault();
            }

            return ("(Unable to determine)");
        }

        private void assignToField(WorkItem workItem, string fieldName, object value)
        {
            if (value != null && string.IsNullOrEmpty(value.ToString()) == false)
            {
                TfsField field = fields[fieldName];
                if (field != null && workItem.Fields.Contains(fieldName))
                {
                    workItem.Fields[fieldName].Value = field.ToFieldValue(value);
                }
            }
        }

        private WorkItem toWorkItem(Ticket toImport)
        {
            var tfs_impersonated = tfsUsers.ImpersonateDefaultCreator();
            if (tfsUsers.CanAddTicket(toImport.CreatedBy))
            {
                tfs_impersonated = tfsUsers.Impersonate(toImport.CreatedBy);
            }

            var workItemStore = (WorkItemStore) tfs_impersonated.GetService(typeof (WorkItemStore));
            var workItemTypes = workItemStore.Projects[project].WorkItemTypes;

            var workItemType = workItemTypes[toImport.TicketType];
            var workItem = new WorkItem(workItemType);

            foreach (var fieldName in tfsFieldMap.Fields.EditableFields.
                Where(fieldName => string.IsNullOrEmpty(fields[fieldName].DefaultValue) == false))
            {
                assignToField(workItem, fieldName, fields[fieldName].DefaultValue);
            }

            workItem.Title = toImport.Summary;
            var description = toImport.Description;

            // TFS's limit on HTML / PlainText fields is 32k.
            if (description.Length > max_Description_length)
            {
                var attachment = string.Format("{0} (Description).txt", toImport.ID);
                description = "<p><b>Description stored as Attachment</b></p>";
                description += "<ul><li>Description exceeds 32K.A limit imposed by TFS.</li>";
                description += ("<li>See attachment \"" + attachment + "\"</li></ul>");
            }

            workItem.Description = description;
            assignToField(workItem, "Repro Steps", description);

            assignToField(workItem, "Team", assignedTeam);
            tfsUsers.AssignUser(toImport.AssignedTo, workItem);

            assignToField(workItem, "Story Points", toImport.StoryPoints);
            assignToField(workItem, "Effort", toImport.StoryPoints);
            workItem.AreaPath = (string.IsNullOrWhiteSpace(assignedAreaPath) ? project : assignedAreaPath);
            assignToField(workItem, "External Reference", toImport.ID);
            assignToField(workItem, tfsPriorityMap.PriorityField, tfsPriorityMap[toImport.Priority]);

            if (toImport.HasUrl)
            {
                try
                {
                    var hl = new Hyperlink(toImport.Url)
                    {
                        Comment = string.Format("{0} [{1}]", externalReferenceTag, toImport.ID)
                    };
                    workItem.Links.Add(hl);
                }
                catch
                {
                    /*Do nothing..*/
                }
            }

            var c = new StringBuilder();
            foreach (var comment in toImport.Comments)
            {
                var body = String.Format("<i>{0}</i></br>Created by {1} on the {2}.<br>",
                    comment.Body.Replace(Environment.NewLine, "<br>"),
                    comment.Author.DisplayName,
                    comment.CreatedOn.ToShortDateString());
                if (comment.UpdatedLater)
                {
                    body = String.Format("{0}<br>(Last updated on the {1}).<br>", body,
                        comment.Updated.ToShortDateString());
                }
                c.Append(body);
            }

            if (c.Length > 0)
            {
                c.Append("<br>");
            }
            c.Append(string.Format("<u><b>Additional {0} information</b></u><br>", externalReferenceTag));

            var rows = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Ticket",
                    string.Format("<a href=\"{0}\">{1}</a>", toImport.Url, toImport.ID + " - " + toImport.Summary)),
                new Tuple<string, string>("Created by ", toImport.CreatedBy.DisplayName),
                new Tuple<string, string>("Created on ", toImport.CreatedOn.ToString(CultureInfo.InvariantCulture))
            };
            if (toImport.TicketState == Ticket.State.Done)
            {
                rows.Add(new Tuple<string, string>("Closed on ", toImport.ClosedOn.ToString(CultureInfo.InvariantCulture)));
            }
            if (string.IsNullOrWhiteSpace(toImport.Project) == false)
            {
                rows.Add(new Tuple<string, string>("Belonged To", toImport.Project));
            }

            c.Append("<table style=\"width:100%\">");
            foreach (var row in rows)
            {
                c.Append(string.Format("<tr><td><b>{0}</b></td><td>{1}</td></tr>", row.Item1, row.Item2));
            }
            c.Append("</table>");
            workItem.History = c.ToString();

            return workItem;
        }

        #endregion

        #region ITicketTarget Interface

        public string Target
        {
            get { return "TFS"; }
        }

        public PercentComplete OnPercentComplete { set; get; }

        public DetailedProcessing OnDetailedProcessing { set; get; }

        public bool StartImport(string externalReferenceTag)
        {
            importSummary.Clear();
            importSummary.Start = DateTime.Now;
            importSummary.TargetDetails.Add(string.Format("TFS Server           : {0}", serverUri));
            importSummary.TargetDetails.Add(string.Format("Able to impersonate? : {0}",
                (tfsUsers.CanImpersonate ? "Yes" : "No")));
            importSummary.TargetDetails.Add(string.Format("Selected Project     : {0}", project));
            importSummary.TargetDetails.Add(string.Format("Template in use      : {0}", processTemplateName));
            failedAttachments = false;

            var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));
            var ableToAdd = workItemStore.Projects[project].HasWorkItemWriteRights;

            if (ableToAdd)
            {
                this.externalReferenceTag = externalReferenceTag;
                tfsFieldMap = new TfsFieldMap(Fields);
                tfsStateMap = new TfsStateMap(this);
                tfsPriorityMap = new TfsPriorityMap();
                newlyImported = new Dictionary<Ticket, WorkItem>();
                findPreviouslyImportedTickets();
            }
            else
            {
                importSummary.Errors.Add(
                    string.Format(
                        "You don't have permission to add work-items to project '{0}'. Contact your local TFS Administrator.",
                        project));
            }
            return ableToAdd;
        }

        public bool CheckTicket(Ticket toAdd, out IFailedTicket failure)
        {
            failure = null;
            var okToAdd = true;
            if (previouslyImported.ContainsKey(toAdd.ID) == false)
            {
                var validationErrors = toWorkItem(toAdd).Validate();
                okToAdd = (validationErrors.Count == 0);
                if (okToAdd == false)
                {
                    failure = new TfsFailedValidation(toAdd, validationErrors);
                }
            }
            return okToAdd;
        }

        public bool AddTicket(Ticket toAdd)
        {
            var addedOk = false;
            var ticketSummary = toAdd.ID + " - " + toAdd.Summary;
            onDetailedProcessing(ticketSummary);

            if (previouslyImported.ContainsKey(toAdd.ID) == false)
            {
                var workItem = toWorkItem(toAdd);
                var rejectedAttachments = new List<string>();

                foreach (var attachment in toAdd.Attachments)
                {
                    if (attachment.Downloaded)
                    {
                        var toAttach =
                            new Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment(attachment.Source);
                        workItem.Attachments.Add(toAttach);
                    }
                }

                if (toAdd.Description.Length > max_Description_length)
                {
                    var attachment =
                        Path.Combine(Path.GetTempPath(), string.Format("{0} (Description).txt", toAdd.ID));
                    File.WriteAllText(attachment, toAdd.Description);
                    var toAttach =
                        new Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment(attachment);
                    workItem.Attachments.Add(toAttach);
                }

                var attempts = workItem.AttachedFileCount + 1;
                do
                {
                    try
                    {
                        workItem.Save(SaveFlags.MergeAll);
                        addedOk = true;
                    }
                    catch (FileAttachmentException attachmentException)
                    {

                        var rejectedFile = attachmentException.SourceAttachment.Name;
                        rejectedAttachments.Add(string.Format("{0} ({1})", rejectedFile, attachmentException.Message));

                        workItem.Attachments.Clear();
                        foreach (var attachment in toAdd.Attachments)
                        {
                            if (string.CompareOrdinal(rejectedFile, attachment.FileName) != 0 && attachment.Downloaded)
                            {
                                var toAttach =
                                    new Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment(attachment.Source);
                                workItem.Attachments.Add(toAttach);
                            }
                        }
                        attempts--;
                    }
                    catch (Exception generalException)
                    {
                        importSummary.Errors.Add(string.Format("Failed to add work item '{0}'.", ticketSummary));
                        importSummary.Errors.Add(generalException.Message);
                        break;
                    }
                } while (addedOk == false && attempts > 0);

                if (addedOk)
                {
                    newlyImported[toAdd] = workItem;
                    if (toAdd.Description.Length > max_Description_length)
                    {
                        importSummary.Warnings.Add(
                            string.Format("Description for {0} - \"{1}\" stored as attachment. Exceeded 32k.", workItem.Id, workItem.Title));
                    }
                    if (rejectedAttachments.Count > 0)
                    {
                        importSummary.Warnings.Add(
                            string.Format("Failed to attach the following item(s) to {0} - \"{1}\"", workItem.Id, workItem.Title));
                        foreach (var file in rejectedAttachments)
                        {
                            importSummary.Warnings.Add(string.Format(" - {0}", file));
                        }
                        failedAttachments = true;
                    }
                }
            }
            else
            {
                addedOk = true;
            }

            return addedOk;
        }

        private string generateBatchFile()
        {
            try
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    csvPath = Path.Combine(path, "Imported_WorkItem_Ids.csv"),
                    batchPath = Path.Combine(path, "UndoImport.bat"),
                    exePath = Path.Combine(path, "DeleteWorkItems.exe");

                using (TextWriter csvFile = File.CreateText(csvPath))
                {
                    var firsId = true;
                    foreach (var wit in newlyImported.Values)
                    {
                        csvFile.Write("{0}{1}", (firsId ? "" : ","), wit.Id);
                        firsId = false;
                    }
                }

                using (TextWriter batchFile = File.CreateText(batchPath))
                {
                    var tfsProject = serverUri + "/" + project;
                    batchFile.WriteLine("\"{0}\" /Project={1} /Ids=\"{2}\"", exePath, tfsProject, csvPath);
                }
                return batchPath;
            }
            catch (Exception ex)
            {
                importSummary.Warnings.Add(
                    string.Format("Failed to generate batchfile for removing imported tickets ({0}).", ex.Message));
                return "";
            }
        }

        private WorkItem findWorkItem(string sourceId)
        {
            var workItem = (from ticket in newlyImported
                            where string.CompareOrdinal(ticket.Key.ID, sourceId) == 0 select ticket.Value).FirstOrDefault();
            if (workItem == null)
            {
                previouslyImported.TryGetValue(sourceId, out workItem);
            }
            return workItem;
        }

        private void findPreviouslyImportedTickets()
        {
            previouslyImported = new Dictionary<string, WorkItem>();
            var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));

            var query = string.Format("Select [ID] From WorkItems where [Team Project] = '{0}' AND [Hyperlink Count] > 0", project);
            var queryResults = workItemStore.Query(query);
            if (queryResults.Count > 0)
            {
                var externalRef = externalReferenceTag + " [";
                var progressNotifer = new ProgressNotifier(OnPercentComplete, queryResults.Count);
                foreach (WorkItem workItem in queryResults)
                {
                    var hyperLinks = workItem.Links.OfType<Hyperlink>();
                    foreach (var link in hyperLinks)
                    {
                        if (link.Comment.IndexOf(externalRef, StringComparison.Ordinal) == 0)
                        {
                            var toExtract = (link.Comment.Length - externalRef.Length) - 1;
                            var sourceId = link.Comment.Substring(externalRef.Length, toExtract);
                            onDetailedProcessing(string.Format("Retrieving previously imported tickets ({0}).", sourceId));
                            previouslyImported[sourceId] = workItem;
                            break;
                        }
                        progressNotifer.UpdateProgress();
                    }
                }
            }
        }

        #region UpdateWorkitem utility methods

        private void updateWorkItemState(Ticket source, WorkItem workItem)
        {
            var statesTosave = new List<string>();

            if (source.TicketState != Ticket.State.Done)
            {
                if (source.TicketState == Ticket.State.InProgress)
                {
                    statesTosave.Add(tfsStateMap.GetSelectedInProgressStateFor(workItem.Type.Name));
                }
                else if (source.TicketState == Ticket.State.Todo || source.TicketState == Ticket.State.Unknown)
                {
                    statesTosave.Add(tfsStateMap.GetSelectedApprovedStateFor(workItem.Type.Name));
                }

                if (importSummary.OpenTickets.ContainsKey(source.TicketType) == false)
                {
                    importSummary.OpenTickets.Add(source.TicketType, 1);
                }
                else
                {
                    importSummary.OpenTickets[source.TicketType]++;
                }
            }
            else
            {
                statesTosave = tfsStateMap.GetStateTransistionsToDoneFor(workItem.Type.Name);
            }

            foreach (var stateToSave in statesTosave)
            {
                workItem.State = stateToSave;

                var validationErrors = workItem.Validate();
                if (validationErrors.Count == 0)
                {
                    workItem.Save(SaveFlags.MergeLinks);
                }
                else
                {
                    var waring = string.Format("Failed to set state for Work-item {0} - \"{1}\"", workItem.Id,
                        workItem.Title);
                    importSummary.Warnings.Add(waring);
                    var failure = new TfsFailedValidation(source, validationErrors);
                    importSummary.Warnings.Add(" " + failure.Summary);
                    foreach (var issue in failure.Issues)
                    {
                        var fieldInTrouble = string.Format("  * {0} - {1} (Value: {2})", issue.Name, issue.Problem,
                            issue.Value);
                        importSummary.Warnings.Add(fieldInTrouble);
                        foreach (var info in issue.Info)
                        {
                            importSummary.Warnings.Add("  * " + info);
                        }
                    }
                    break;
                }
            }
        }

        private void updateWorkItem(Ticket source, WorkItem workItem)
        {
            var ticketTitle = workItem.Id + " - " + workItem.Title;
            onDetailedProcessing(ticketTitle);

            if (source.HasParent)
            {
                var parentWorkItem = findWorkItem(source.Parent);
                if (parentWorkItem != null)
                {
                    try
                    {
                        var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));
                        var linkType = workItemStore.WorkItemLinkTypes[CoreLinkTypeReferenceNames.Hierarchy];
                        parentWorkItem.Links.Add(new WorkItemLink(linkType.ForwardEnd, workItem.Id));
                    }
                    catch (Exception linkException)
                    {
                        importSummary.Warnings.Add(string.Format("Failed to update parent link for '{0}'.", ticketTitle));
                        importSummary.Warnings.Add(linkException.Message);
                    }
                }
            }

            if (source.HasLinks)
            {
                var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));
                if (workItemStore.WorkItemLinkTypes.Contains("System.LinkTypes.Related"))
                {
                    var linkType = workItemStore.WorkItemLinkTypes["System.LinkTypes.Related"];
                    var linkTypeEnd = workItemStore.WorkItemLinkTypes.LinkTypeEnds[linkType.ForwardEnd.Name];
                    foreach (var link in source.Links)
                    {
                        var linkedWorkItem = findWorkItem(link.LinkedTo);
                        if (linkedWorkItem != null)
                        {
                            try
                            {
                                var relatedLink = new RelatedLink(linkTypeEnd, linkedWorkItem.Id);
                                relatedLink.Comment = link.LinkName;
                                workItem.Links.Add(relatedLink);
                            }
                            catch (Exception linkException)
                            {
                                if (linkException.Message.Contains("TF237099") == false)
                                {
                                    importSummary.Warnings.Add(string.Format("Failed to update links for '{0}'.",
                                        ticketTitle));
                                    importSummary.Warnings.Add(linkException.Message);
                                }
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(source.Epic) == false)
            {
                var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));
                var feature = findWorkItem(source.Epic);
                if (feature != null)
                {
                    try
                    {
                        var linkType = workItemStore.WorkItemLinkTypes["System.LinkTypes.Hierarchy"];
                        var linkTypeEnd = workItemStore.WorkItemLinkTypes.LinkTypeEnds[linkType.ReverseEnd.Name];
                        var relatedLink = new RelatedLink(linkTypeEnd, feature.Id);
                        relatedLink.Comment = string.Format("A member of Epic '{0} - {1}'.", feature.Id, feature.Title);
                        workItem.Links.Add(relatedLink);
                    }
                    catch (Exception linkException)
                    {
                        if (linkException.Message.Contains("TF237099") == false)
                        {
                            importSummary.Warnings.Add(string.Format("Failed to update epic link for '{0}'.",
                                ticketTitle));
                            importSummary.Warnings.Add(linkException.Message);
                        }
                    }
                }
            }

            if (workItem.IsDirty)
            {
                try
                {
                    workItem.Save(SaveFlags.MergeLinks);
                }
                catch (Exception ex)
                {
                    importSummary.Errors.Add(
                        string.Format("Failed to update work item '{0} - {1}'.", workItem.Id, workItem.Title));
                    importSummary.Errors.Add(ex.Message);
                    return;
                }
            }

            updateWorkItemState(source, workItem);
        }

        #endregion

        public void EndImport()
        {
            var batchFile = "";
            if (newlyImported.Count > 0)
            {
                var progressNotifer = new ProgressNotifier(OnPercentComplete, (newlyImported.Count + 3));

                // Batch save WorkItems to TFS
                progressNotifer.UpdateProgress();
                var workItemStore = (WorkItemStore) tfs.GetService(typeof (WorkItemStore));

                var batch = new WorkItem[newlyImported.Count];
                newlyImported.Values.CopyTo(batch, 0);

                // Unfortunately once created we need to revisit & update TFS ticket state.
                // (Seemingly you cannot create a new TFS ticket with a status of "Closed", "In-progress" etc).
                foreach (var item in newlyImported)
                {
                    updateWorkItem(item.Key, item.Value);
                    progressNotifer.UpdateProgress();
                }
                progressNotifer.UpdateProgress();

                batchFile = generateBatchFile();
                Users.ReleaseImpersonations();
                progressNotifer.UpdateProgress();
            }

            if (tfsUsers.CanImpersonate == false || tfsUsers.FailedImpersonations.Count > 0)
            {
                if (importSummary.Warnings.Count > 0)
                {
                    importSummary.Warnings.Add(Environment.NewLine);
                }
                importSummary.Warnings.Add("Impersonation");
                importSummary.Warnings.Add("-------------");
            }

            if (tfsUsers.CanImpersonate == false)
            {
                importSummary.Warnings.Add(
                    "You don't have impersonation rights enabled. Hence all tickets were created under your name.");
            }
            else if (tfsUsers.FailedImpersonations.Count > 0)
            {
                importSummary.Warnings.Add(string.Format("TFS failed to impersonate the following {0} users.",
                    externalReferenceTag));
                foreach (var failedUser in tfsUsers.FailedImpersonations)
                {
                    importSummary.Warnings.Add(string.Format("* {0}", failedUser.DisplayName));
                }
            }

            if (tfsUsers.NoRightsToAdd.Count > 0)
            {
                if (importSummary.Warnings.Count > 0)
                {
                    importSummary.Warnings.Add(Environment.NewLine);
                }
                importSummary.Warnings.Add("User Rights");
                importSummary.Warnings.Add("-----------");
                importSummary.Warnings.Add(
                    string.Format("The following {0} users do not have rights to add tickets in {1}.",
                        externalReferenceTag, Target));
                importSummary.Warnings.Add(string.Format("Default ticket creator '{0}' used instead.",
                    tfsUsers.CurrentDefaultCreator));
                foreach (var failedUser in tfsUsers.NoRightsToAdd)
                {
                    importSummary.Warnings.Add(string.Format("* {0}", failedUser.DisplayName));
                }
            }

            importSummary.End = DateTime.Now;
            importSummary.Imported = newlyImported.Count;
            importSummary.PreviouslyImported = previouslyImported.Count;
            if (failedAttachments)
            {
                importSummary.Notes.Add(
                    "Default max. attachment size in TFS is 4MB. Contact your TFS Administrator to expand this limit if required.");
            }
            if (string.IsNullOrWhiteSpace(batchFile) == false)
            {
                importSummary.Notes.Add(string.Format("To undo import run the following batch file: '{0}'.", batchFile));
            }
        }

        public bool SupportsHtml
        {
            get { return supportsHtml; }
        }

        public ImportSummary ImportSummary
        {
            get { return importSummary; }
        }

        public IAvailableTicketTypes GetAvailableTicketTypes()
        {
            return this;
        }
        #endregion

        #region ITicketType Interface
        public IEnumerable<string> Types
        {
            get { return (from WorkItemType type in this.WorkItemTypes select type.Name).ToList(); }
        }

        private string firstFound(params string[] possibleValues)
        {
            foreach (var p in possibleValues.Where(p => Types.Any(t => string.Equals(t, p, StringComparison.CurrentCultureIgnoreCase))))
            {
                return p;
            }
            return "Task";
        }
        public bool Contains(string type)
        {
            return Types.Any(t => string.Equals(t, type, StringComparison.CurrentCultureIgnoreCase));
        }

        public string Bug
        {
            get { return firstFound("Bug", "Issue"); }
        }

        public string Decision
        {
            get { return firstFound("Decision"); }
        }

        public string Risk
        {
            get { return firstFound("Risk", "Issue"); }
        }

        public string Story
        {
            get { return firstFound("User Story", "Product Backlog Item"); }
        }

        public string Epic
        {
            get { return firstFound("Epic", "Feature", "Product Backlog Item"); }
        }

        public string TestCase
        {
            get { return firstFound("Test Case"); }
        }

        public string Impediment
        {
            get { return firstFound("Impediment", "Issue"); }
        }
        public string Task
        {
            get { return firstFound("Task"); }
        }
        #endregion
    }
}