using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Timers;
using TfsBuildMonitor.Core.TFSobjects;

namespace TfsBuildMonitor.Core
{
    public class TfsBuildMonitorManager : IDisposable
    {
        public event EventHandler<StatusChangedEventArgs> StatusChanged;
        public event EventHandler<EventArgs> BuildsLoaded;

        private readonly ITfsService _tfsService;
        private readonly Timer _timer;
        private readonly double _pollingInterval;

        public IEnumerable<IGrouping<int, Build>> BuildDefinitions { get; private set; }

        public TfsBuildMonitorManager(ITfsService tfsService)
        {
            _tfsService = tfsService;
            _pollingInterval = double.Parse(ConfigurationManager.AppSettings["polling_interval"]);

            _timer = new Timer(_pollingInterval * 1000);
            _timer.Elapsed += _timer_Elapsed;
        }

        public void Init()
        {
            GetSelectedBuilds();
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GetSelectedBuilds();
        }

        private void GetSelectedBuilds()
        {
            IEnumerable<IGrouping<int, Build>> result = null;
            var task = Task.Run(() => _tfsService.GetBuildDefinitionsAsync(".CI")).ContinueWith(r => result = r.Result);
            task.Wait();

            BuildDefinitions = task.Result;
            OnBuildsLoaded();
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Elapsed -= _timer_Elapsed;
            _timer.Dispose();
        }

        protected virtual void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        protected virtual void OnBuildsLoaded()
        {
            BuildsLoaded?.Invoke(this, EventArgs.Empty);
        }
    }
}
