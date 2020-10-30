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
using System.Diagnostics;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public class AddFilterInListCommand : CommandBase {

        public AddFilterInListCommand() : this("Add Filter") { }

        public AddFilterInListCommand(string text) : base(text) { }

        public override bool CanExecute(object parameter) =>
            parameter != null && parameter is SettingsContainer;

        public override void Execute(object parameter) {

            if (parameter is SettingsContainer settingsContainer) {
                Debug.Print(Text);
                settingsContainer.Filters.Add(new SortFilter());
            }
        }
    }
}
