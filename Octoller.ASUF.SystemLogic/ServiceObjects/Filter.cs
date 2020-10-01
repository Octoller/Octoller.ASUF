using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octoller.ASUF.SystemLogic.ServiceObjects {
    public sealed class Filter {
        public string[] Extension {
            get; set;
        }

        public string MovesFolderPatch {
            get; set;
        }

        public Filter() : this (" ", " ") {

        }

        public Filter(string movesFolderPath, params string[] extension) {
            Extension = extension;
            MovesFolderPatch = movesFolderPath;
        }
    }
}
