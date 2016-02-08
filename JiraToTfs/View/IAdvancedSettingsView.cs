/*================================================================================================================================
Copyright (c) 2015 Ian Montgomery

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:
    
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
================================================================================================================================*/

using System.Collections.Generic;

namespace JiraToTfs.View
{
    public delegate List<string> AllowedValuesForField(string fieldName);

    public delegate void NextStatesFor(string workItemName, out List<string> states, out string defaultState);

    public delegate string MapstoFrom(string priorityToMap);

    public interface IAdvancedSettingsView
    {
        void SetAvailableTicketTypes(IEnumerable<string> availableTypes);
        void ShowCurrentTicketTypeMappings(IEnumerable<KeyValuePair<string, string>> currentMappings);
        IEnumerable<KeyValuePair<string, string>> GetCurrentTicketTypeMappings();
        void DefaultToTicketTypes();
        void JiraNotAvailable();

        void ShowCurrentTfsFieldValues(IEnumerable<KeyValuePair<string, string>> fields,
            AllowedValuesForField allowedValuesForField);

        IEnumerable<KeyValuePair<string, string>> GetCurrentTfsFieldValues();
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