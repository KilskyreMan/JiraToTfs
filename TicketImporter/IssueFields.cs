using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace TechTalk.JiraRestClient
{
    public class IssueFields
    {
        public String summary { get; set; }
        public IssueType issuetype { get; set; }
        public String description { get; set; }
        public Priority priority { get; set; }
        public Timetracking timetracking { get; set; }
        public Status status { get; set; }
        public Parent parent { get; set; }

        public JiraUser reporter { get; set; }
        public DateTime updated { get; set; }
        public DateTime created { get; set; }

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

        public IssueFields()
        {
            this.summary = "";
            this.issuetype = new IssueType();
            this.description = "";
            this.customfield_10800 = "";
            this.customfield_10007 = ""; /*new List<Sprint>();*/
            this.customfield_10004 = "";
            this.updated = new DateTime();
            this.created = new DateTime();

            this.status = new Status();
            this.timetracking = new Timetracking();

            this.labels = new List<String>();
            this.comments = new List<Comment>();
            this.issuelinks = new List<IssueLink>();
            this.attachment = new List<Attachment>();
            this.reporter = new JiraUser();
            this.assignee = new JiraUser();
            this.parent = new Parent();
            this.priority = new Priority();
        }
    }
}
