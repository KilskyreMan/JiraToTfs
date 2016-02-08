using System.Collections.Generic;

namespace TechTalk.JiraRestClient
{
    internal class ChangeLog
    {
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public List<History> histories { get; set; }
    }
}