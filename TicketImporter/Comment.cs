using System;

namespace TechTalk.JiraRestClient
{
    public class Comment
    {
        public string id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public JiraUser author { get; set; }
        public JiraUser updateAuthor { get; set; }
        public string body { get; set; }

        public Comment()
        {
            this.id = "";
            this.created = new DateTime();
            this.updated = new DateTime();
            this.author = new JiraUser();
            this.updateAuthor = new JiraUser();
            this.body = "";
        }
    }
}
