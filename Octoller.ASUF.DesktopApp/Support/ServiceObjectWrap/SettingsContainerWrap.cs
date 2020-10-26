/*
 * **************************************************************************************************************************
 *     _    ____  _   _ _____ 
 *    / \  / ___|| | | |  ___|
 *   / _ \ \___ \| | | | |_   
 *  / ___ \ ___) | |_| |  _|  
 * /_/   \_\____/ \___/|_|  
 * 
 * Octoller.ASUF
 * Desctop.WPF
 * 25.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.Extension;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Octoller.ASUF.DesktopApp.Support {

    public class SettingsContainerWrap : INotifyPropertyChanged {

        private string watchedFolder;
        private string folderNotFilter;

        public ObservableCollection<SortFilterWrap> Filters {
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

            Filters = new ObservableCollection<SortFilterWrap>();

            if (!settingsContainer.Empty()) {
                Array.ForEach(settingsContainer.Filter, f => Filters.Add(new SortFilterWrap() { 
                    Extension = f.Extension,
                    RootFolderPatch = f.RootFolderPatch,
                    ReasonCreating = f.ReasonCreating,
                    Limit = f.Limit
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
