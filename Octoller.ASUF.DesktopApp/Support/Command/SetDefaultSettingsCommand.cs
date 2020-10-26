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

using Octoller.ASUF.Kernel.Extension;
using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class SetDefaultSettingsCommand : CommandBase {

        private SettingsBuilder builder;

        public SetDefaultSettingsCommand(SettingsBuilder builder) {
            this.builder = builder;
        }

        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainerWrap;

        public override void Execute(object parameter) {

            if (parameter is SettingsContainerWrap container) {

                var tempContainer = builder.CreateDefaultSettings();
                WriteInContainerWrap(container, tempContainer);
            }
        }

        private void WriteInContainerWrap(SettingsContainerWrap settingsWrap, SettingsContainer settings) {
            
            settingsWrap.Filters.Clear();
            settingsWrap.FolderNotFilter = string.Empty;
            settingsWrap.WatchedFolder = string.Empty;

            if (!settings.Empty()) {

                Array.ForEach(settings.Filter, 
                    f => settingsWrap.Filters.Add(new SortFilterWrap() {
                        Extension = f.Extension,
                        RootFolderPatch = f.RootFolderPatch,
                        ReasonCreating = f.ReasonCreating,
                        Limit = f.Limit
                    }));

                settingsWrap.FolderNotFilter = settings.FolderNotFilter;
                settingsWrap.WatchedFolder = settings.WatchedFolder;
            }
        }
    }
}
