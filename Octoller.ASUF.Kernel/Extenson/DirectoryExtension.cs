/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using System.IO;

namespace Octoller.ASUF.Kernel.Extenson {
    public static class DirectoryExtension {
        public static void CreateDirectoryIfNotFound(this DirectoryInfo directory) {
            if (directory != null && !directory.Exists) {
                directory.Create();
            }
        }
    }
}
