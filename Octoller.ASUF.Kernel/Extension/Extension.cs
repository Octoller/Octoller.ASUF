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

    public static class Extension {

        /// <summary>
        /// Returns the current folder counter depending 
        /// on the specified reason for creating a new folder.
        /// </summary>
        /// <param name="reson"> Reason for creating subfolder. </param>
        /// <param name="rootPath"> Path to root folder. </param>
        /// <returns> Number. </returns>
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
        /// Returns the value by which to increase the counter 
        /// depending on the reason for creating a new subfolder.
        /// </summary>
        /// <param name="reson"> Reason for creating subfolder. </param>
        /// <param name="file"> Save file. </param>
        /// <returns> Returns the value by which to increase the counter. </returns>
        public static double AddCount(this ReasonCreatingFolder reson, FileInfo file) =>
            reson switch {
                ReasonCreatingFolder.OverflowAmount => 1,
                ReasonCreatingFolder.OverflowSize => (Convert.ToDouble(file.Length) / 1024 / 1024),
                ReasonCreatingFolder.None => 0,
                _ => 0
            };

        /// <summary>
        /// Сhecks the settings object for installed data.
        /// </summary>
        /// <param name="unit"> Settings object. </param>
        /// <returns> True if property in settings object not set or settings object is null. </returns>
        public static bool Empty(this SettingsContainer unit) =>
            (unit is null) || ((unit.Filters == null || !unit.Filters.Any())
            || string.IsNullOrEmpty(unit.WatchedFolder)
            || string.IsNullOrEmpty(unit.FolderNotFilter));

        /// <summary>
        /// Сhecks the sort filter for installed data.
        /// </summary>
        /// <param name="unit"> Sort filter </param>
        /// <returns> True if property in sort filter not set or sort filter is null. </returns>
        public static bool Empty(this SortFilter unit) =>
            (unit is null) || unit.Extension == null || !unit.Extension.Any()
            || string.IsNullOrEmpty(unit.RootFolderPatch);

        /// <summary>
        /// Splits the input string into an array of strings and 
        /// puts it in an object SortFilter property Extension.
        /// </summary>
        /// <param name="filter"> Sort filter. </param>
        /// <param name="input"> Input string. </param>
        public static void SplitStringExtension(this SortFilter filter, string input) =>
            filter.Extension = input.Replace(" ", "")
            .Split(new[] { ";", }, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        /// <summary>
        /// Returns a string representation of the Extension property from a SortFilter object.
        /// </summary>
        /// <param name="filter"> Sort filter. </param>
        /// <returns> String representation of the Extension property. </returns>
        public static string ExtensionArrayToString(this SortFilter filter) =>
            String.Join("; ", filter.Extension);
    }
}
