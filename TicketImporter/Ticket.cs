using System;
using System.Collections.Generic;
using TechTalk.JiraRestClient;

namespace TicketImporter
{
    public class Link
    {
        public string LinkedTo;
        public string LinkName;

        public Link(string LinkedTo, string LinkName)
        {
            this.LinkName = LinkName;
            this.LinkedTo = LinkedTo;
        }

        public Link() : this("", "")
        {
        }
    }

    public class Comment
    {
        public User Author;
        public string Body;
        public DateTime CreatedOn;
        public DateTime Updated;

        public Comment(User author, string body, DateTime createdOn)
        {
            Author = author;
            Body = JiraString.StripNonPrintable(body);
            CreatedOn = createdOn;
            Updated = createdOn;
        }

        public bool UpdatedLater
        {
            get { return (Updated > CreatedOn ? true : false); }
        }
    }

    public class Attachment
    {
        public bool Downloaded;
        public string FileName;
        public string Source;

        public Attachment(string filename, string content)
        {
            FileName = filename;
            Source = content;
            Downloaded = false;
        }
    }

    public class Ticket
    {
        public enum State
        {
            Unknown,
            Todo,
            InProgress,
            Done
        };

        public User AssignedTo;
        public List<Attachment> Attachments;
        public DateTime ClosedOn;
        public List<Comment> Comments;
        public User CreatedBy;
        public DateTime CreatedOn;
        public string Description;
        public string Epic;
        public string ExternalReference;
        public string ID;
        public DateTime LastModified;
        public List<Link> Links;
        public string Parent;
        public string Priority;
        public string Project;
        public string ProjectUrl;
        public int StoryPoints;
        public string Summary;
        public State TicketState;
        public string TicketType;
        public string Url;

        public Ticket()
        {
            ID = "";
            Description = "";
            TicketType = "";
            TicketState = State.Unknown;
            Comments = new List<Comment>();
            ExternalReference = "";
            Project = "";
            Url = "";
            Summary = "";
            Links = new List<Link>();
            CreatedOn = new DateTime();
            LastModified = new DateTime();
            ClosedOn = new DateTime();
            CreatedBy = new User();
            AssignedTo = new User();
            Epic = "";
            Parent = "";
            StoryPoints = 0;
            Attachments = new List<Attachment>();
            Priority = "";
        }

        public bool HasUrl
        {
            get { return (String.IsNullOrWhiteSpace(Url) ? false : true); }
        }

        public bool HasLinks
        {
            get { return (Links.Count > 0 ? true : false); }
        }

        public bool HasParent
        {
            get { return (String.IsNullOrWhiteSpace(Parent) ? false : true); }
        }

        public bool HasAttachments
        {
            get { return (Attachments.Count > 0 ? true : false); }
        }

        public override bool Equals(Object obj)
        {
            var isEqual = false;
            var toCompare = obj as Ticket;
            if (toCompare != null)
            {
                isEqual = (String.Compare(ID, toCompare.ID, true) == 0 ? true : false);
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}