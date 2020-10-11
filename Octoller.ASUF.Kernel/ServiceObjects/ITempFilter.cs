/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 11.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public interface ITempFilter {


        public string RootFolderPatch {
            get; set;
        }

        public string LastFolderPatch {
            get; set;
        }

        public double Limit {
            get; set;
        }

        public double Counter {
            get; set;
        }

        public bool isExcess {
            get;
        }

        public ReasonCreatingFolder ReasonCreating {
            get; set;
        }
    }
}
