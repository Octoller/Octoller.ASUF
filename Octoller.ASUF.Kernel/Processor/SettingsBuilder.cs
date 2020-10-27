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

using Octoller.ASUF.Kernel.Extension;
using Octoller.ASUF.Kernel.ServiceObjects;
using System.IO;

namespace Octoller.ASUF.Kernel.Processor {

    using static Octoller.ASUF.Kernel.Resource.DefaultExtension;
    using static Octoller.ASUF.Kernel.Resource.DefaultPath;

    public sealed class SettingsBuilder {

        private const ReasonCreatingFolder DEFAULT_REASON
            = ReasonCreatingFolder.OverflowAmount;
        private const int DEFAULT_LIMIT = 50;

        private SettingsWriRead settingsWR;
        private SettingsContainer currentSettings;

        public SettingsBuilder() {
            
            settingsWR = new SettingsWriRead();
            currentSettings = new SettingsContainer();
        }

        public SettingsContainer GetSettings() {
            
            if (currentSettings is null) {
                currentSettings = new SettingsContainer();
            }

            if (currentSettings.Empty()) {
                currentSettings = settingsWR.ReadSettingFile();
                if (currentSettings.Empty()) {
                    currentSettings = CreateDefaultSettings();
                    settingsWR.WriteSettingFile(currentSettings);
                }
            }

            return currentSettings;
        }

        public void SaveSettings(SettingsContainer settings) {

            if (!settings.Empty()) {
                settingsWR.WriteSettingFile(settings);
            }
        }

        public void ReloadSettings() =>
            currentSettings = settingsWR.ReadSettingFile();

        public SettingsContainer CreateDefaultSettings() {

            var settings = new SettingsContainer();

            string root = FolderHandler
                .CreateDirectoryIfNotFound(Path.Combine(Directory.GetCurrentDirectory(), defoltRootFolder));

            string temp = FolderHandler
                .CreateDirectoryIfNotFound(Path.Combine(root, defoltTempFolder));

            string sorted = FolderHandler.
                CreateDirectoryIfNotFound(Path.Combine(root, sortedRootFolder));

            /*
            FolderHandler.CreateDirectoryIfNotFound(Path.Combine(sorted, imageFolder));
            FolderHandler.CreateDirectoryIfNotFound(Path.Combine(sorted, docFolder));
            FolderHandler.CreateDirectoryIfNotFound(Path.Combine(sorted, gifFolder));
            */

            settings.Filter = new[] {
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

            settings.WatchedFolder = temp;
            settings.FolderNotFilter = FolderHandler
                .CreateDirectoryIfNotFound(Path.Combine(sorted, otherFolder));

            return settings;
        }
    }
}
