using System.Collections.Generic;

namespace TechTalk.JiraRestClient
{
    public class History
    {
        public List<Transition> items;

        public History()
        {
            items = new List<Transition>();
        }

        public string id { get; set; }
        public JiraUser author { get; set; }
        public string created { get; set; }
    }
}