/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.Extenson;
using Octoller.ASUF.Kernel.ServiceObjects;
using System.IO;

namespace Octoller.ASUF.Kernel.Processor {

    using static Octoller.ASUF.Kernel.Resource.DefaultExtension;
    using static Octoller.ASUF.Kernel.Resource.DefaultPath;

    public sealed class SettingsBuilder {
        private SettingsWriRead settingsrw;
        private SettingsContainer settingsUnit;

        public SettingsBuilder() {
            settingsrw = new SettingsWriRead();
            settingsUnit = settingsrw.ReadSettingFile();
        }

        public SettingsContainer GetSettings() {
            if (settingsUnit.Empty()) {
                SetSettings(CreateDefaultSettings());
            }
            return settingsUnit;
        }

        public void SetSettings(SettingsContainer settings) {
            if (settings.Empty()) {
                //TODO: Проблема установки дефолтный настроек
                /* 
                 * Решить проблему при которой, если не задан хотя бы один параметр,
                 * устанавливаются настройки по умолчанию, сбрасывая все параметры
                 * В идеале на дефолтные должен ставится только параметр по усолчанию
                 * Думаю решить через методы расширения, которые будут проверять валидность установленного параметра
                 * и при невалидности сбрасывать этот параметр на дефолтные настройки
                 */
                settings = CreateDefaultSettings();
            }
            settingsUnit = settings;
            settingsrw.WriteSettingFile(settingsUnit);
            CheckFoldersByPaths(settingsUnit);
        }

        private void CheckFoldersByPaths(SettingsContainer settingUnit) {
            if (!settingUnit.Empty()) {
                (new DirectoryInfo(settingUnit.WatchedFolder))
                    .CreateDirectoryIfNotFound();
                (new DirectoryInfo(settingUnit.FolderNotFilter))
                    .CreateDirectoryIfNotFound();

                foreach (var f in settingUnit.Filter) {
                    (new DirectoryInfo(f.MovesFolderPatch))
                        .CreateDirectoryIfNotFound();
                }
            }
        }

        private SettingsContainer CreateDefaultSettings() {

            string root = Directory.GetCurrentDirectory() + defoltRootFolder;
            string temp = root + defoltTempFolder;

            SortFilter[] filters = new SortFilter[] {
                new SortFilter(root + imageFolder, jpg, jpeg, bmp, png),
                new SortFilter(root + docFolder, doc, txt, xls, pdf),
                new SortFilter(root + gifFolder, gif)
            };

            return new SettingsContainer() {
                Filter = filters,
                WatchedFolder = temp,
                FolderNotFilter = root + otherFolder,
                ReasonCreating = ReasonCreatingFolder.OverflowAmount
            };
        }
    }
}
