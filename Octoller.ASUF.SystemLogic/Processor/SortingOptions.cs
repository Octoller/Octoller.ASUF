using Octoller.ASUF.SystemLogic.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octoller.ASUF.SystemLogic.Processor {
    public sealed class SortingOptions {

        private ReasonCreatingFolder reasonCreating;

        public SortingOptions(ReasonCreatingFolder reason) {
            reasonCreating = reason;
        }
    }
}
