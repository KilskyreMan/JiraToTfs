#region License
/*
    This source makes up part of JiraToTfs, a utility for migrating Jira
    tickets to Microsoft TFS.

    Copyright(C) 2016  Ian Montgomery

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/
#endregion

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