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

using Octoller.ASUF.DesktopApp.Support.Command;
using Octoller.ASUF.Kernel.ServiceObjects;
using System.Runtime.CompilerServices;
using Octoller.ASUF.Kernel.Processor;
using System.ComponentModel;
using Octoller.ASUF.Kernel.Extension;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Octoller.ASUF.DesktopApp.View {

    public class ASUFApplicationViewModel : INotifyPropertyChanged {

        private SettingsContainer settingsContainer;
        private CommandController commandController;
        private readonly SettingsBuilder settingsBuilder;
        private readonly Watcher watcher;

        public ObservableCollection<WatcherMovedEventArg> MovedFileCollection {
            get; set;
        } = new ObservableCollection<WatcherMovedEventArg>();

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

            if (!SettingsContainer.Empty()) {

                if (!watcher.IsWatcing) {
                    watcher.ApplySettings(SettingsContainer);
                }
            }

            watcher.OnMoveFile += (arg) => {
                App.Current.Dispatcher.Invoke(new System.Action(
                    () => MovedFileCollection.Add(arg)
                ));
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
