/*
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

namespace Octoller.ASUF.Kernel.Processor {
    using static Octoller.ASUF.Kernel.Resource.DefaultExtension;
    using static Octoller.ASUF.Kernel.Resource.DefaultPath;

    public sealed class SettingsBuilder {

        private const ReasonCreatingFolder DEFAULT_REASON
            = ReasonCreatingFolder.OverflowAmount;
        private const int DEFAULT_LIMIT = 50;

        private SettingsWriRead settingsWR;
        private SettingsContainer currentSettings;

        //TODO: Проблема установки дефолтный настроек
        /* 
         * Решить проблему при которой, если не задан хотя бы один параметр,
         * устанавливаются настройки по умолчанию, сбрасывая все параметры
         * В идеале на дефолтные должен ставится только параметр по усолчанию
         * Думаю решить через методы расширения, которые будут проверять валидность установленного параметра
         * и при невалидности сбрасывать этот параметр на дефолтные настройки
         */

        public SettingsBuilder() {
            
            settingsWR = new SettingsWriRead();
            currentSettings = new SettingsContainer();

            if (currentSettings.Empty()) {

                currentSettings = settingsWR.ReadSettingFile();

                if (currentSettings.Empty()) {

                    currentSettings = CreateDefaultSettings(currentSettings);
                    settingsWR.WriteSettingFile(currentSettings);

                }
            }
        }

        public SettingsContainer GetSettings() =>
            currentSettings;

        public void SetSettings(SettingsContainer newSettings) {
            
            if (newSettings.Empty()) {
                throw new ArgumentException("Trying to set empty settings.", nameof(newSettings));
            }

            currentSettings = newSettings;
            settingsWR.WriteSettingFile(currentSettings);
        }

        public SettingsContainer CreateDefaultSettings(SettingsContainer settings) {
            
            string root = Directory.GetCurrentDirectory() + defoltRootFolder;
            string temp = root + defoltTempFolder;

            settings.Filter = new[] {
                new SortFilter() {
                    Extension = new[] { jpg, jpeg, bmp, png },
                    RootFolderPatch = root + imageFolder,
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                },
                new SortFilter() {
                    Extension = new[] { doc, txt, xls, pdf },
                    RootFolderPatch = root + docFolder,
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                },
                new SortFilter() {
                    Extension = new[] { gif },
                    RootFolderPatch = root + gifFolder,
                    ReasonCreating = DEFAULT_REASON,
                    Limit = DEFAULT_LIMIT
                 }
            };

            settings.WatchedFolder = temp;
            settings.FolderNotFilter = root + otherFolder;

            return settings;
        }
    }
}
