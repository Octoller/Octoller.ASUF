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
using System.ComponentModel;
using System.IO;
using System;

namespace Octoller.ASUF.Kernel.ServiceObjects {
    /// <summary>
    /// Filter class for sorting files by extension group.
    /// </summary>
    public sealed class SortFilter : INotifyPropertyChanged {

        private string[] extension;
        private string rootFolderPatch;
        private int limit;
        private ReasonCreatingFolder reasonCreating
            = ReasonCreatingFolder.None;

        /// <summary>
        /// Stores an array of string representations of file extensions.
        /// </summary>
        public string[] Extension {
            get => extension;
            set {
                extension = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Provides the address of the root directory for sorting files of the specified extension.
        /// </summary>
        public string RootFolderPatch {
            get => rootFolderPatch;
            set {
                if (!Directory.Exists(value)) {
                    throw new ArgumentException("Invalid directory path specified");
                } else {
                    rootFolderPatch = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Reason for creating a new subfolder.
        /// </summary>
        public ReasonCreatingFolder ReasonCreating {
            get => reasonCreating;
            set {
                reasonCreating = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The size above which the new subfolder will be created.
        /// </summary>
        public int Limit {
            get => limit;
            set {
                if (value < 0) {
                    throw new ArgumentException("Invalid limit specified");
                } else {
                    limit = value;
                    OnPropertyChanged();
                }   
            }
        }

        /// <summary>
        /// Default construction.
        /// </summary>
        public SortFilter() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
