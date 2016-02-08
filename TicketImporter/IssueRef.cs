using System;

namespace TechTalk.JiraRestClient
{
    public class IssueRef
    {
        public string id { get; set; }
        public string key { get; set; }

        public IssueRef()
        {
            this.id = "";
            this.key = "";
        }
    }
}
