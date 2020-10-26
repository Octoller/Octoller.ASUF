using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;

namespace Octoller.ASUF.DesktopApp.Support.Command {
    public class SelectPathFolderCommand : CommandBase {

        private Action<SettingsContainerWrap, string> write;

        public SelectPathFolderCommand(Action<SettingsContainerWrap, string> write) {
            this.write = write;
        }

        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainerWrap;


        public override void Execute(object parameter) {

            if (parameter is SettingsContainerWrap container) {

                var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                dialog.RootFolder = Environment.SpecialFolder.MyDocuments;

                if (dialog.ShowDialog().GetValueOrDefault()) {
                    write(container, dialog.SelectedPath);
                }
            }
        }
    }
}
