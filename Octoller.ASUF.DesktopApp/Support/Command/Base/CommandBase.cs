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
 * 25.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using System.Windows.Input;
using System;

namespace Octoller.ASUF.DesktopApp.Support.Command {

    public abstract class CommandBase : ICommand {

        public string Text {
            get;
        }

        protected CommandBase(string text) {
            Text = text;
        }

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
