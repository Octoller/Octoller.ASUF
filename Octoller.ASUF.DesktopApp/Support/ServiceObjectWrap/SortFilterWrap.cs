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
 * 26.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Octoller.ASUF.DesktopApp.Support {

    public class SortFilterWrap : INotifyPropertyChanged {

        private string[] extension;
        private string rootFolderPatch;
        private ReasonCreatingFolder reasonCreating;
        private int limit;

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

        public bool IsEmpty {
            get => extension == null
                || string.IsNullOrEmpty(RootFolderPatch);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
