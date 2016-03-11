using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using JiraToTfs.Properties;
using JiraToTfs.View;
using log4net;
using Microsoft.TeamFoundation.Client;
using TicketImporter;

namespace JiraToTfs.Presenter
{
    public class JiraToTfsPresenter
    {
        public JiraToTfsPresenter(IJiraToTfsView view)
        {
            this.view = view;
            importWorker = new BackgroundWorker { WorkerReportsProgress = true };
            importWorker.DoWork += import_DoWork;
            importWorker.RunWorkerCompleted += import_Done;
            importWorker.ProgressChanged += onProgressChanged;

            tfsConnectWorker = new BackgroundWorker();
            tfsConnectWorker.DoWork += tfsConnect_DoWork;
            tfsConnectWorker.RunWorkerCompleted += tfsConnect_Done;
        }

        public void Initialise()
        {
            view.JiraServer = Settings.Default.JiraServer;
            view.JiraProject = Settings.Default.JiraProject;
            view.JiraUserName = Settings.Default.JiraUserName;
            view.TfsProject = Settings.Default.TfsProject;
            view.IncludeAttachments = true;
            selectTfsProject(Settings.Default.TfsServerUri, Settings.Default.TfsProject);
        }

        public void SettingsHaveChanged()
        {
            validateSettings();
        }

        public void SelectTfsProject()
        {
            var tfsPp = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false, new UICredentialsProvider());
            tfsPp.ShowDialog();
            if (tfsPp.SelectedTeamProjectCollection != null)
            {
                selectTfsProject(tfsPp.SelectedTeamProjectCollection.Uri.AbsoluteUri, tfsPp.SelectedProjects[0].Name);
            }
        }

        public void StartImport()
        {
            if (validateSettings())
            {
                view.ImportStarted();
                importWorker.RunWorkerAsync();
            }
        }

        private void showAdvancedSettings(AdvancedSettings.ShowFirst showFirst)
        {
            try
            {
                var jiraProject = new JiraProject(view.JiraServer, view.JiraProject,
                    view.JiraUserName, view.JiraPassword);
                var advancedSettings = new AdvancedSettings(jiraProject, selectedProject, showFirst);
                view.ShowAdvancedSettings(advancedSettings);
            }
            catch (Exception e)
            {
                view.WarnUser(e.Message);
            }
        }

        public void OnShowAdvancedSettings()
        {
            showAdvancedSettings(AdvancedSettings.ShowFirst.typeMappings);
        }

        public void OnShowCustomFieldMappings()
        {
            showAdvancedSettings(AdvancedSettings.ShowFirst.tfsFieldMappings);
        }

        public void OnShowReport()
        {
            view.ShowReport(importAgent.GenerateReport());
        }

        public void ClosingUp()
        {
            Settings.Default.JiraServer = view.JiraServer;
            Settings.Default.JiraProject = view.JiraProject;
            Settings.Default.JiraUserName = view.JiraUserName;
            Settings.Default.TfsProject = view.TfsProject;
            if (selectedProject != null)
            {
                Settings.Default.TfsServerUri = selectedProject.ServerUri;
            }
            Settings.Default.TfsTeam = view.SelectedTfsTeam;
            Settings.Default.TfsAreaPath = view.SelectedAreaPath;
            Settings.Default.Save();
        }

        #region private class members

        private readonly IJiraToTfsView view;
        private TfsProject selectedProject;
        private readonly BackgroundWorker importWorker;
        private readonly BackgroundWorker tfsConnectWorker;

        private Exception lastThrownException;
        private TicketImportAgent importAgent;

        private static readonly ILog log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        private bool validateSettings()
        {
            var allOk = true;
            view.ClearMessages();

            string jiraServer = view.JiraServer.Trim(),
                jiraUserName = view.JiraUserName.Trim(),
                jiraPassword = view.JiraPassword.Trim(),
                jiraProject = view.JiraProject.Trim(),
                tfsTeam = view.TfsProject.Trim();

            var whatsMissing = "";
            if (jiraServer.Length == 0)
            {
                whatsMissing += "Jira Server";
            }
            if (jiraUserName.Length == 0)
            {
                if (whatsMissing.Length > 0)
                {
                    whatsMissing += ", ";
                }
                whatsMissing += "Username";
            }
            if (jiraProject.Length == 0)
            {
                if (whatsMissing.Length > 0)
                {
                    whatsMissing += ", ";
                }
                whatsMissing += "Project Key";
            }
            if (tfsTeam.Length == 0)
            {
                if (whatsMissing.Length > 0)
                {
                    whatsMissing += ", ";
                }
                whatsMissing += "Team project";
            }

            if (whatsMissing.Length > 0)
            {
                var warnUser = "Please supply " + whatsMissing + ".";
                view.WarnUser(warnUser);
                allOk = false;
            }

            if (whatsMissing.Length == 0)
            {
                view.InformUser("Ready to start import.");
            }
            return allOk;
        }

