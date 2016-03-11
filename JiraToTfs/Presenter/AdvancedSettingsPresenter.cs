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
using JiraToTfs.View;
using TicketImporter;

namespace JiraToTfs.Presenter
{
    public class AdvancedSettingsPresenter
    {
        public AdvancedSettingsPresenter(IAdvancedSettingsView view, AdvancedSettings advancedSettings)
        {
            this.view = view;
            this.advancedSettings = advancedSettings;
        }

        private void updatePriorityFields()
        {
            var fields = new List<string>(advancedSettings.TfsProject.Fields.Names);
            view.ListAvailablePriorityFields(fields, advancedSettings.TfsPriorityMap.PriorityField);
            view.ListAvailableFromPriorities(advancedSettings.TfsPriorityMap.JiraPriorities, MapPriorityTo);
        }

        public void OnFirstShow()
        {
            if (advancedSettings.JiraSettingsAvailable && advancedSettings.TfsSettingsAvailable)
            {
                view.SetAvailableTicketTypes(advancedSettings.JiraTypeMap.AvailableTicketTypes);
                view.ShowCurrentTicketTypeMappings(advancedSettings.JiraTypeMap.Mappings);
            }
            else
            {
                view.JiraNotAvailable();
            }

            if (advancedSettings.TfsSettingsAvailable)
            {
                view.ShowDefaultTfsFieldValues(advancedSettings.TfsFieldMap.Fields);
                view.SetDefaultAsignees(advancedSettings.TfsProject.Users.DefaultAsignees,
                    advancedSettings.TfsProject.Users.CurrentDefaultAsignee);
                view.SetDefaultCreators(advancedSettings.TfsProject.Users.DefaultCreators,
                    advancedSettings.TfsProject.Users.CurrentDefaultCreator);
                view.DisplayAvailableStates(advancedSettings.TfsStateMap.WorkItemNames, NextStatesFor);
                updatePriorityFields();
            }
            else
            {
                view.TfsNotAvailable();
            }

            if (advancedSettings.ShowAtStartUp == AdvancedSettings.ShowFirst.tfsFieldMappings)
            {
                view.DefaultToTfsFieldMappings();
            }
            else
            {
                view.DefaultToTicketTypes();
            }
        }

        public void OnSave()
        {
            advancedSettings.JiraTypeMap.Save(view.GetCurrentTicketTypeMappings());
            view.GetDefaultTfsFieldValues(advancedSettings.TfsFieldMap.Fields);
            advancedSettings.TfsFieldMap.Save();
            advancedSettings.TfsProject.Users.CurrentDefaultAsignee = view.GetCurrentDefaultAssignee();
            advancedSettings.TfsProject.Users.CurrentDefaultCreator = view.GetCurrentDefaultCreator();
            advancedSettings.TfsProject.Users.Save();
            advancedSettings.TfsStateMap.Save(view.GetCurrentStates());
            advancedSettings.TfsPriorityMap.Save(view.GetCurrentPriorityField(), view.GetCurrentPriorities());
        }

        public void OnRestoreTicketTypeDefaults()
        {
            advancedSettings.JiraTypeMap.RestoreDefaults();
            view.ShowCurrentTicketTypeMappings(advancedSettings.JiraTypeMap.Mappings);
        }

        public void OnRestoreWorkItemStates()
        {
            advancedSettings.TfsStateMap.RestoreDefaults();
            view.DisplayAvailableStates(advancedSettings.TfsStateMap.WorkItemNames, NextStatesFor);
        }

        public void OnRestoreBugPriorities()
        {
            advancedSettings.TfsPriorityMap.RestoreDefaults();
            view.ListAvailableFromPriorities(advancedSettings.TfsPriorityMap.JiraPriorities, MapPriorityTo);
        }

        public void OnCustomPriorityFieldChanged(string priorityField)
        {
            view.ListAvailableToPriorities(advancedSettings.TfsProject.AllowedValuesForField(priorityField));
        }

        public void OnRestorePriorityDefaults()
        {
            advancedSettings.TfsPriorityMap.RestoreDefaults();
            OnCustomPriorityFieldChanged(advancedSettings.TfsPriorityMap.PriorityField);
            updatePriorityFields();
        }

        #region private class members

        private readonly IAdvancedSettingsView view;
        private readonly AdvancedSettings advancedSettings;

        #endregion

        #region Delegates used by IAdvancedSettingsView

        public void NextStatesFor(string workItemName, out List<string> states, out string defaultState)
        {
            states = advancedSettings.TfsStateMap.GetAvailableNextStatesFor(workItemName);
            defaultState = advancedSettings.TfsStateMap.GetSelectedApprovedStateFor(workItemName);
        }

        public string MapPriorityTo(string priorityToMap)
        {
            return advancedSettings.TfsPriorityMap[priorityToMap];
        }

        #endregion
    }
}