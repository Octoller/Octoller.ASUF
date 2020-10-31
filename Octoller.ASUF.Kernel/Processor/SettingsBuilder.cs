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
        private SettingsContainer currentSettings;

        /// <summary>
        /// 
        /// </summary>
        public SettingsBuilder() {
            
            settingsWR = new SettingsWriRead();
            currentSettings = new SettingsContainer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SettingsContainer GetSettings() {
            
            if (currentSettings is null) {
                currentSettings = new SettingsContainer();
            }

            if (currentSettings.Empty()) {

                try {
                    currentSettings = settingsWR.ReadSettingFile();
                } catch (IOException ex) {
                    ////TODO: Можно будет реализовать запись ошибок в лог-документ
                    currentSettings = CreateDefaultSettings();
                    settingsWR.WriteSettingFile(currentSettings);
                }
            }

            return currentSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public void SaveSettings(SettingsContainer settings) {

            if (!settings.Empty()) {
                settingsWR.WriteSettingFile(settings);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SettingsContainer CreateDefaultSettings() {

            var settings = new SettingsContainer();

            string root = FolderHandler
                .CreateDirectoryIfNotFound(Path.Combine(Directory.GetCurrentDirectory(), defoltRootFolder));

            string temp = FolderHandler
                .CreateDirectoryIfNotFound(Path.Combine(root, defoltTempFolder));

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

            Array.ForEach(arrayFilters, f => settings.Filters.Add(f));

            settings.WatchedFolder = temp;
            string tempPath = FolderHandler
                .CreateDirectoryIfNotFound(Path.Combine(sorted, otherFolder));
            Debug.Print("Default: " + tempPath);
            settings.FolderNotFilter = tempPath;


            return settings;
        }
    }
}
