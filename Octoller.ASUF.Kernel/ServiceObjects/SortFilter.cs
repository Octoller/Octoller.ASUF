/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.Extension;
using System.Text.Json.Serialization;

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public sealed class SortFilter {

        public string[] Extension {
            get; set;
        }

        public ReasonCreatingFolder ReasonCreating {
            get; set;
        } = ReasonCreatingFolder.OverflowAmount;

        public string RootFolderPatch {
            get; set;
        }

        public int Limit {
            get; set;
        }

        public SortFilter() { }
    }
}