        private void selectTfsProject(string serverUri, string projectName)
        {
            view.WaitStart();
            tfsConnectWorker.RunWorkerAsync(new Tuple<string, string>(serverUri, projectName));
        }

        private void tfsConnect_DoWork(object sender, DoWorkEventArgs arguments)
        {
            try
            {
                var serverDetails = (arguments.Argument as Tuple<string, string>);
                selectedProject = new TfsProject(serverDetails.Item1, serverDetails.Item2);
                arguments.Result = null;
            }
            catch (Exception e)
            {
                arguments.Result = e;
            }
        }

        private void tfsConnect_Done(object sender, RunWorkerCompletedEventArgs e)
        {
            view.WaitEnd();
            if (e.Result == null)
            {
                if (selectedProject != null)
                {
                    view.TfsProject = selectedProject.Project;
                    view.TfsTeams = selectedProject.Teams;
                    view.AreaPaths = selectedProject.AreaPaths;
                    view.SelectedTfsTeam = Settings.Default.TfsTeam;
                    view.SelectedAreaPath = Settings.Default.TfsAreaPath;
                    view.WarnAboutImpersonation = (selectedProject.Users.CanImpersonate == false);
                    validateSettings();
                }
            }
            else
            {
                var problem = e.Result as Exception;
                log.Error(problem.ToString());
                view.WarnUser(problem.Message);
                if (problem.GetType() == typeof (FileNotFoundException))
                {
                    view.TfsDependenciesMissing();
                }
            }
        }

        #endregion

        #region private background worker methods called when importing Jira tickets.

        private enum UpdateProgress
        {
            overAll,
            detailed
        }

        private void onProgressChanged(Object sender, ProgressChangedEventArgs e)
        {
            var progress = e.UserState as string;
            if (e.ProgressPercentage == (int) UpdateProgress.overAll)
            {
                view.InformUser(progress);
            }
            else if (e.ProgressPercentage == (int) UpdateProgress.detailed)
            {
                view.InformUserOfDetailedProgress(progress);
            }
        }

        private void import_DoWork(object sender, DoWorkEventArgs e)
        {
            lastThrownException = null;
            try
            {
                selectedProject.AssignTicketsToTeam(view.SelectedTfsTeam);
                selectedProject.AssignTicketsToAreaPath(view.SelectedAreaPath);
                var jiraProject = new JiraProject(view.JiraServer, view.JiraProject,
                    view.JiraUserName, view.JiraPassword);
                importAgent = new TicketImportAgent(jiraProject, selectedProject, view.IncludeAttachments);
                importAgent.ReportProgress += (action, percentComplete) =>
                {
                    if (percentComplete > 0)
                    {
                        importWorker.ReportProgress((int) UpdateProgress.overAll,
                            string.Format("{0} ({1}% complete).", action, percentComplete));
                    }
                    else
                    {
                        importWorker.ReportProgress((int) UpdateProgress.overAll, String.Format("{0}.", action));
                    }
                };
                importAgent.OnDetailedProcessing +=
                    detail => { importWorker.ReportProgress((int) UpdateProgress.detailed, detail); };

                importAgent.StartImport();
            }
            catch (Exception ex)
            {
                lastThrownException = ex;
                log.Error(ex.ToString());
            }
        }

        private void import_Done(object sender, RunWorkerCompletedEventArgs e)
        {
            if (lastThrownException == null)
            {
                if (importAgent.TicketsNotImported > 0)
                {
                    view.ShowFailedTickets(importAgent.FailedTickets);
                }
                else
                {
                    var summary = importAgent.ImportSummary;
                    bool errorsFound = (summary.Errors.Count > 0),
                        warningsFound = (summary.Warnings.Count > 0);
                    if (errorsFound || warningsFound)
                    {
                        if (errorsFound)
                        {
                            view.WarnUser("Import complete but with errors.");
                        }
                        if (warningsFound)
                        {
                            if (errorsFound)
                            {
                                view.WarnUser("Import complete but with errors and warnings.");
                            }
                            else
                            {
                                view.InformUser("Import complete but with warnings.");
                            }
                        }
                    }
                    else
                    {
                        view.InformUser("Import complete.");
                    }
                }
            }
            else
            {
                view.WarnUser(lastThrownException.Message);
                log.Error(lastThrownException.ToString());
            }
            view.ImportFinished();
        }

        #endregion
    }
}