﻿/*
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

using Octoller.ASUF.Kernel.ServiceObjects;
using System;

namespace Octoller.ASUF.DesktopApp.Support.Command {
    public class PathFolderSelectCommand<T> : CommandBase {

        private Action<T, string> write;

        public PathFolderSelectCommand(Action<T, string> write) 
            : this (write, "Folder Select") { }

        public PathFolderSelectCommand(Action<T, string> write, string text)
            : base(text) {
            this.write = write;
        }

        public override bool CanExecute(object parameter) {

            if (parameter is SortFilter) {
                return true;
            }
            return true;
        }


        public override void Execute(object parameter) {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyDocuments;

            if (parameter is T container) {

                // Временный вариант через стороннюю библиотеку Ookii.Dialog, 
                // в будущем написать своё решение диалогового окна выбора папки

                if (dialog.ShowDialog().GetValueOrDefault()) {

                    write(container, dialog.SelectedPath);
                }
            }

        }
    }
}
