/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;

namespace Octoller.ASUF.Kernel.Extenson {
    public static class SettingsContainerExtension {
        public static bool Empty(this SettingsContainer unit) =>
            ((unit.Filter == null || unit.Filter.Length == 0)
            || string.IsNullOrEmpty(unit.WatchedFolder)
            || string.IsNullOrEmpty(unit.FolderNotFilter));
    }
}
