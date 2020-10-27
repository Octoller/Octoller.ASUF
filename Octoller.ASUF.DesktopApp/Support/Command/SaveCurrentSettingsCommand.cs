/*
 * **************************************************************************************************************************
 *     _    ____  _   _ _____ 
 *    / \  / ___|| | | |  ___|
 *   / _ \ \___ \| | | | |_   
 *  / ___ \ ___) | |_| |  _|  
 * /_/   \_\____/ \___/|_|  
 * 
 * Octoller.ASUF
 * Desctop.WPF
 * 26.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class SaveCurrentSettingsCommand : CommandBase {

        private SettingsBuilder builder;
        private Watcher watcher;

        public SaveCurrentSettingsCommand(SettingsBuilder builder, Watcher watcher) {

            this.watcher = watcher;
            this.builder = builder;
        }

        public override bool CanExecute(object parameter) {


            if (parameter is SettingsContainerWrap container) {

                return container.Empty() ? false : !container.Filters.Any(f => f.IsEmpty);                    
            }

            return true;
        }
            
            

        public override void Execute(object parameter) {
            
            if (parameter is SettingsContainerWrap container) {

                var tempContainer = CreateContainer(container);

                watcher.StopWatching();

                try {

                    watcher.ApplaySettings(tempContainer);
                    watcher.Subscrible();
                    builder.SaveSettings(tempContainer);

                } catch (Exception ex) {

                    MessageBox.Show(ex.StackTrace);
                    return;
                }

                watcher.StartWatching();
            }
        }

        private SettingsContainer CreateContainer(SettingsContainerWrap container) {

            var tempList = new List<SortFilter>();

            Array.ForEach(container.Filters.ToArray(),
                f => tempList.Add(new SortFilter() {
                    Extension = f.Extension,
                    RootFolderPatch = f.RootFolderPatch,
                    ReasonCreating = f.ReasonCreating,
                    Limit = f.Limit
                }));

            return new SettingsContainer() {
                Filter = tempList.ToArray(),
                FolderNotFilter = container.FolderNotFilter,
                WatchedFolder = container.WatchedFolder
            };
        }
    }
}
