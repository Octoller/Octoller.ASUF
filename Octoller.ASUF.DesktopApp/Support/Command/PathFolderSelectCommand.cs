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
 * 26.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using System;
using System.Diagnostics;

namespace Octoller.ASUF.DesktopApp.Support.Command {
    public class PathFolderSelectCommand : CommandBase {

        private Action<SettingsContainerWrap, string> write;

        public PathFolderSelectCommand(Action<SettingsContainerWrap, string> write) {
            this.write = write;
        }

        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainerWrap;


        public override void Execute(object parameter) {

            if (parameter is SettingsContainerWrap container) {

                // Временный вариант через стороннюю библиотеку Ookii.Dialog, 
                // в будущем написать своё решение диалогового окна выбора папки

                var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                dialog.RootFolder = Environment.SpecialFolder.MyDocuments;

                if (dialog.ShowDialog().GetValueOrDefault()) {

                    write(container, dialog.SelectedPath);
                }
            }
        }
    }
}
