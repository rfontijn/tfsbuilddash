using System;

namespace TfsBuildMonitor.Core
{
    public class StatusChangedEventArgs : EventArgs
    {
        public int BuildId { get; set; }
        public string NewStatus { get; set; }


        public StatusChangedEventArgs(int buildId, string newStatus)
        {
            BuildId = buildId;
            NewStatus = newStatus;
        }
    }
}