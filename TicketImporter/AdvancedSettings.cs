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

namespace TicketImporter
{
    public class AdvancedSettings
    {
        public enum ShowFirst
        {
            typeMappings,
            tfsFieldMappings,
            tfsStateMappings,
            tfsPriorityField
        };

        public AdvancedSettings(JiraProject jiraProject, TfsProject tfsProject, ShowFirst showFirst)
        {
            var jira = jiraProject;
            this.tfsProject = tfsProject;
            jiraTypeMap = new JiraTypeMap(jira, this.tfsProject);
            tfsFieldMap = new TfsFieldMap(this.tfsProject.Fields);
            tfsStateMap = new TfsStateMap(this.tfsProject);
            tfsPriorityMap = new TfsPriorityMap();
            this.showFirst = showFirst;
        }

        public ShowFirst ShowAtStartUp
        {
            get { return showFirst; }
        }

        public bool TfsSettingsAvailable
        {
            get { return (tfsFieldMap.Fields.Count > 0); }
        }

        public bool JiraSettingsAvailable
        {
            get { return (jiraTypeMap.Count > 0); }
        }

        public JiraTypeMap JiraTypeMap
        {
            get { return jiraTypeMap; }
        }

        public TfsFieldMap TfsFieldMap
        {
            get { return tfsFieldMap; }
        }

        public TfsStateMap TfsStateMap
        {
            get { return tfsStateMap; }
        }

        public TfsPriorityMap TfsPriorityMap
        {
            get { return tfsPriorityMap; }
        }

        public TfsProject TfsProject
        {
            get { return tfsProject; }
        }

        #region private class members

        private readonly TfsProject tfsProject;
        private readonly JiraTypeMap jiraTypeMap;
        private readonly TfsFieldMap tfsFieldMap;
        private readonly TfsStateMap tfsStateMap;
        private readonly TfsPriorityMap tfsPriorityMap;
        private readonly ShowFirst showFirst;

        #endregion
    }
}