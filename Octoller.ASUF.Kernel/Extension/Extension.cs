/*
 * **************************************************************************************************************************
 *     _    ____  _   _ _____ 
 *    / \  / ___|| | | |  ___|
 *   / _ \ \___ \| | | | |_   
 *  / ___ \ ___) | |_| |  _|  
 * /_/   \_\____/ \___/|_|  
 * 
 * Octoller.ASUF
 * Library
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;
using Octoller.ASUF.Kernel.Processor;
using System.Linq;
using System.IO;
using System;

namespace Octoller.ASUF.Kernel.Extension {

    /// <summary>
    /// 
    /// </summary>
    public static class Extension {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reson"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static double CurrentCount(this ReasonCreatingFolder reson, string rootPath) =>
            reson switch {
                ReasonCreatingFolder.OverflowAmount =>
                    FolderHandler.GetLenghtFolder(rootPath),
                ReasonCreatingFolder.OverflowSize =>
                    FolderHandler.GetSizeFolder(rootPath),
                ReasonCreatingFolder.None => 0.0,
                _ => 0.0
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reson"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static double AddCount(this ReasonCreatingFolder reson, FileInfo file) =>
            reson switch {
                ReasonCreatingFolder.OverflowAmount => 1,
                ReasonCreatingFolder.OverflowSize => (Convert.ToDouble(file.Length) / 1024 / 1024),
                ReasonCreatingFolder.None => 0,
                _ => 0
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool Empty(this SettingsContainer unit) =>
            ((unit.Filters == null || !unit.Filters.Any())
            || string.IsNullOrEmpty(unit.WatchedFolder)
            || string.IsNullOrEmpty(unit.FolderNotFilter));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool Empty(this SortFilter unit) =>
            unit.Extension == null || !unit.Extension.Any()
            || string.IsNullOrEmpty(unit.RootFolderPatch);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        public static void SplitStringExtension(this SortFilter filter, string input) =>
            filter.Extension = input.Replace(" ", "")
            .Split(new[] { ";", }, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string ExtensionArrayToString(this SortFilter filter) =>
            String.Join("; ", filter.Extension);
    }
}
