// Note: JiraRestClient has been modified to work with the JiraToTfs importer.
//       Replacing with an updated version will loose these changes and hence
//       break the import.
//JIRA REST API documentation: https://docs.atlassian.com/jira/REST/latest

using System;
using System.Collections.Generic;
using TrackProgress;

namespace TechTalk.JiraRestClient
{
    public interface IJiraClient<TIssueFields> where TIssueFields : IssueFields, new()
    {
        /// <summary>Returns all issues for the given project</summary>
        IEnumerable<Issue<TIssueFields>> GetIssues(String projectKey);
        /// <summary>Returns all issues of the specified type for the given project</summary>
        IEnumerable<Issue<TIssueFields>> GetIssues(String projectKey, String issueType);
        /// <summary>Enumerates through all issues for the given project</summary>
        IEnumerable<Issue<TIssueFields>> EnumerateIssues(String projectKey);
        /// <summary>Enumerates through all issues of the specified type for the given project</summary>
        IEnumerable<Issue<TIssueFields>> EnumerateIssues(String projectKey, String issueType);

        /// <summary>Returns the issue identified by the given ref</summary>
        Issue<TIssueFields> LoadIssue(String issueRef);
        /// <summary>Returns the issue identified by the given ref</summary>
        Issue<TIssueFields> LoadIssue(IssueRef issueRef);

        /// <summary>Returns all comments for the given issue</summary>
        IEnumerable<Comment> GetComments(IssueRef issue);

        /// <summary>Returns all links for the given issue</summary>
        IEnumerable<IssueLink> GetIssueLinks(IssueRef issue);
        /// <summary>Returns the link between two issues of the given relation</summary>
        IssueLink LoadIssueLink(IssueRef parent, IssueRef child, String relationship);

        /// <summary>Returns all remote links (attached urls) for the given issue</summary>
        IEnumerable<RemoteLink> GetRemoteLinks(IssueRef issue);

        /// <summary>Returns all issue types</summary>
        IEnumerable<IssueType> GetIssueTypes();

        /// <summary>Returns all issue statuses</summary>
        IEnumerable<Status> GetIssueStatuses();

        /// <summary>Returns information about the JIRA server</summary>
        ServerInfo GetServerInfo();

        /// <summary>Progress update when reading / enumerating through large number of Jira entries</summary>
        event PercentComplete OnPercentComplete;

        /// <summary>Returns the change-log for a given issue (currently excludes Transition history)</summary>
        IEnumerable<History> GetChangeLog(IssueRef issue);
    }
}
