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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Octoller.ASUF.DesktopApp.View {

    public class ASUFApplicationViewModel : INotifyPropertyChanged {

        private SettingsBuilder settingsBuilder;
        private Watcher watcher;

        private SettingsContainerWrap containerWrap;
        private CommandController commandController;

        public SettingsContainerWrap ContainerWrap {
            get => containerWrap;
            set {
                containerWrap = value;
                OnPropertyChanged();
            }
        }

        public CommandController CommandController {
            get => commandController ??=
                new CommandController(settingsBuilder, watcher);
        }

        public ASUFApplicationViewModel() {

            settingsBuilder = new SettingsBuilder();
            var tempSettings = settingsBuilder.GetSettings();
            containerWrap = new SettingsContainerWrap(tempSettings);
            watcher = new Watcher();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
