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
 * 27.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class DeleteFilterCommand : CommandBase {

        public DeleteFilterCommand() : this ("Delete") { }

        public DeleteFilterCommand(string text) : base (text) { }

        public override bool CanExecute(object parameter) {

            if (parameter is SettingsContainer container) {

                if (container != null) {

                    return true;
                }
            }
            return false;
        }        

        public override void Execute(object parameter) {

            if (parameter is SettingsContainer container) {

                container.Filters.Remove(container.SelectedFilter);
            }
        }
    }
}
