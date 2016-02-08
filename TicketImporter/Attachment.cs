using System;

namespace TechTalk.JiraRestClient
{
    public class Attachment
    {
        public string filename { get; set; }
        public string content { get; set; }

        public Attachment()
        {
            this.filename = "";
            this.content = "";
        }
    }
}
