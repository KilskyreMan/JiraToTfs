using TrackProgress;

namespace TicketImporter.Interface
{
    public interface ITicketTarget
    {
        string Target { get; }
        PercentComplete OnPercentComplete { set; get; }
        DetailedProcessing OnDetailedProcessing { set; get; }
        ImportSummary ImportSummary { get; }
        bool SupportsHtml { get; }
        bool StartImport(string externalReferenceTag);
        bool CheckTicket(Ticket toAdd, out IFailedTicket failure);
        bool AddTicket(Ticket toAdd);
        void EndImport();
    }
}