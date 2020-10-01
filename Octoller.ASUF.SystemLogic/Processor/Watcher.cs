using Octoller.ASUF.SystemLogic.Processor.Extension;
using Octoller.ASUF.SystemLogic.ServiceObjects;
using Octoller.ASUF.SystemLogic.ServiceObjects.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Octoller.ASUF.SystemLogic.Processor {
    public sealed class Watcher {

        private FileSystemWatcher fileWatcher;

        Dictionary<string[], string> sortingFilters =
            new Dictionary<string[], string>();

        private string folderNotFilter = null;
        private string watchedFolder = null;

        private bool notSet {
            get => string.IsNullOrEmpty(fileWatcher.Path);
        }

        public Watcher(SettingUnit settingUnit) {
            fileWatcher = new FileSystemWatcher();
            if (!settingUnit.Empty()) {
                foreach (var f in settingUnit.Filter) {
                    sortingFilters.Add(f.Extension, f.MovesFolderPatch);
                }

                watchedFolder = settingUnit.WatchedFolder;
                fileWatcher.Path = watchedFolder;
                folderNotFilter = settingUnit.FolderNotFilter;
            }

            fileWatcher.NotifyFilter = NotifyFilters.FileName;
            fileWatcher.Filter = "*.*";
        }

        public void Subscribe() {
            if (notSet) {
                throw new DirectoryNotFoundException("Не установлены пути отслеживания");
            }

            fileWatcher.Created += OnCreate;
        }

        public void StartWatching() =>
            fileWatcher.EnableRaisingEvents = true;

        public void StopWatching() =>
            fileWatcher.EnableRaisingEvents = false;

        private void OnCreate(object source, FileSystemEventArgs e) {
            Thread.Sleep(300);
            FileInfo file = new FileInfo(e.FullPath);

            string destination = GetFilterPatch(file.Extension);

            (new DirectoryInfo(destination))
                .CreateDirectoryIfNotFound();

            MovedFile(file, destination);
        }

        private string GetFilterPatch(string fileExtension) =>
            sortingFilters.GetValuePartKey(fileExtension) ?? folderNotFilter;

        private void MovedFile(FileInfo file, string destination) {
            if (File.Exists(destination + file.Name)) {
                string newFullName = destination + Guid.NewGuid().ToString() + file.Extension;
                File.Move(file.FullName, newFullName);
            } else {
                File.Move(file.FullName, destination + file.Name);
            }
        }
    }
}
