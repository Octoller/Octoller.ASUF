﻿/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 06.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.Extension;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.IO;
using System.Linq;
using System.Text;

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

            if (currentSettings.Empty()) {

                currentSettings = settingsWR.ReadSettingFile();

            }
        }

        public SettingsContainer GetSettings() =>
            currentSettings;

        public void ReloadSettings() =>
            currentSettings = settingsWR.ReadSettingFile();

        public void SaveSettings(SettingsContainer settings) {
            if (!settings.Empty()) {
                settingsWR.WriteSettingFile(settings);
            }
        }

        public SettingsContainer CreateDefaultSettings() {

            var settings = new SettingsContainer();

            string root = FolderHandler
                .CreateDirectoryIfNotFound(Directory.GetCurrentDirectory() + defoltRootFolder);

            string temp = FolderHandler
                .CreateDirectoryIfNotFound(root + defoltTempFolder);

            settings.Filter = new[] {
                new SortFilter() {
                    Extension = new[] { jpg, jpeg, bmp, png },
                    RootFolderPatch = FolderHandler
                        .CreateDirectoryIfNotFound(root + imageFolder),
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                },

                new SortFilter() {
                    Extension = new[] { doc, txt, xls, pdf },
                    RootFolderPatch = FolderHandler
                        .CreateDirectoryIfNotFound(root + docFolder),
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                },

                new SortFilter() {
                    Extension = new[] { gif },
                    RootFolderPatch = FolderHandler
                        .CreateDirectoryIfNotFound(root + gifFolder),
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                 }
            };

            settings.WatchedFolder = temp;
            settings.FolderNotFilter = FolderHandler
                .CreateDirectoryIfNotFound(root + otherFolder);

            return settings;
        }
    }
}
