/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Octoller.ASUF.Kernel.Extension {
    public static class Extension {
        public static double CurrentCount(this ReasonCreatingFolder reson, string rootPath) =>
            reson switch {
                ReasonCreatingFolder.OverflowAmount =>
                    FolderHandler.GetLenghtFolder(rootPath),
                ReasonCreatingFolder.OverflowSize =>
                    FolderHandler.GetSizeFolder(rootPath),
                ReasonCreatingFolder.None => 0.0,
                _ => 0.0
            };

        public static double AddCount(this ReasonCreatingFolder reson, FileInfo file) =>
            reson switch {
                ReasonCreatingFolder.OverflowAmount => 1,
                ReasonCreatingFolder.OverflowSize => (Convert.ToDouble(file.Length) / 1024 / 1024),
                ReasonCreatingFolder.None => 0,
                _ => 0
            };

        public static bool Empty(this SettingsContainer unit) =>
            ((unit.Filter == null || unit.Filter.Length == 0)
            || string.IsNullOrEmpty(unit.WatchedFolder)
            || string.IsNullOrEmpty(unit.FolderNotFilter));

        public static void SplitStringExtension(this SortFilter filter, string input) =>
            filter.Extension = input.Replace(" ", "")
            .Split(new[] { ";", }, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        public static string ExtensionArrayToString(this SortFilter filter) =>
            String.Join("; ", filter.Extension);
    }
}
