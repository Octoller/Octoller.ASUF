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
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Octoller.ASUF.Kernel.Processor {
    public sealed class Watcher {
        private static int OPERATION_INTERVAL = 200;
        private FileSystemWatcher fileWatcher;

        private Dictionary<string[], string> sortingFilters =
            new Dictionary<string[], string>();

        private string folderNotFilter = null;
        private string watchedFolder = null;
        private ReasonCreatingFolder reasonCreating;
        private int limit = 10;

        private bool notSet {
            get => string.IsNullOrEmpty(fileWatcher.Path);
        }

        public Watcher(SettingsContainer settingUnit) {
            fileWatcher = new FileSystemWatcher();
            if (!settingUnit.Empty()) {
                foreach (var f in settingUnit.Filter) {
                    sortingFilters.Add(f.Extension, f.MovesFolderPatch);
                }

                reasonCreating = settingUnit.ReasonCreating;
                watchedFolder = settingUnit.WatchedFolder;
                fileWatcher.Path = watchedFolder;
                folderNotFilter = settingUnit.FolderNotFilter;
                fileWatcher.InternalBufferSize = 64;
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
            Thread.Sleep(OPERATION_INTERVAL);
            FileInfo file = new FileInfo(e.FullPath);

            string destination = GetFilterPatch(file.Extension);
            //destination = reasonCreating.Create(destination, limit);

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
