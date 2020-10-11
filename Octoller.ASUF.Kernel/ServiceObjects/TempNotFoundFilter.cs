/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 11.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public sealed class TempNotFoundFilter : ITempFilter {


        string ITempFilter.RootFolderPatch {
            get; set;
        }

        public string LastFolderPatch {
            get; set;
        }

        double ITempFilter.Limit {
            get; set;
        }

        double ITempFilter.Counter {
            get; set;
        }

        public bool isExcess => false;

        public ReasonCreatingFolder ReasonCreating {
            get; set;
        } = ReasonCreatingFolder.None;
    }
}
