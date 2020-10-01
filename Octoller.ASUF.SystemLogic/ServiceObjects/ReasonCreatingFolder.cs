using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octoller.ASUF.SystemLogic.ServiceObjects {
    public enum ReasonCreatingFolder {
        NewYar, NewMonth, NewDay, NewWeek,
        OverflowSize, OverflowAmount
    }
}
