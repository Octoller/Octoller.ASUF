using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octoller.ASUF.SystemLogic.ServiceObjects {
    public sealed class SettingUnit {

        public ReasonCreatingFolder ReasonCreating {
            get; set;
        }

        public int Limit {
            get; set;
        } = 1000;

        public string WatchedFolder {
            get; set;
        }

        public string FolderNotFilter {
            get; set;
        }

        public Filter[] Filter {
            get; set;
        }
    }
}
