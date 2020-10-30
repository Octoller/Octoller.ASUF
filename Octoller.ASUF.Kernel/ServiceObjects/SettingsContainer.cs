/*
 * **************************************************************************************************************************
 *     _    ____  _   _ _____ 
 *    / \  / ___|| | | |  ___|
 *   / _ \ \___ \| | | | |_   
 *  / ___ \ ___) | |_| |  _|  
 * /_/   \_\____/ \___/|_|  
 * 
 * Octoller.ASUF
 * Library
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Octoller.ASUF.Kernel.ServiceObjects {

    public sealed class SettingsContainer : INotifyPropertyChanged {

        private string watchedFolder;
        private string folderNotFilter;
        private SortFilter selectedFilter;

        public ObservableCollection<SortFilter> Filters {
            get; set;
        } = new ObservableCollection<SortFilter>();

        public SortFilter SelectedFilter {
            get => selectedFilter;
            set {
                selectedFilter = value;
                OnPropertyChanged();
            }
        }

        public string WatchedFolder {
            get => watchedFolder;
            set {
                if (!Directory.Exists(value)) {
                    throw new ArgumentException("Invalid directory path specified");
                } else {
                    watchedFolder = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FolderNotFilter {
            get => folderNotFilter;
            set {
                if (!Directory.Exists(value)) {
                    throw new ArgumentException("Invalid directory path specified");
                } else {
                    folderNotFilter = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
