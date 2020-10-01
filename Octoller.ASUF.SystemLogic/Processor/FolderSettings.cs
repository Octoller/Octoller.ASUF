using Octoller.ASUF.SystemLogic.ServiceObjects;
using Octoller.ASUF.SystemLogic.ServiceObjects.Extension;
using Octoller.ASUF.SystemLogic.Processor.Extension;
using Octoller.ASUF.SystemLogic.Resource;
using System.IO;

using static Octoller.ASUF.SystemLogic.Resource.DefaultExtension;
using static Octoller.ASUF.SystemLogic.Resource.DefaultPath;

namespace Octoller.ASUF.SystemLogic.Processor {
    public sealed class FolderSettings {

        private SettingsRW settingsrw;
        private SettingUnit settingsUnit;

        public FolderSettings() {
            settingsrw = new SettingsRW();
            settingsUnit = settingsrw.ReadSettingFile();
        }

        public SettingUnit GetSettings() {
            if (settingsUnit.Empty()) {
                SetSettings(CreateDefaultSettings());
            }
            return settingsUnit;
        }

        public void SetSettings(SettingUnit settings) {
            if (settings.Empty()) {
                //TODO: Проблема установки дефолтный настроек
                /* 
                 * Решить проблему при которой, если не задан хотя бы один параметр,
                 * устанавливаются настройки по умолчанию, сбрасывая все параметры
                 * В идеале на дефолтные должен ставится только параметр по усолчанию
                 * Думаю решить через методы расширения, которые будут проверять валидность установленного параметра
                 * и при невалидности сбрасывать этот параметр на дефолтные настройки
                 */
                settingsUnit = CreateDefaultSettings();
            }
            settingsrw.WriteSettingFile(settingsUnit);
            CheckFoldersByPaths(settingsUnit);
        }

        private void CheckFoldersByPaths(SettingUnit settingUnit) {
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

        private SettingUnit CreateDefaultSettings() {

            string root = Directory.GetCurrentDirectory() + defoltRootFolder;
            string temp = root + defoltTempFolder;

            Filter[] filters = new Filter[] {
                new Filter(root + imageFolder, jpg, jpeg, bmp, png),
                new Filter(root + docFolder, doc, txt, xls, pdf),
                new Filter(root + gifFolder, gif)
            };

            return new SettingUnit() {
                Filter = filters,
                WatchedFolder = temp,
                FolderNotFilter = root + otherFolder,
                ReasonCreating = ReasonCreatingFolder.NewWeek
            };
        }

    }
}
