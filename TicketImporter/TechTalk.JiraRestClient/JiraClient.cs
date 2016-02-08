// Note: JiraRestClient has been modified to work with the JiraToTfs importer.
//       Replacing with an updated version will loose these changes and hence
//       break the import.
//JIRA REST API documentation: https://docs.atlassian.com/jira/REST/latest

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;
using TrackProgress;

namespace TechTalk.JiraRestClient
{
    public class JiraClient<TIssueFields> : IJiraClient<TIssueFields> where TIssueFields : IssueFields, new()
    {
        private readonly RestClient client;
        private readonly JsonDeserializer deserializer;
        private readonly string password;
        private readonly string username;

        public JiraClient(string baseUrl, string username, string password)
        {
            this.username = username;
            this.password = password;
            deserializer = new JsonDeserializer();
            client = new RestClient {BaseUrl = baseUrl + (baseUrl.EndsWith("/") ? "" : "/") + "rest/api/2/"};
        }

        public IEnumerable<Issue<TIssueFields>> GetIssues(String projectKey)
        {
            return EnumerateIssues(projectKey, null).ToArray();
        }

        public IEnumerable<Issue<TIssueFields>> GetIssues(String projectKey, String issueType)
        {
            return EnumerateIssues(projectKey, issueType).ToArray();
        }

        public IEnumerable<Issue<TIssueFields>> EnumerateIssues(String projectKey)
        {
            return EnumerateIssues(projectKey, null);
        }

        public IEnumerable<Issue<TIssueFields>> EnumerateIssues(String projectKey, String issueType)
        {
            try
            {
                return EnumerateIssuesInternal(projectKey, issueType);
            }
            catch (Exception ex)
            {
                Trace.TraceError("EnumerateIssues(projectKey, issueType) error: {0}", ex);
                throw new JiraClientException("Could not load issues", ex);
            }
        }

        public Issue<TIssueFields> LoadIssue(IssueRef issueRef)
        {
            if (String.IsNullOrEmpty(issueRef.id))
                return LoadIssue(issueRef.key);
            return LoadIssue(issueRef.id);
        }

        public Issue<TIssueFields> LoadIssue(String issueRef)
        {
            try
            {
                var path = String.Format("issue/{0}?expand=renderedFields", issueRef);
                var request = CreateRequest(Method.GET, path);

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                var issue = deserializer.Deserialize<Issue<TIssueFields>>(response);
                issue.fields.comments = GetComments(issue).ToList();
                Issue.ExpandLinks(issue);
                return issue;
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetIssue(issueRef) error: {0}", ex);
                throw new JiraClientException("Could not load issue", ex);
            }
        }

        public IEnumerable<Comment> GetComments(IssueRef issue)
        {
            try
            {
                var path = String.Format("issue/{0}/comment", issue.id);
                var request = CreateRequest(Method.GET, path);

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                var data = deserializer.Deserialize<CommentsContainer>(response);
                return data.comments ?? Enumerable.Empty<Comment>();
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetComments(issue) error: {0}", ex);
                throw new JiraClientException("Could not load comments", ex);
            }
        }

        public IEnumerable<IssueLink> GetIssueLinks(IssueRef issue)
        {
            return LoadIssue(issue).fields.issuelinks;
        }

        public IssueLink LoadIssueLink(IssueRef parent, IssueRef child, String relationship)
        {
            try
            {
                var issue = LoadIssue(parent);
                var links = issue.fields.issuelinks
                    .Where(l => l.type.name == relationship)
                    .Where(l => l.inwardIssue.id == parent.id)
                    .Where(l => l.outwardIssue.id == child.id)
                    .ToArray();

                if (links.Length > 1)
                    throw new JiraClientException("Ambiguous issue link");
                return links.SingleOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError("LoadIssueLink(parent, child, relationship) error: {0}", ex);
                throw new JiraClientException("Could not load issue link", ex);
            }
        }

        public IEnumerable<RemoteLink> GetRemoteLinks(IssueRef issue)
        {
            try
            {
                var path = string.Format("issue/{0}/remotelink", issue.id);
                var request = CreateRequest(Method.GET, path);
                request.AddHeader("ContentType", "application/json");

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                return deserializer.Deserialize<List<RemoteLinkResult>>(response)
                    .Select(RemoteLink.Convert).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetRemoteLinks(issue) error: {0}", ex);
                throw new JiraClientException("Could not load external links for issue", ex);
            }
        }

