using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octoller.ASUF.SystemLogic.Processor.Extension {
    public static class DictionaryExtension {

        public static string GetValuePartKey(this Dictionary<string[], string> dictionary, string partKey) {
            foreach (var c in dictionary) {
                var path = c.Value;
                var arrayExtension = c.Key;
                if (!arrayExtension.Contains(partKey)) {
                    continue;
                } else {
                    return path;
                }
            }
            return null;
        }
    }
}
