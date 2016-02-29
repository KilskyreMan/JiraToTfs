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
    }
}