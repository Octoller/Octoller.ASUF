/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 11.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public sealed class TempFilter : ITempFilter {

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
            get => Counter > Limit;
        }

        public ReasonCreatingFolder ReasonCreating {
            get; set;
        }

        public TempFilter(string root, double limit) {

            if (limit <= 0) {
                Limit = 200;
            } else {
                Limit = limit;
            }

            RootFolderPatch = root;
            
        }
    }
}
