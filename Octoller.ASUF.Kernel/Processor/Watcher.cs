/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 06.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;
using Octoller.ASUF.Kernel.Extension;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Octoller.ASUF.Kernel.Processor {
    public sealed class Watcher {
        
        private const int OPERATION_INTERVAL = 200;

        private string watchedFolder = string.Empty;

        private Dictionary<string[], ITempFilter> filtersLibrary;

        private FileSystemWatcher systemWatcher;
        private FolderHandler folderHandler;

        private ITempFilter folderNotFilter;

        public Watcher(SettingsContainer settings) {

            systemWatcher = new FileSystemWatcher();
            folderHandler = new FolderHandler();

            SetSettings(settings);

            systemWatcher.InternalBufferSize = 63;
            systemWatcher.NotifyFilter = NotifyFilters.FileName;
            systemWatcher.Filter = "*.*";
        }

        public void Subscrible() {

            if (string.IsNullOrEmpty(systemWatcher.Path)) {
                throw new DirectoryNotFoundException("Tracking paths not set");
            }

            systemWatcher.Created += OnCreate;
        }

        public void StartWatching() =>
            systemWatcher.EnableRaisingEvents = true;

        public void StopWatching() =>
            systemWatcher.EnableRaisingEvents = false;

        public void ApplaySettings(SettingsContainer newSettings) {

            StopWatching();
            SetSettings(newSettings);
            StartWatching();
        }

        private void OnCreate(object source, FileSystemEventArgs e) {

            Thread.Sleep(OPERATION_INTERVAL);
            FileInfo file = new FileInfo(e.FullPath);

            ITempFilter destination = GetRequestPatch(file.Extension);
            if (destination.isExcess) {
                destination.LastFolderPatch = folderHandler
                    .GetNewSubFolder(destination.RootFolderPatch);
                destination.Counter = 0;
            }

            FolderHandler.CreateDirectoryIfNotFound(destination.LastFolderPatch);
            destination.Counter += destination.ReasonCreating.AddCount(file);
            FileHandler.MovedFile(file, destination.LastFolderPatch + "\\");
        }

        private ITempFilter GetRequestPatch(string fileExtension) {

            foreach (var c in filtersLibrary) {
                if (c.Key.Contains(fileExtension)) {

                    return c.Value;
                }
            }
            return folderNotFilter;
        }

        private void SetSettings(SettingsContainer settings) {

            filtersLibrary = new Dictionary<string[], ITempFilter>();

            if (settings.Empty()) {
                return;
            }

            foreach (var f in settings.Filter) {
                ////BUG: Ошибка при первом запуске, нужно проверить пути к папкам 
                string lastFolder =
                    FolderHandler.GetLastFolder(f.RootFolderPatch);

                filtersLibrary.Add(f.Extension, new TempFilter(f.RootFolderPatch, f.Limit) {
                    LastFolderPatch = lastFolder,
                    Counter = f.ReasonCreating.CurrentCount(lastFolder),
                    ReasonCreating = f.ReasonCreating
                });
            }

            watchedFolder = settings.WatchedFolder;
            systemWatcher.Path = watchedFolder;

            folderNotFilter = new  TempNotFoundFilter() {
                LastFolderPatch = settings.FolderNotFilter
            };
        }
    }
}
