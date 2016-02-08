using System;

namespace TechTalk.JiraRestClient
{
    public class StatusCategory
    {
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }

        public StatusCategory()
        {
            this.self = "";
            this.id = "";
            this.key = "";
            this.name = "";
        }
    }

    public class Status
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public StatusCategory statusCategory { get; set; }

        public Status()
        {
            this.id = "";
            this.name = "";
            this.description = "";
            this.statusCategory = new StatusCategory();
        }
    }
}
