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

using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System;

namespace Octoller.ASUF.Kernel.ServiceObjects {

    /// <summary>
    /// Сontainer class for grouping, saving and loading settings.
    /// </summary>
    public sealed class SettingsContainer : INotifyPropertyChanged {

        private string watchedFolder;
        private string folderNotFilter;
        private SortFilter selectedFilter;

        /// <summary>
        /// Observable filter collection.
        /// </summary>
        public ObservableCollection<SortFilter> Filters {
            get; set;
        } = new ObservableCollection<SortFilter>();

        /// <summary>
        /// Filter currently selected from the collection.
        /// </summary>
        public SortFilter SelectedFilter {
            get => selectedFilter;
            set {
                selectedFilter = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current watched directory.
        /// </summary>
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

        /// <summary>
        /// Directory for extension files which do not match filters.
        /// </summary>
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
