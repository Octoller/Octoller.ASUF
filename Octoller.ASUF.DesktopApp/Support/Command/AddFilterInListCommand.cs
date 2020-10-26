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

using System.Diagnostics;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class AddFilterInListCommand : CommandBase {
        
        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainerWrap;

        public override void Execute(object parameter) {

            if (parameter is SettingsContainerWrap settingsContainer) {

                settingsContainer.Filters.Add(new SortFilterWrap());
            }
        }
    }
}
