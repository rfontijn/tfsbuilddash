using System;
using System.Net;
using MvvmCross.Core.ViewModels;

namespace TfsBuildMonitor.Core.ViewModels
{
    public class BuildDefinitionViewModel : MvxViewModel, IComparable
    {
        private string _status;
        private string _lastRequestedBy;
        private DateTime _lastRequested;
        private string _name;
        private string _lastChange;
        private string _userImage;

        public int Id { get; set; }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public DateTime LastRequested
        {
            get { return _lastRequested; }
            set { SetProperty(ref _lastRequested, value); }
        }

        public string LastRequestedBy
        {
            get { return _lastRequestedBy; }
            set { SetProperty(ref _lastRequestedBy, value); }
        }

        public string UserImage
        {
            get { return _userImage; }
            set { SetProperty(ref _userImage, value); }
        }

        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public string LastChange
        {
            get { return _lastChange; }
            set { SetProperty(ref _lastChange, value); }
        }

        public int CompareTo(object obj)
        {
            var d = (BuildDefinitionViewModel)obj;
            return d.LastRequested.CompareTo(LastRequested);
        }
    }
}
