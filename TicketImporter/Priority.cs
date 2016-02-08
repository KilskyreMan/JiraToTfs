using System;

namespace TechTalk.JiraRestClient
{
    public class Priority
    {
        public string name { get; set; }
        public string id { get; set; }

        public Priority()
        {
            this.name = "";
            this.id = "";
        }
    }
}
