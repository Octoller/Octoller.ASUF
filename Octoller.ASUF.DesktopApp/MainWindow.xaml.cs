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

using Octoller.ASUF.DesktopApp.View;
using System.Windows;

namespace Octoller.ASUF.DesktopApp {

    public partial class MainWindow : Window {

        public MainWindow() {

            InitializeComponent();

            this.DataContext = new ASUFApplicationViewModel();
        }
    }
}
