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
 * 23.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using System.Threading;
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
