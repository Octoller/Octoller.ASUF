/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public sealed class SortFilter {
        public string[] Extension {
            get; set;
        }

        public string MovesFolderPatch {
            get; set;
        }

        public SortFilter(string movesFolderPath, params string[] extension) {
            Extension = extension;
            MovesFolderPatch = movesFolderPath;
        }
    }
}
