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


using Octoller.ASUF.Kernel.ServiceObjects;
using Octoller.ASUF.Kernel.Extension;
using Octoller.ASUF.Kernel.Processor;
using System;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class SetDefaultSettingsCommand : CommandBase {

        private SettingsBuilder builder;

        public SetDefaultSettingsCommand(SettingsBuilder builder) 
            : this (builder, "Set Default Settings") { }

        public SetDefaultSettingsCommand(SettingsBuilder builder, string text) 
            : base(text) {

            this.builder = builder;
        }

        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainer;

        public override void Execute(object parameter) {

            if (parameter is SettingsContainer container) {

                var tempContainer = builder.CreateDefaultSettings();
                WriteInContainerWrap(container, tempContainer);
            }
        }

        private void WriteInContainerWrap(SettingsContainer settingsOld, SettingsContainer settingsNew) {

            settingsOld.Filters.Clear();

            if (!settingsNew.Empty()) {

                for (int i = 0; i < settingsNew.Filters.Count; i++) {
                    settingsOld.Filters.Add(new SortFilter() {
                        Extension = settingsNew.Filters[i].Extension,
                        RootFolderPatch = settingsNew.Filters[i].RootFolderPatch,
                        ReasonCreating = settingsNew.Filters[i].ReasonCreating,
                        Limit = settingsNew.Filters[i].Limit
                    });
                }

                settingsOld.FolderNotFilter = settingsNew.FolderNotFilter;
                settingsOld.WatchedFolder = settingsNew.WatchedFolder;
            }
        }
    }
}
