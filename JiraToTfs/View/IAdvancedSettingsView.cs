using System.Collections.Generic;
using TicketImporter;

namespace JiraToTfs.View
{
    public delegate void NextStatesFor(string workItemName, out List<string> states, out string defaultState);

    public delegate string MapstoFrom(string priorityToMap);

    public interface IAdvancedSettingsView
    {
        void SetAvailableTicketTypes(IEnumerable<string> availableTypes);
        void ShowCurrentTicketTypeMappings(IEnumerable<KeyValuePair<string, string>> currentMappings);
        IEnumerable<KeyValuePair<string, string>> GetCurrentTicketTypeMappings();
        void DefaultToTicketTypes();
        void JiraNotAvailable();
        void ShowDefaultTfsFieldValues(TfsFieldCollection fields);
        void GetDefaultTfsFieldValues(TfsFieldCollection fields);
        void DefaultToTfsFieldMappings();
        void SetDefaultAsignees(IEnumerable<string> tfsUsers, string defaultTo);
        string GetCurrentDefaultAssignee();
        void SetDefaultCreators(IEnumerable<string> tfsUsers, string defaultTo);
        string GetCurrentDefaultCreator();
        void DisplayAvailableStates(List<string> workItemNames, NextStatesFor nextStates);
        IEnumerable<KeyValuePair<string, string>> GetCurrentStates();
        void ListAvailablePriorityFields(List<string> fields, string defaultPriorityFieldName);
        void ListAvailableToPriorities(List<string> values);
        void ListAvailableFromPriorities(List<string> values, MapstoFrom mapsTo);
        string GetCurrentPriorityField();
        IEnumerable<KeyValuePair<string, string>> GetCurrentPriorities();
        void TfsNotAvailable();
    }
}