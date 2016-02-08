// Note: JiraRestClient has been modified to work with the JiraToTfs importer.
//       Replacing with an updated version will loose these changes and hence
//       break the import.
//JIRA REST API documentation: https://docs.atlassian.com/jira/REST/latest

using System;
using System.Collections.Generic;
using System.Linq;
using TrackProgress;

namespace TechTalk.JiraRestClient
{
    public interface IJiraClient
    {
        /// <summary>Returns all issues for the given project</summary>
        IEnumerable<Issue> GetIssues(String projectKey);

        /// <summary>Returns all issues of the specified type for the given project</summary>
        IEnumerable<Issue> GetIssues(String projectKey, String issueType);

        /// <summary>Enumerates through all issues for the given project</summary>
        IEnumerable<Issue> EnumerateIssues(String projectKey);

        /// <summary>Enumerates through all issues of the specified type for the given project</summary>
        IEnumerable<Issue> EnumerateIssues(String projectKey, String issueType);

        /// <summary>Returns the issue identified by the given ref</summary>
        Issue LoadIssue(String issueRef);

        /// <summary>Returns the issue identified by the given ref</summary>
        Issue LoadIssue(IssueRef issueRef);

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

        /// <summary>Returns all issue priorities (Critical, major minor etc)</summary>
        IEnumerable<Priority> GetIssuePriorities();

        /// <summary>Returns information about the JIRA server</summary>
        ServerInfo GetServerInfo();

        /// <summary>Progress update when reading / enumerating through large number of Jira entries</summary>
        event PercentComplete OnPercentComplete;

        /// <summary>Returns the change-log for a given issue (currently excludes Transition history)</summary>
        IEnumerable<History> GetChangeLog(IssueRef issue);
    }

    public class JiraClient : IJiraClient
    {
        private readonly IJiraClient<IssueFields> client;

        public JiraClient(string baseUrl, string username, string password)
        {
            client = new JiraClient<IssueFields>(baseUrl, username, password);
            client.OnPercentComplete += onPercentComplete;
        }

        public event PercentComplete OnPercentComplete;

        public IEnumerable<Issue> GetIssues(String projectKey)
        {
            return client.GetIssues(projectKey).Select(Issue.From).ToArray();
        }

        public IEnumerable<Issue> GetIssues(String projectKey, String issueType)
        {
            return client.GetIssues(projectKey, issueType).Select(Issue.From).ToArray();
        }

        public IEnumerable<Issue> EnumerateIssues(String projectKey)
        {
            return client.EnumerateIssues(projectKey).Select(Issue.From);
        }

        public IEnumerable<Issue> EnumerateIssues(String projectKey, String issueType)
        {
            return client.EnumerateIssues(projectKey, issueType).Select(Issue.From);
        }

        public Issue LoadIssue(String issueRef)
        {
            return Issue.From(client.LoadIssue(issueRef));
        }

        public Issue LoadIssue(IssueRef issueRef)
        {
            return Issue.From(client.LoadIssue(issueRef));
        }

        public IEnumerable<Comment> GetComments(IssueRef issue)
        {
            return client.GetComments(issue);
        }

        public IEnumerable<IssueLink> GetIssueLinks(IssueRef issue)
        {
            return client.GetIssueLinks(issue);
        }

        public IssueLink LoadIssueLink(IssueRef parent, IssueRef child, string relationship)
        {
            return client.LoadIssueLink(parent, child, relationship);
        }

        public IEnumerable<RemoteLink> GetRemoteLinks(IssueRef issue)
        {
            return client.GetRemoteLinks(issue);
        }

        public IEnumerable<IssueType> GetIssueTypes()
        {
            return client.GetIssueTypes();
        }

        public IEnumerable<Status> GetIssueStatuses()
        {
            return client.GetIssueStatuses();
        }

        public IEnumerable<Priority> GetIssuePriorities()
        {
            return client.GetIssuePriorities();
        }

        public ServerInfo GetServerInfo()
        {
            return client.GetServerInfo();
        }

        public IEnumerable<History> GetChangeLog(IssueRef issue)
        {
            return client.GetChangeLog(issue);
        }

        private void onPercentComplete(int percentComplete)
        {
            if (OnPercentComplete != null)
            {
                OnPercentComplete(percentComplete);
            }
        }
    }

    public class Issue : Issue<IssueFields>
    {
        internal static Issue From(Issue<IssueFields> other)
        {
            if (other == null)
                return null;

            return new Issue
            {
                expand = other.expand,
                id = other.id,
                key = other.key,
                self = other.self,
                fields = other.fields,
                renderedFields = other.renderedFields
            };
        }
    }
}