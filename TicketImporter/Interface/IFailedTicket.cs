using System.Collections.Generic;

namespace TicketImporter.Interface
{
    public interface IFailedTicket
    {
        string Summary { get; }
        string Title { get; }
        string Type { get; }
        Ticket Source { get; }
        List<TfsIssue> Issues { get; }
    }
}