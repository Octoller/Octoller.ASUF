using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Octoller.ASUF.DesktopApp {

    public partial class App : Application {

        private Mutex instanceMutex = null;
        private static string appName = "Octoller.ASUF";

        protected override void OnStartup(StartupEventArgs e) {

            bool createNew;

            instanceMutex = new Mutex(true, appName, out createNew);

            if (!createNew) {
                instanceMutex = null;
                Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e) {

            if (instanceMutex != null) {
                instanceMutex.ReleaseMutex();
            }

            base.OnExit(e);
        }

    }
}
