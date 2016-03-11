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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TicketImporter
{
    internal class WorkItemState
    {
        public WorkItemState(WorkItemType wit)
        {
            this.wit = wit;
            selectedInProgressState =
                selectedApprovedState =
                    initialState = wit.NewWorkItem().State;
            closedStates = new List<string> {"Done", "Inactive", "Closed", "Completed", "Rejected", "Removed" };
            witd = wit.Export(true);
            gatherNextStates();
            findDonePath();
        }

        public string SelectedApprovedState
        {
            get { return selectedApprovedState; }
            set { selectedApprovedState = value; }
        }

        public string SelectedInProgressState
        {
            get { return selectedInProgressState; }
            set { selectedInProgressState = value; }
        }

        public List<string> NextStates { get; private set; }

        public string Name
        {
            get { return wit.Name; }
        }

        public List<string> PathToDone { get; private set; }

        public bool TypeMatches(string typeName)
        {
            return (string.Compare(wit.Name, typeName) == 0);
        }

        #region private class members

        private readonly WorkItemType wit;
        private readonly List<string> closedStates;
        private readonly string initialState;
        private string selectedApprovedState;
        private string selectedInProgressState;
        private readonly XmlDocument witd;

        private void gatherNextStates()
        {
            NextStates = new List<string> {initialState};
            var Transitions =
                witd.SelectNodes(string.Format("descendant::TRANSITIONS/TRANSITION[@from='{0}']", initialState));
            foreach (XmlNode transition in Transitions)
            {
                NextStates.Add(transition.Attributes.GetNamedItem("to").Value);
            }
        }

        private void findDonePath()
        {
            PathToDone = new List<string>();
            if (pathToDoneFrom(initialState))
            {
                PathToDone.RemoveAt(0);
            }
            else
            {
                PathToDone.Clear();
            }
        }

        private bool pathToDoneFrom(string searchFrom)
        {
            var foundPath = false;

            PathToDone.Add(searchFrom);
            var possiblePaths =
                witd.SelectNodes(
                    string.Format(
                        "descendant::TRANSITIONS/TRANSITION[@from='{0}' and (@to!='Removed' and @to!='Deleted')]",
                        searchFrom));

            foreach (XmlNode path in possiblePaths)
            {
                var to = path.Attributes.GetNamedItem("to").Value;
                if (closedStates.Contains(to, StringComparer.OrdinalIgnoreCase))
                {
                    PathToDone.Add(to);
                    foundPath = true;
                    break;
                }
            }

            if (foundPath == false)
            {
                foreach (XmlNode path in possiblePaths)
                {
                    var from = path.Attributes.GetNamedItem("to").Value;
                    foundPath = pathToDoneFrom(from);
                    if (foundPath)
                    {
                        break;
                    }
                }
            }

            return foundPath;
        }

        #endregion
    }

    public class TfsStateMap
    {
        public TfsStateMap(TfsProject tfsProject)
        {
            this.tfsProject = tfsProject;
            loadDefaultStates(this.tfsProject);
            var storedStates = SettingsStore.Load(approved_key);
            if (storedStates.Count > 0)
            {
                foreach (var wis in workItemStates)
                {
                    if (storedStates.ContainsKey(wis.Name))
                    {
                        wis.SelectedApprovedState = storedStates[wis.Name];
                    }
                }
            }
        }

        public List<string> WorkItemNames
        {
            get
            {
                var names = new List<string>();
                foreach (var wis in workItemStates)
                {
                    names.Add(wis.Name);
                }
                return names;
            }
        }

        public List<string> GetAvailableNextStatesFor(string workItemName)
        {
            var availableStates = new List<string>();
            findAndExcute(workItemName, wis => { availableStates = wis.NextStates; });
            return availableStates;
        }

        public string GetSelectedApprovedStateFor(string workItemName)
        {
            var state = "";
            findAndExcute(workItemName, wis => { state = wis.SelectedApprovedState; });
            return state;
        }

        public void SelectedApprovedStateFor(string workItemName, string selectedState)
        {
            findAndExcute(workItemName, wis => { wis.SelectedApprovedState = selectedState; });
        }

        public string GetSelectedInProgressStateFor(string workItemName)
        {
            var state = "";
            findAndExcute(workItemName, wis => { state = wis.SelectedInProgressState; });
            return state;
        }

        public void SetSelectedInProgressStateFor(string workItemName, string selectedState)
        {
            findAndExcute(workItemName, wis => { wis.SelectedInProgressState = selectedState; });
        }

        public List<string> GetStateTransistionsToDoneFor(string workItemName)
        {
            var states = new List<string>();
            findAndExcute(workItemName, wis => { states = wis.PathToDone; });
            return states;
        }

        public void Save(IEnumerable<KeyValuePair<string, string>> approvedStates)
        {
            SettingsStore.Save(approved_key, approvedStates.ToDictionary(state => state.Key, state => state.Value));
        }

        public void RestoreDefaults()
        {
            loadDefaultStates(tfsProject); // <===== REVISIT METHOD WHEN EXPOSING ON UI...
            var toStore = workItemStates.ToDictionary(state => state.Name, state => state.SelectedApprovedState);
            SettingsStore.Save(approved_key, toStore);
        }

        #region private class members

        private readonly TfsProject tfsProject;
        private List<WorkItemState> workItemStates;
        private const string approved_key = "ApprovedWorkItems";

        private void loadDefaultStates(TfsProject tfsProject)
        {
            workItemStates = new List<WorkItemState>();
            foreach (WorkItemType wit in tfsProject.WorkItemTypes)
            {
                workItemStates.Add(new WorkItemState(wit));
            }
        }

        private delegate void Action(WorkItemState wis);

        private void findAndExcute(string workItemName, Action actionToPerform)
        {
            foreach (var wis in workItemStates.Where(
                wis => wis.TypeMatches(workItemName)))
            {
                actionToPerform(wis);
                break;
            }
        }

        #endregion
    }
}