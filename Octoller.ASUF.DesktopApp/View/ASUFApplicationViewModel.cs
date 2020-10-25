using Octoller.ASUF.DesktopApp.Support;
using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Octoller.ASUF.DesktopApp.View {
    public class ASUFApplicationViewModel : INotifyPropertyChanged {

        private SettingsBuilder settingsBuilder;
        private Watcher watcher;
        private SettingsContainerWrap containerWrap;

        public SettingsContainerWrap ContainerWrap {
            get => containerWrap;
            set {
                containerWrap = value;
                OnPropertyChanged();
            }
        }

        public ASUFApplicationViewModel() {
            settingsBuilder = new SettingsBuilder();
            var tempSettings = settingsBuilder.GetSettings();
            containerWrap = new SettingsContainerWrap(tempSettings);
            watcher = new Watcher(tempSettings);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
