namespace TechTalk.JiraRestClient
{
    public class Attachment
    {
        public Attachment()
        {
            filename = "";
            content = "";
        }

        public string filename { get; set; }
        public string content { get; set; }
    }
}