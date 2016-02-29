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