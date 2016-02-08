using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechTalk.JiraRestClient
{
    public class Parent
    {
        public string id { get; set; }
        public string key { get; set; }

        public Parent()
        {
            this.id = "";
            this.key = "";
        }
    }
}
