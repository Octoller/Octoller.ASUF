using Octoller.ASUF.Kernel.Extension;
using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;

namespace Octoller.ASUF.DesktopApp.Support.Command {
    public class CancelСhangesCommand : CommandBase {

        private SettingsBuilder builder;

        public CancelСhangesCommand(SettingsBuilder builder) : this (builder, "CancelСhanges") { }

        public CancelСhangesCommand(SettingsBuilder builder, string text) : base (text) {
            this.builder = builder;
        }

        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainer;

        public override void Execute(object parameter) {
            if (parameter is SettingsContainer containder) {
                
                var tempSettings = builder.GetSettings();

                if (tempSettings.Empty()) {
                    tempSettings = builder.CreateDefaultSettings();
                }

                WriteInContainer(containder, tempSettings);
            }
        }

        private void WriteInContainer(SettingsContainer settingsOld, SettingsContainer settingsNew) {

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
