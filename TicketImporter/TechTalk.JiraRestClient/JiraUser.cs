namespace TechTalk.JiraRestClient
{
    public class JiraUser
    {
        public JiraUser()
        {
            name = "";
            emailAddress = "";
            displayName = "";
        }

        public string name { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
    }
}