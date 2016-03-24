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
using System.IO;
using System.Reflection;
using log4net;
using TicketImporter.Interface;
using TrackProgress;

namespace TicketImporter
{
    public delegate void Report(string Action, int percentComplete);

    public delegate void DetailedProcessing(string detail);

    public class TicketImportAgent
    {
        public TicketImportAgent(ITicketSource ticketSource, ITicketTarget ticketTarget, bool includeAttachments)
        {
            this.ticketSource = ticketSource;
            this.ticketTarget = ticketTarget;
            this.ticketTarget.OnPercentComplete += onPercentComplete;
            this.ticketSource.OnPercentComplete += onPercentComplete;
            this.includeAttachments = includeAttachments;
            downloadFolder = "";
            if (this.includeAttachments)
            {
                var directoryName = string.Format("{0}_to_{1}", this.ticketSource.Source, this.ticketTarget.Target);
                downloadFolder = Path.Combine(Path.GetTempPath(), directoryName);
                Directory.CreateDirectory(downloadFolder);
            }
        }

        public long TicketsNotImported
        {
            get { return FailedTickets.Count; }
        }

        public long TicketsImported
        {
            get { return passedTickets.Count; }
        }

        public string TargetName
        {
            get { return ticketTarget.Target; }
        }

        public string SourceName
        {
            get { return ticketSource.Source; }
        }

        public List<IFailedTicket> FailedTickets { get; private set; }

        public ImportSummary ImportSummary
        {
            get { return ticketTarget.ImportSummary; }
        }

        #region Download Heplers

        private void clearDownloadFolder()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(downloadFolder) == false)
                {
                    var fileInfo = new DirectoryInfo(downloadFolder);
                    foreach (var file in fileInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        public void StartImport()
        {
            setCurrentAction("Preparing to import");
            FailedTickets = new List<IFailedTicket>();
            passedTickets = new List<Ticket>();
            var okToImport = ticketTarget.StartImport(ticketSource.Source);
            if (okToImport)
            {
                ticketSource.PreferHtml = ticketTarget.SupportsHtml;

                setCurrentAction(String.Format("Validating {0} tickets against {1}", ticketSource.Source, ticketTarget.Target));

                foreach (var sourceTicket in ticketSource.Tickets(ticketTarget.GetAvailableTicketTypes()))
                {
                    IFailedTicket failedTicket;
                    if (ticketTarget.CheckTicket(sourceTicket, out failedTicket))
                    {
                        passedTickets.Add(sourceTicket);
                    }
                    else
                    {
                        FailedTickets.Add(failedTicket);
                    }
                }

            if (FailedTickets.Count == 0)
            {
                setCurrentAction(String.Format("Creating {0} tickets", ticketTarget.Target));
                var progressNotifer = new ProgressNotifier(onPercentComplete, passedTickets.Count);
                foreach (var passedTicket in passedTickets)
                {
                    if (includeAttachments)
                    {
                        clearDownloadFolder();
                        ticketSource.DownloadAttachments(passedTicket, downloadFolder);
                    }
                    else
                    {
                        passedTicket.Attachments.Clear();
                    }
                    ticketTarget.AddTicket(passedTicket);
                    progressNotifer.UpdateProgress();
                }

                setCurrentAction(String.Format("Updating {0} tickets", ticketTarget.Target));
                ticketTarget.EndImport();

                if (includeAttachments)
                {
                    clearDownloadFolder();
                    Directory.Delete(downloadFolder);
                }

                setCurrentAction("Import complete.");
            }
        }
}

public string GenerateReport()
{
var summary = ImportSummary;
string reportPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
reportName = string.Format("{0}to{1}_Report.txt", SourceName, TargetName),
fullPath = Path.Combine(reportPath, reportName);
using (var file = new StreamWriter(fullPath, false))
{
file.WriteLine("=======");
file.WriteLine("Summary");
file.WriteLine("=====================================================");
file.WriteLine("Started on  : {0}", summary.Start);
file.WriteLine("Finished on : {0}", summary.End);
file.WriteLine("");

var targetDetails = ticketTarget.Target + " Details";
file.WriteLine(targetDetails);
file.WriteLine(new String('-', targetDetails.Length));
foreach (var detail in ticketTarget.ImportSummary.TargetDetails)
{
    file.WriteLine("* {0}", detail);
}
file.WriteLine("");

long totalOpen = 0;
var openBreakDown = "";
if (summary.OpenTickets.Count > 0)
{
    openBreakDown = "Open Tickets:" + Environment.NewLine;
    openBreakDown += "-------------" + Environment.NewLine;
    foreach (var openTicket in summary.OpenTickets)
    {
        openBreakDown += string.Format("{0}s\t : {1}", openTicket.Key, openTicket.Value) +
                         Environment.NewLine;
        totalOpen += openTicket.Value;
    }
    file.WriteLine("");
}

file.WriteLine("Imported:");
file.WriteLine("---------");
file.WriteLine("Open         : {0}", totalOpen);
file.WriteLine("Closed       : {0}", summary.Imported - totalOpen);
if (summary.Errors.Count > 0)
{
    file.WriteLine("Not imported : {0}", summary.Errors.Count);
}
file.WriteLine("           {0}", new string('-', summary.Imported.ToString().Length));
file.WriteLine("Total    : {0}", summary.Imported);
file.WriteLine("");

file.WriteLine(openBreakDown);

if (summary.PreviouslyImported > 0)
{
    file.WriteLine("Previously Imported : {0}", summary.PreviouslyImported);
}

if (summary.Errors.Count > 0)
{
    file.WriteLine("========");
    file.WriteLine("Errors:");
    file.WriteLine("=====================================================");
    file.WriteLine("");
    foreach (var error in summary.Errors)
    {
        file.WriteLine(error);
    }
    file.WriteLine("");
}

if (summary.Warnings.Count > 0)
{
    file.WriteLine("=========");
    file.WriteLine("Warnings:");
    file.WriteLine("=====================================================");
    file.WriteLine("");
    foreach (var warning in summary.Warnings)
    {
        file.WriteLine(warning);
    }
    file.WriteLine("");
}

if (summary.Notes.Count > 0)
{
    file.WriteLine("======");
    file.WriteLine("Notes:");
    file.WriteLine("=====================================================");
    file.WriteLine("");
    foreach (var note in summary.Notes)
    {
        file.WriteLine("* " + note);
    }
    file.WriteLine("");
}
}
return fullPath;
}

#region private members

private TicketImportAgent()
{
}

private readonly ITicketSource ticketSource;
private readonly ITicketTarget ticketTarget;
private string currentAction;
private DetailedProcessing detailedProcessing;
private List<Ticket> passedTickets;
private readonly bool includeAttachments;
private readonly string downloadFolder;

private static readonly ILog log = LogManager.GetLogger
(MethodBase.GetCurrentMethod().DeclaringType);

#endregion

#region Progress & log Helpers

private void updateProgress(string Action, int percentComplete)
{
if (ReportProgress != null)
{
ReportProgress(Action, percentComplete);
}
}

private void onPercentComplete(int percentComplete)
{
updateProgress(currentAction, percentComplete);
}

public Report ReportProgress { set; get; }

public DetailedProcessing OnDetailedProcessing
{
set
{
detailedProcessing = value;
ticketSource.OnDetailedProcessing += value;
ticketTarget.OnDetailedProcessing += value;
}
get { return detailedProcessing; }
}

private void setCurrentAction(string currentAction)
{
this.currentAction = currentAction;
log.Info(currentAction);
}

#endregion
}
}
 