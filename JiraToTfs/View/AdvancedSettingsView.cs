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
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using JiraToTfs.Presenter;
using TicketImporter;

namespace JiraToTfs.View
{
    public partial class AdvancedSettingsView : Form, IAdvancedSettingsView
    {
        public AdvancedSettingsView(AdvancedSettings advancedSettings)
        {
            InitializeComponent();
            presenter = new AdvancedSettingsPresenter(this, advancedSettings);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            presenter.OnSave();
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnClickRestoreTicketTypeDefaults(object sender, EventArgs e)
        {
            presenter.OnRestoreTicketTypeDefaults();
        }

        private void seeMicrosoftLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://msdn.microsoft.com/en-us/library/vstudio/jj920147.aspx");
        }

        private void restoreStateDefaults_Click(object sender, EventArgs e)
        {
            presenter.OnRestoreWorkItemStates();
        }

        private void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void customPriorityField_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.OnCustomPriorityFieldChanged(customPriorityField.SelectedItem as string);
        }

        private void OnFirstShow(object sender, EventArgs e)
        {
            presenter.OnFirstShow();
        }

        private void restorePriorityDefaults_Click(object sender, EventArgs e)
        {
            presenter.OnRestorePriorityDefaults();
        }

        #region private class members

        private readonly AdvancedSettingsPresenter presenter;
        private int activePriorityColumn;

        #endregion

        #region IAdvancedSettingsView Interface

        private void disableTypeMapping()
        {
            jiraGrid.Visible = false;
            restoreDefaultTypesBtn.Enabled = false;
            noJiraTicketTypes.Visible = true;
            pleaseCheckJira.Visible = true;
        }

        public void JiraNotAvailable()
        {
            disableTypeMapping();
        }

        public void TfsNotAvailable()
        {
            disableTypeMapping();

            tfsFieldGrid.Visible = false;
            noFieldsFound.Visible = true;
            checkFieldSettings.Visible = true;

            defaultUserLabel.Visible = false;
            detailedNoteOnDefaultUser.Visible = false;
            defaultAsigneeLabel.Visible = false;
            defaultAssigneeList.Visible = false;
            defaultCreatorLabel.Visible = false;
            defaultCreatorList.Visible = false;
            priorityPanel.Visible = false;

            workItemGrid.Visible = false;

            noUsersFound.Visible = true;
            checkUserSettings.Visible = true;
        }

        public void SetAvailableTicketTypes(IEnumerable<string> availableTypes)
        {
            maptoColumn.DataSource = new List<string>(availableTypes);
        }

