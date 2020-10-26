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

namespace Octoller.ASUF.Kernel.ServiceObjects {

    public sealed class SettingsContainer {

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
