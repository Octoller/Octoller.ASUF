using System.IO;

namespace Octoller.ASUF.SystemLogic.ServiceObjects.Extension {
    public static class SettingUnitExtension {
        public static bool Empty(this SettingUnit unit) => 
            ((unit.Filter == null || unit.Filter.Length < 1)
            || string.IsNullOrEmpty(unit.WatchedFolder)
            || string.IsNullOrEmpty(unit.FolderNotFilter));
    }
}