        public IEnumerable<IssueType> GetIssueTypes()
        {
            try
            {
                var request = CreateRequest(Method.GET, "issuetype");
                request.AddHeader("ContentType", "application/json");

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                var data = deserializer.Deserialize<List<IssueType>>(response);
                return data;
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetIssueTypes() error: {0}", ex);
                throw new JiraClientException("Could not load issue types", ex);
            }
        }

        public IEnumerable<Status> GetIssueStatuses()
        {
            try
            {
                var request = CreateRequest(Method.GET, "status");
                request.AddHeader("ContentType", "application/json");

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                var data = deserializer.Deserialize<List<Status>>(response);
                return data;
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetIssueStatuses() error: {0}", ex);
                throw new JiraClientException("Could not load issue statuses", ex);
            }
        }

        public IEnumerable<Priority> GetIssuePriorities()
        {
            try
            {
                var request = CreateRequest(Method.GET, "priority");
                request.AddHeader("ContentType", "application/json");

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                var data = deserializer.Deserialize<List<Priority>>(response);
                return data;
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetIssuePriorities() error: {0}", ex);
                throw new JiraClientException("Could not load issue priorities", ex);
            }
        }

        public ServerInfo GetServerInfo()
        {
            try
            {
                var request = CreateRequest(Method.GET, "serverInfo");
                request.AddHeader("ContentType", "application/json");

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                return deserializer.Deserialize<ServerInfo>(response);
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetServerInfo() error: {0}", ex);
                throw new JiraClientException("Could not retrieve server information", ex);
            }
        }

        public event PercentComplete OnPercentComplete;

        public IEnumerable<History> GetChangeLog(IssueRef issue)
        {
            try
            {
                var path = String.Format("issue/{0}?expand=changelog&fields=summary", issue.id);
                var request = CreateRequest(Method.GET, path);

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                var data = deserializer.Deserialize<ChangeLogContainer>(response);
                return data.changelog.histories ?? Enumerable.Empty<History>();
            }
            catch (Exception ex)
            {
                Trace.TraceError("GetChangeLog(IssueRef issue) error: {0}", ex);
                throw new JiraClientException("Could not load change-log", ex);
            }
        }

        private RestRequest CreateRequest(Method method, String path)
        {
            var request = new RestRequest {Method = method, Resource = path, RequestFormat = DataFormat.Json};
            request.AddHeader("Authorization",
                "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", username, password))));
            return request;
        }

        private void AssertStatus(IRestResponse response, HttpStatusCode status)
        {
            if (response.ErrorException != null)
                throw new JiraClientException("Transport level error: " + response.ErrorMessage, response.ErrorException);
            if (response.StatusCode != status)
                throw new JiraClientException("JIRA returned status: " + response.StatusDescription, response.Content);
        }

        private IEnumerable<Issue<TIssueFields>> EnumerateIssuesInternal(String projectKey, String issueType)
        {
            var queryCount = 50;
            var resultCount = 0;
            onPercentComplete(0);

            while (true)
            {
                var jql = String.Format("project={0}", Uri.EscapeUriString(projectKey));
                if (!String.IsNullOrEmpty(issueType))
                    jql += String.Format("+AND+issueType={0}", Uri.EscapeUriString(issueType));
                var path = String.Format("search?jql={0}&startAt={1}&maxResults={2}&fields=issueKey", jql, resultCount,
                    queryCount);
                var request = CreateRequest(Method.GET, path);

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.OK);

                var data = deserializer.Deserialize<IssueContainer<TIssueFields>>(response);
                var issues = data.issues ?? Enumerable.Empty<Issue<TIssueFields>>();

                foreach (var item in issues) yield return item;
                resultCount += issues.Count();

                var currentPercent = (resultCount*100)/data.total;
                if (currentPercent > 100)
                {
                    currentPercent = 100;
                }
                onPercentComplete(currentPercent);

                if (resultCount < data.total) continue;
                break;
            }
        }

        public void DeleteIssueLink(IssueLink link)
        {
            try
            {
                var path = String.Format("issueLink/{0}", link.id);
                var request = CreateRequest(Method.DELETE, path);

                var response = client.Execute(request);
                AssertStatus(response, HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Trace.TraceError("DeleteIssueLink(link) error: {0}", ex);
                throw new JiraClientException("Could not delete issue link", ex);
            }
        }

        private void onPercentComplete(int percentComplete)
        {
            if (OnPercentComplete != null)
            {
                OnPercentComplete(percentComplete);
            }
        }
    }
}