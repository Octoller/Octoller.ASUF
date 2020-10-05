/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public sealed class SettingsContainer {

        public ReasonCreatingFolder ReasonCreating {
            get; set;
        } = ReasonCreatingFolder.OverflowAmount;

        public int Limit {
            get; set;
        } = 1000;

        public string WatchedFolder {
            get; set;
        }

        public string FolderNotFilter {
            get; set;
        }

        public SortFilter[] Filter {
            get; set;
        }
    }
}
