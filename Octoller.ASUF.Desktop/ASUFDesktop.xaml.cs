using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Octoller.ASUF.Kernel.Extension;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Octoller.ASUF.Kernel.Processor;

namespace Octoller.ASUF.Desktop {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ASUFDesctop : Window {

        private SettingsBuilder settingsBuilder;
        private Watcher watcher;
        private SettingsContainer settingsContainer;

        public ASUFDesctop() {
            InitializedComponent();
            settingsBuilder = new SettingsBuilder();
            settingsContainer = settingsBuilder.GetSettings();
            watcher = new Watcher(settingsContainer);
            DataContext = settingsContainer;
            //CreateList(settingsContainer);
        }
    }
}
