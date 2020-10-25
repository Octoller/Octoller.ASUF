using Octoller.ASUF.DesktopApp.Support;
using Octoller.ASUF.DesktopApp.Support.Command;
using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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

        private TestCommand addCommand;
        public TestCommand AddCommand {
            get {
                return addCommand ??
                    (addCommand = new TestCommand(obj => {
                        ContainerWrap.Filters.Add(new SortFilter());
                    }));
            }
        }

        private TestCommand defaultSettings;
        public TestCommand DefaultSettings {
            get {
                return defaultSettings ??
                    (defaultSettings = new TestCommand(obj => {
                        var tempSettings = settingsBuilder.CreateDefaultSettings();
                        watcher.StopWatching();
                        ContainerWrap = new SettingsContainerWrap(tempSettings);
                        watcher.ApplaySettings(tempSettings);

                        try {
                            watcher.Subscrible();
                        } catch {
                            return;
                        }

                        settingsBuilder.SaveSettings(tempSettings);
                        watcher.StartWatching();
                    }));
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
