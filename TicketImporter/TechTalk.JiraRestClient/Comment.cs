using System;

namespace TechTalk.JiraRestClient
{
    public class Comment
    {
        public Comment()
        {
            id = "";
            created = new DateTime();
            updated = new DateTime();
            author = new JiraUser();
            updateAuthor = new JiraUser();
            body = "";
        }

        public string id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public JiraUser author { get; set; }
        public JiraUser updateAuthor { get; set; }
        public string body { get; set; }
    }
}