using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octoller.ASUF.SystemLogic.Processor.Extension {
    public static class DirectoryExtension {
        public static void CreateDirectoryIfNotFound(this DirectoryInfo directory) {
            if (directory != null && !directory.Exists) {
                directory.Create();
            }
        }
    }
}
