using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechTalk.JiraRestClient
{
    public class History
    {
        public History()
        {
            items = new List<Transition>();
        }

        public string id { get; set; }
        public JiraUser author { get; set; }
        public string created { get; set; }
        public List<Transition> items;
    }
}
