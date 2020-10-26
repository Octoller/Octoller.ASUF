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

        private CommandBase addFilterCommand;
        private CommandBase defaultSettingsCommand;
        private CommandBase saveSettingsCommand;
        private CommandBase selectPatchFolderCommand;

        public SettingsContainerWrap ContainerWrap {
            get => containerWrap;
            set {
                containerWrap = value;
                OnPropertyChanged();
            }
        }

        public CommandBase AddFilterCommand {
            get => addFilterCommand ??=
                new AddFilterInListCommand();
        }

        public CommandBase DefaultSettingsCommand {
            get => defaultSettingsCommand ??= 
                new SetDefaultSettingsCommand(settingsBuilder);
        }

        public CommandBase SaveSettingsCommand {
            get => saveSettingsCommand ??= 
                new SaveCurrentSettingsCommand(settingsBuilder, watcher);
        }

        public CommandBase SelectPatchFolderCommand {
            get => selectPatchFolderCommand ??=
                new SelectPathFolderCommand((csw, p) => csw.WatchedFolder = p);
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
