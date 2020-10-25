using Octoller.ASUF.Kernel.Extension;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Octoller.ASUF.DesktopApp.Support {

    public class SettingsContainerWrap : INotifyPropertyChanged {

        private string watchedFolder;
        private string folderNotFilter;

        public ObservableCollection<SortFilter> Filters {
            get; set;
        }

        public string WatchedFolder {
            get => watchedFolder;
            set {
                watchedFolder = value;
                OnPropertyChanged();
            }
        }

        public string FolderNotFilter {
            get => folderNotFilter;
            set {
                folderNotFilter = value;
                OnPropertyChanged();
            }
        }

        public SettingsContainerWrap(SettingsContainer settingsContainer) {
            WatchedFolder = settingsContainer.WatchedFolder;
            FolderNotFilter = settingsContainer.FolderNotFilter;

            Filters = new ObservableCollection<SortFilter>();
            if (!settingsContainer.Empty()) {
                Array.ForEach(settingsContainer.Filter, f => Filters.Add(f));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
