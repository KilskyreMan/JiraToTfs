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