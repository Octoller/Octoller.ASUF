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
 * 24.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.DesktopApp.Support;
using Octoller.ASUF.DesktopApp.Support.Command;
using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Octoller.ASUF.DesktopApp.View {

    public class ASUFApplicationViewModel : INotifyPropertyChanged {

        private SettingsBuilder settingsBuilder;
        private Watcher watcher;

        private SettingsContainer settingsContainer;
        private CommandController commandController;

        public SettingsContainer SettingsContainer {
            get => settingsContainer;
            set {
                settingsContainer = value;
                OnPropertyChanged();
            }
        }

        public CommandController CommandController {
            get => commandController ??=
                new CommandController(settingsBuilder, watcher);
        }

        public ASUFApplicationViewModel() {

            settingsBuilder = new SettingsBuilder();
            SettingsContainer = settingsBuilder.GetSettings();
            watcher = new Watcher();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
