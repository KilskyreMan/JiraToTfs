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

namespace TrackProgress
{
    public class ProgressNotifier
    {
        public ProgressNotifier(PercentComplete clientToNotify, int recordCount)
        {
            percentCompleteClient = clientToNotify;
            this.recordCount = recordCount;
            UpdateProgress();
        }

        public void UpdateProgress()
        {
            if (clientAvailable())
            {
                if (recordCount > 0 && percentComplete < 100)
                {
                    var currentPercent = (currentRecord*100)/recordCount;
                    if (currentPercent > 100)
                    {
                        currentPercent = 100;
                    }
                    if (currentPercent != percentComplete)
                    {
                        percentComplete = currentPercent;
                        notifyClient();
                    }
                    currentRecord++;
                }
                else if (recordCount == 0)
                {
                    percentComplete = 100;
                    notifyClient();
                }
            }
        }

        #region private class members

        private readonly PercentComplete percentCompleteClient;
        private readonly int recordCount;
        private int currentRecord;
        private int percentComplete = -1;

        private bool clientAvailable()
        {
            return (percentCompleteClient != null);
        }

        private void notifyClient()
        {
            if (percentCompleteClient != null)
            {
                percentCompleteClient(percentComplete);
            }
        }

        #endregion
    }
}