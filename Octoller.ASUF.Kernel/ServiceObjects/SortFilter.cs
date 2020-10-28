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

using System.ComponentModel;
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
                rootFolderPatch = value;
                OnPropertyChanged();
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
                limit = value;
                OnPropertyChanged();
            }
        }

        public SortFilter() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
