using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using MvvmCross.Core.ViewModels;

namespace TfsBuildMonitor.Core.ViewModels
{
    public class BuildMonitorViewModel
        : MvxViewModel
    {
        private TfsBuildMonitorManager _manager;
        public ObservableCollection<BuildDefinitionViewModel> Builds { get; set; } = new ObservableCollection<BuildDefinitionViewModel>();

        public IMvxCommand RefreshCommand { get; set; }
        public BuildMonitorViewModel()
        {
            var url = ConfigurationManager.AppSettings["tfs_url"];
            var user = ConfigurationManager.AppSettings["tfs_user"];
            var password = ConfigurationManager.AppSettings["tfs_password"];

            var credential = new NetworkCredential { UserName = user, Password = password };
            _manager = new TfsBuildMonitorManager(new TfsService(url, credential));
            //_manager.StatusChanged += Manager_StatusChanged;
            _manager.BuildsLoaded += Manager_BuildsLoaded;
            _manager.Init();
            
            RefreshCommand = new MvxCommand(Refresh);
        }

        private void Manager_BuildsLoaded(object sender, EventArgs e)
        {
            Dispatcher.RequestMainThreadAction(() => InitBuilds(_manager));
        }

        private void InitBuilds(TfsBuildMonitorManager manager)
        {
            Builds.Clear();
            foreach (var buildDef in manager.BuildDefinitions)
            {
                var t = buildDef.First();
                Builds.Add(new BuildDefinitionViewModel()
                {
                    Id = t.id,
                    Name = t.definition.name,
                    LastRequested = DateTime.Parse(t.queueTime),
                    LastRequestedBy = t.requestedFor.displayName,
                    UserImage = t.requestedFor.imageUrl,
                    Status = t.result, 
                    LastChange = t.LastChange
                });
            }
            Refresh();
        }

        //private void Manager_StatusChanged(object sender, StatusChangedEventArgs e)
        //{
        //    var build = Builds.First(b => b.Id == e.BuildId);
        //    build.Status = "";
        //    build.LastRequested = DateTime.Now;

        //}

        private void Refresh()
        {
            // reorder

            Builds.Sort();
        }
    }
}