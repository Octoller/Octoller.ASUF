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
 * 06.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;
using Octoller.ASUF.Kernel.Extension;
using System.Diagnostics;
using System.IO;
using System;

namespace Octoller.ASUF.Kernel.Processor {

    using static Octoller.ASUF.Kernel.Resource.DefaultExtension;
    using static Octoller.ASUF.Kernel.Resource.DefaultPath;

    /// <summary>
    /// 
    /// </summary>
    public sealed class SettingsBuilder {

        private const ReasonCreatingFolder DEFAULT_REASON
            = ReasonCreatingFolder.OverflowAmount;
        private const int DEFAULT_LIMIT = 50;

        private SettingsWriRead settingsWR;

        /// <summary>
        /// Default construction.
        /// </summary>
        public SettingsBuilder() {
            
            settingsWR = new SettingsWriRead();
        }

        /// <summary>
        /// Reads settings from a file.
        /// </summary>
        /// <returns> SettingsContainer </returns>
        public SettingsContainer GetSettings() {

            try {
                return settingsWR.ReadSettingFile();
            } catch (IOException ex) {
                ////TODO: Можно будет реализовать запись ошибок в лог-документ
                return new SettingsContainer();
            } 
        }

        /// <summary>
        /// Save new settings in a file.
        /// </summary>
        /// <param name="settings"> New settings </param>
        public void SaveSettings(SettingsContainer settings) {
            
            if (settings.Empty()) {
                throw new ArgumentException("Attempting to write an empty settings object.");
            }

            settingsWR.WriteSettingFile(settings);
        }

        /// <summary>
        /// Creates a settings object filled with default values.
        /// </summary>
        /// <returns> New object settings. </returns>
        public SettingsContainer CreateDefaultSettings() {

            string root = FolderHandler
                .CreateDirectoryIfNotFound(Path.Combine(Directory.GetCurrentDirectory(), defoltRootFolder));

            string sorted = FolderHandler.
                CreateDirectoryIfNotFound(Path.Combine(root, sortedRootFolder));

            var arrayFilters = new[] {
                new SortFilter() {
                    Extension = new[] { jpg, jpeg, bmp, png },
                    RootFolderPatch = FolderHandler
                        .CreateDirectoryIfNotFound(Path.Combine(sorted, imageFolder)),
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                },

                new SortFilter() {
                    Extension = new[] { doc, txt, xls, pdf },
                    RootFolderPatch = FolderHandler
                        .CreateDirectoryIfNotFound(Path.Combine(sorted, docFolder)),
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                },

                new SortFilter() {
                    Extension = new[] { gif },
                    RootFolderPatch = FolderHandler
                        .CreateDirectoryIfNotFound(Path.Combine(sorted, gifFolder)),
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                 }
            };

            var settings = new SettingsContainer() {

                WatchedFolder = FolderHandler
                    .CreateDirectoryIfNotFound(Path.Combine(root, defoltTempFolder)),

                FolderNotFilter = FolderHandler
                    .CreateDirectoryIfNotFound(Path.Combine(sorted, otherFolder))
            };

            Array.ForEach(arrayFilters, f => settings.Filters.Add(f));

            return settings;
        }
    }
}
