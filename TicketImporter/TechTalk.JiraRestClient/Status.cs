namespace TechTalk.JiraRestClient
{
    public class StatusCategory
    {
        public StatusCategory()
        {
            self = "";
            id = "";
            key = "";
            name = "";
        }

        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
    }

    public class Status
    {
        public Status()
        {
            id = "";
            name = "";
            description = "";
            statusCategory = new StatusCategory();
        }

        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public StatusCategory statusCategory { get; set; }
    }
}