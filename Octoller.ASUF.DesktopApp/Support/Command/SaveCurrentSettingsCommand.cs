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


            if (parameter is SettingsContainer container) {
                return container.Empty() ? false : !container.Filters.Any(f => f.Empty());
            } 

            return false;
        }

        public override void Execute(object parameter) {
            
            if (parameter is SettingsContainer container) {

                try {

                    bool restart = false;

                    if (watcher.IsWatcing) {
                        watcher.StopWatching();
                        restart = true;
                    }

                    watcher.UnSubscrible();
                    builder.SaveSettings(container);
                    watcher.ApplySettings(container);
                    watcher.Subscrible();

                    if (restart) {
                       watcher.StartWatching();
                    }

                } catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
    }
}
