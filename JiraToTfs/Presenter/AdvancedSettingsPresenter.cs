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
            var fields = new List<string>(advancedSettings.TfsProject.WorkItemFields);
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
                view.ShowCurrentTfsFieldValues(advancedSettings.TfsFieldMap.Fields, AllowedValuesForField);
                view.SetDefaultAsignees(advancedSettings.TfsProject.TfsUsers.DefaultAsignees,
                    advancedSettings.TfsProject.TfsUsers.CurrentDefaultAsignee);
                view.SetDefaultCreators(advancedSettings.TfsProject.TfsUsers.DefaultCreators,
                    advancedSettings.TfsProject.TfsUsers.CurrentDefaultCreator);
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
            advancedSettings.TfsFieldMap.Save(view.GetCurrentTfsFieldValues());
            advancedSettings.TfsProject.TfsUsers.CurrentDefaultAsignee = view.GetCurrentDefaultAssignee();
            advancedSettings.TfsProject.TfsUsers.CurrentDefaultCreator = view.GetCurrentDefaultCreator();
            advancedSettings.TfsProject.TfsUsers.Save();
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

        public List<string> AllowedValuesForField(string fieldName)
        {
            return advancedSettings.TfsProject.AllowedValuesForField(fieldName);
        }

        public string MapPriorityTo(string priorityToMap)
        {
            return advancedSettings.TfsPriorityMap[priorityToMap];
        }

        #endregion
    }
}