/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 06.10.2020
 * 
 * ************************************************************************************************************************** 
 */

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public sealed class TempFilter : ITempFilter {

        public SortFilter CurrentFilter {
            get; set;
        }

        public string LastFolderPatch {
            get; set;
        }

        public double Counter {
            get; set;
        }

        public bool isExcess {
            get => Counter > CurrentFilter.Limit;
        }
    }
}
