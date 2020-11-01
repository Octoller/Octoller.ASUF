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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;
using System;

namespace Octoller.ASUF.Kernel.Processor {

    /// <summary>
    /// Sorting files from the watched folder according to the specified settings.
    /// </summary>
    public sealed class Watcher {
        
        private const int OPERATION_INTERVAL = 200;

        private string watchedFolder = string.Empty;
        private Dictionary<string[], TempFilter> filtersLibrary;
        private readonly FileSystemWatcher systemWatcher;

        private TempFilter folderNotFilter;

        public event Action<WatcherMovedEventArg> OnMoveFile;

        /// <summary>
        /// Returns true if tracking is started.
        /// </summary>
        public bool IsWatcing {
            get => systemWatcher.EnableRaisingEvents;
        }

        /// <summary>
        /// Returns true if there are settings set.
        /// </summary>
        public bool IsAplaySettings {
            get => !string.IsNullOrEmpty(systemWatcher.Path)
                && filtersLibrary.Any();
        }

        /// <summary>
        /// Default construction.
        /// </summary>
        public Watcher() {

            systemWatcher = new FileSystemWatcher {
                InternalBufferSize = 63,
                NotifyFilter = NotifyFilters.FileName,
                Filter = "*.*"
            };
        }

        /// <summary>
        /// Starts the tracking process.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException">
        /// Issued if the path to the monitored directory is not specified.
        /// </exception>
        public void StartWatching() {

            if (string.IsNullOrEmpty(systemWatcher.Path)) {
                throw new DirectoryNotFoundException("Tracking paths not set");
            }

            systemWatcher.Created += OnCreate;
            systemWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Stop the tracking process
        /// </summary>
        public void StopWatching() {

            systemWatcher.EnableRaisingEvents = false;
            systemWatcher.Created -= OnCreate;
        }

        /// <summary>
        /// Set new settings.
        /// </summary>
        /// <param name="settings"> New object settings. </param>
        /// <exception cref="InvalidOperationException">
        /// Issued if the tracking process has not been stopped before installation.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if an empty settings object is passed.
        /// </exception>
        public void ApplySettings(SettingsContainer settings) {

            if (IsWatcing) {
                throw new InvalidOperationException("The tracking process " +
                    "has not been stopped before setting new settings.");
            }

            if (settings.Empty()) {
                throw new ArgumentException("Unable to apply empty settings object.");
            }

            filtersLibrary = SetFilters(settings.Filters);

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
                    .CreatePatchNewSubFolder(destination.RootFolderPatch);
                destination.Counter = 0;
            }

            FolderHandler.CreateDirectoryIfNotFound(destination.LastFolderPatch);
            destination.Counter += destination.ReasonCreating.AddCount(file);
            
            var movedFileInfo = FileHandler.MovedFile(file, Path.Combine(destination.LastFolderPatch));

            OnMoveFile?.Invoke(new WatcherMovedEventArg(movedFileInfo));
        }

        private TempFilter GetRequestPatch(string fileExtension) {

            foreach (var c in filtersLibrary) {

                if (c.Key.Contains(fileExtension)) {

                    return c.Value;
                }
            }
            return folderNotFilter;
        }

        private Dictionary<string[], TempFilter> SetFilters(Collection<SortFilter> filtersCollection) {

            var tempDictionary = new Dictionary<string[], TempFilter>();

            foreach (var f in filtersCollection) {

                string lastFolder =
                    FolderHandler.GetLastFolder(f.RootFolderPatch);

                tempDictionary.Add(f.Extension, new TempFilter(f.RootFolderPatch, f.Limit) {
                    LastFolderPatch = lastFolder,
                    Counter = f.ReasonCreating.CurrentCount(lastFolder),
                    ReasonCreating = f.ReasonCreating
                });
            }

            return tempDictionary;
        }
    }
}
