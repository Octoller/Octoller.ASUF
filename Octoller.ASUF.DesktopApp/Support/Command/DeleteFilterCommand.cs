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

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class DeleteFilterCommand : CommandBase {

        public override bool CanExecute(object parameter) {

            if (parameter is SettingsContainerWrap container) {

                if (container.SelectedFilter != null) {

                    return true;
                }
            }
            return false;
        }        

        public override void Execute(object parameter) {

            if (parameter is SettingsContainerWrap container) {

                container.Filters.Remove(container.SelectedFilter);
            }
        }
    }
}
