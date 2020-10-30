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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System;

namespace Octoller.ASUF.Kernel.Processor {

    public sealed class Watcher {
        
        private const int OPERATION_INTERVAL = 200;

        private string watchedFolder = string.Empty;
        private Dictionary<string[], TempFilter> filtersLibrary;
        private FileSystemWatcher systemWatcher;

        private TempFilter folderNotFilter;

        public bool IsWatcing {
            get => systemWatcher.EnableRaisingEvents;
        }

        public Watcher() {

            systemWatcher = new FileSystemWatcher();

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

        public void UnSubscrible() =>
            systemWatcher.Created -= OnCreate;

        public void StartWatching() =>
            systemWatcher.EnableRaisingEvents = true;

        public void StopWatching() =>
            systemWatcher.EnableRaisingEvents = false;

        public void ApplySettings(SettingsContainer settings) {

            filtersLibrary = new Dictionary<string[], TempFilter>();

            if (settings.Empty()) {
                throw new ArgumentException("Unable to apply empty settings object.", nameof(settings));
            }

            foreach (var f in settings.Filters) {
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

            folderNotFilter = new EmptyTempFilter() {
                LastFolderPatch = settings.FolderNotFilter
            };
        }

        private void OnCreate(object source, FileSystemEventArgs e) {

            Thread.Sleep(OPERATION_INTERVAL);
            FileInfo file = new FileInfo(e.FullPath);

            TempFilter destination = GetRequestPatch(file.Extension);
            if (destination.IsExcess()) {
                destination.LastFolderPatch = FolderHandler
                    .GetNewSubFolder(destination.RootFolderPatch);
                destination.Counter = 0;
            }

            FolderHandler.CreateDirectoryIfNotFound(destination.LastFolderPatch);
            destination.Counter += destination.ReasonCreating.AddCount(file);
            FileHandler.MovedFile(file, Path.Combine(destination.LastFolderPatch));
        }

        private TempFilter GetRequestPatch(string fileExtension) {

            foreach (var c in filtersLibrary) {
                if (c.Key.Contains(fileExtension)) {

                    return c.Value;
                }
            }
            return folderNotFilter;
        }
    }
}
