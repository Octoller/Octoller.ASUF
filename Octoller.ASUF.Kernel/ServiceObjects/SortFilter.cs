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

    public sealed class SortFilter {

        public string[] Extension {
            get; set;
        }

        public string RootFolderPatch {
            get; set;
        }

        public ReasonCreatingFolder ReasonCreating {
            get; set;
        } = ReasonCreatingFolder.OverflowAmount;

        public int Limit {
            get; set;
        }

        public SortFilter() { }
    }
}