        public void ShowCurrentTicketTypeMappings(IEnumerable<KeyValuePair<string, string>> currentMappings)
        {
            jiraGrid.Rows.Clear();
            foreach (var mapping in currentMappings)
            {
                jiraGrid.Rows.Add(mapping.Key, mapping.Value);
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetCurrentTicketTypeMappings()
        {
            foreach (DataGridViewRow row in jiraGrid.Rows)
            {
                string name = row.Cells[0].EditedFormattedValue.ToString(),
                    Value = (row.Cells[1].EditedFormattedValue != null
                        ? row.Cells[1].EditedFormattedValue.ToString()
                        : "");
                yield return new KeyValuePair<string, string>(name, Value);
            }
        }

        public void ShowDefaultTfsFieldValues(TfsFieldCollection fields)
        {
            tfsFieldGrid.Rows.Clear();
            foreach (string fieldName in fields.EditableFields)
            {
                TfsField field = fields[fieldName];
                var row = new DataGridViewRow();
                row.CreateCells(tfsFieldGrid);
                row.Cells[0] = new DataGridViewTextBoxCell { Value = fieldName };
                if (field.AllowedValues.Count > 0)
                {
                    row.Cells[1] = new DataGridViewComboBoxCell();
                    var cell = (DataGridViewComboBoxCell)row.Cells[1];
                    cell.DataSource = field.AllowedValues;
                }
                else
                {
                    row.Cells[1] = new DataGridViewTextBoxCell();
                }
                row.Cells[1].Value = field.DefaultValue;
                tfsFieldGrid.Rows.Add(row);
            }
        }

        public void GetDefaultTfsFieldValues(TfsFieldCollection fields)
        {
            foreach (DataGridViewRow row in tfsFieldGrid.Rows)
            {
                string name = row.Cells[0].EditedFormattedValue.ToString(),
                       value = (row.Cells[1].EditedFormattedValue != null ? row.Cells[1].EditedFormattedValue.ToString() : "");
                fields[name].DefaultValue = value;
            }
        }

        public void DefaultToTicketTypes()
        {
            tabControl.SelectTab(0);
        }

        public void DefaultToTfsFieldMappings()
        {
            tabControl.SelectTab(1);
        }

        public void SetDefaultAsignees(IEnumerable<string> tfsUsers, string defaultTo)
        {
            foreach (var user in tfsUsers)
            {
                defaultAssigneeList.Items.Add(user);
            }
            defaultAssigneeList.Text = defaultTo;
        }

        public string GetCurrentDefaultAssignee()
        {
            return defaultAssigneeList.Text;
        }

        public void SetDefaultCreators(IEnumerable<string> tfsUsers, string defaultTo)
        {
            foreach (var user in tfsUsers)
            {
                defaultCreatorList.Items.Add(user);
            }
            defaultCreatorList.Text = defaultTo;
        }

        public string GetCurrentDefaultCreator()
        {
            return defaultCreatorList.Text;
        }

        public void DisplayAvailableStates(List<string> workItemNames, NextStatesFor nextStatesFor)
        {
            workItemGrid.Rows.Clear();
            foreach (var workItemName in workItemNames)
            {
                var row = new DataGridViewRow();
                row.CreateCells(workItemGrid);
                row.Cells[0] = new DataGridViewTextBoxCell();
                row.Cells[0].Value = workItemName;

                row.Cells[1] = new DataGridViewComboBoxCell();
                var cell = (DataGridViewComboBoxCell) row.Cells[1];

                List<string> states;
                string defaultState;
                nextStatesFor(workItemName, out states, out defaultState);
                cell.DataSource = states;
                cell.Value = defaultState;

                workItemGrid.Rows.Add(row);
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetCurrentStates()
        {
            return from DataGridViewRow row in workItemGrid.Rows
                select new KeyValuePair<string, string>(row.Cells[0].EditedFormattedValue as string,
                    row.Cells[1].EditedFormattedValue as string);
        }

        public string GetCurrentPriorityField()
        {
            return customPriorityField.Text;
        }

        public void ListAvailablePriorityFields(List<string> fields, string defaultPriorityFieldName)
        {
            customPriorityField.DataSource = fields;
            customPriorityField.SelectedItem = defaultPriorityFieldName;
        }

        public void ListAvailableFromPriorities(List<string> values, MapstoFrom mapTo)
        {
            priorityGrid.Rows.Clear();
            foreach (var value in values)
            {
                priorityGrid.Rows.Add(value, mapTo(value));
            }
        }

        public void ListAvailableToPriorities(List<string> values)
        {
            activePriorityColumn = 1;
            if (values != null && values.Count > 0)
            {
                priorityGrid.Columns[1].Visible = true;
                priorityGrid.Columns[2].Visible = false;
                ((DataGridViewComboBoxColumn) priorityGrid.Columns[1]).DataSource = values;
            }
            else
            {
                activePriorityColumn = 2;
                priorityGrid.Columns[1].Visible = false;
                priorityGrid.Columns[2].Visible = true;
            }
            foreach (DataGridViewRow row in priorityGrid.Rows)
            {
                row.Cells[activePriorityColumn].Value = "";
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetCurrentPriorities()
        {
            return from DataGridViewRow row in priorityGrid.Rows
                select new KeyValuePair<string, string>(row.Cells[0].EditedFormattedValue.ToString(),
                    row.Cells[activePriorityColumn].EditedFormattedValue.ToString());
        }

        #endregion

        private void TellMeMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/KilskyreMan/JiraToTfs/wiki#advanced-settings");
        }
    }
}