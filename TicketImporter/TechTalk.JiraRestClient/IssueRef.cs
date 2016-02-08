namespace TechTalk.JiraRestClient
{
    public class IssueRef
    {
        public IssueRef()
        {
            id = "";
            key = "";
        }

        public string id { get; set; }
        public string key { get; set; }
    }
}