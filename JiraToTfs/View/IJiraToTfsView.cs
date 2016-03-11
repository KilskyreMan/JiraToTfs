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
using TicketImporter;
using TicketImporter.Interface;

namespace JiraToTfs.View
{
    public interface IJiraToTfsView
    {
        string TfsProject { set; get; }
        string JiraServer { set; get; }
        string JiraUserName { set; get; }
        string JiraPassword { set; get; }
        string JiraProject { set; get; }
        List<string> TfsTeams { set; }
        string SelectedTfsTeam { set; get; }
        List<string> AreaPaths { set; }
        string SelectedAreaPath { set; get; }
        bool IncludeAttachments { get; set; }
        void ClearMessages();
        void WarnUser(string problem);
        void InformUser(string info);
        bool WarnAboutImpersonation { set; }
        void InformUserOfDetailedProgress(string info);
        void ShowFailedTickets(List<IFailedTicket> failedTickets);
        void ImportStarted();
        void ImportFinished();
        void ShowReport(string path);
        void ShowAdvancedSettings(AdvancedSettings advancedSettings);
        void WaitStart();
        void WaitEnd();
        void TfsDependenciesMissing();
    }
}