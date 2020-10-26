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

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class SaveCurrentSettingsCommand : CommandBase {

        private SettingsBuilder builder;

        public SaveCurrentSettingsCommand(SettingsBuilder builder) {

            this.builder = builder;
        }

        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainerWrap;

        public override void Execute(object parameter) {
            
            if (parameter is SettingsContainerWrap container) {

                var tempContainer = CreateContainer(container);
                builder.SaveSettings(tempContainer);
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
