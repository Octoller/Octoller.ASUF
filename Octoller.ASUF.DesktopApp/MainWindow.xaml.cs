using Octoller.ASUF.DesktopApp.Support;
using Octoller.ASUF.DesktopApp.View;
using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Octoller.ASUF.DesktopApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            DataContext = new ASUFApplicationViewModel();
        }

        /*
        private void Button_Click(object sender, RoutedEventArgs e) {

            settingsContainer = settingsBuilder.CreateDefaultSettings();
            watcher.StopWatching();
            watcher.ApplaySettings(settingsContainer);

            try {
                watcher.Subscrible();
            } catch {
                return;
            }

            settingsBuilder.SaveSettings(settingsContainer);
            DataContext = settingsContainer;
            watcher.StartWatching();
        }
        */
    }
}
