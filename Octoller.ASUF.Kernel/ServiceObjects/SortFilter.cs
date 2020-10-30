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
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Octoller.ASUF.Kernel.ServiceObjects {

    public sealed class SortFilter : INotifyPropertyChanged {

        private string[] extension;
        private string rootFolderPatch;
        private int limit;
        private ReasonCreatingFolder reasonCreating
            = ReasonCreatingFolder.None;


        public string[] Extension {
            get => extension;
            set {
                extension = value;
                OnPropertyChanged();
            }
        }

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

        public ReasonCreatingFolder ReasonCreating {
            get => reasonCreating;
            set {
                reasonCreating = value;
                OnPropertyChanged();
            }
        }

        public int Limit {
            get => limit;
            set {
                if (value <= 0) {
                    throw new ArgumentException("Invalid limit specified");
                } else {
                    limit = value;
                    OnPropertyChanged();
                }
                    
            }
        }

        public SortFilter() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
