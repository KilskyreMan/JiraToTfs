using System;
using System.Collections.Generic;

namespace TechTalk.JiraRestClient
{
    public class IssueFields
    {
        public IssueFields()
        {
            summary = "";
            issuetype = new IssueType();
            description = "";
            customfield_10800 = "";
            customfield_10007 = "";
            customfield_10004 = "";
            updated = new DateTime();
            created = new DateTime();
            resolutiondate = new DateTime();

            status = new Status();
            timetracking = new Timetracking();

            labels = new List<String>();
            comments = new List<Comment>();
            issuelinks = new List<IssueLink>();
            attachment = new List<Attachment>();
            reporter = new JiraUser();
            assignee = new JiraUser();
            parent = new IssueRef();
            priority = new Priority();
            project = new ParentProject();
        }

        public String summary { get; set; }
        public IssueType issuetype { get; set; }
        public String description { get; set; }
        public Priority priority { get; set; }
        public Timetracking timetracking { get; set; }
        public Status status { get; set; }
        public IssueRef parent { get; set; }
        public ParentProject project { get; set; }
        public JiraUser reporter { get; set; }
        public DateTime updated { get; set; }
        public DateTime created { get; set; }
        public DateTime resolutiondate { get; set; }
        // ("Epic")
        public string customfield_10800 { get; set; }
        // ("Sprint")
        public string customfield_10007 { get; set; }
        // ("Story Points")
        public string customfield_10004 { get; set; }
        public JiraUser assignee { get; set; }
        public List<String> labels { get; set; }
        public List<Comment> comments { get; set; }
        public List<IssueLink> issuelinks { get; set; }
        public List<Attachment> attachment { get; set; }
    }
}