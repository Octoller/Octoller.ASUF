using System;
using System.Collections.Generic;
using System.Text;

namespace Octoller.ASUF.Kernel.ServiceObjects {
    public sealed class TempNotFoundFilter : ITempFilter {
        public SortFilter CurrentFilter {
            get; set;
        }
        public string LastFolderPatch {
            get; set;
        }
        public double Counter {
            get; set;
        }

        public bool isExcess => false;
    }
}
