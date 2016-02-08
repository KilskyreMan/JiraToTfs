using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechTalk.JiraRestClient
{
    internal class ChangeLogContainer
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public IssueFields fields { get; set; }
        public ChangeLog changelog { get; set; }
    }
}
