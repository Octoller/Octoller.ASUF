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
 * 01.11.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;
using Octoller.ASUF.Kernel.Processor;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class StopWatchingCommand : CommandBase {

        private Watcher watcher;

        public StopWatchingCommand(Watcher watcher)
            : this(watcher, "Stop") { }

        public StopWatchingCommand(Watcher watcher, string text) 
            : base (text) {
            this.watcher = watcher;
        }

        public override bool CanExecute(object parameter)
            => parameter != null
            && parameter is SettingsContainer
            && watcher.IsWatcing;

        public override void Execute(object parameter) {

            if (parameter is SettingsContainer container) {
                if (watcher.IsWatcing) {
                    watcher.StopWatching();
                }
            }
        }
    }
}
