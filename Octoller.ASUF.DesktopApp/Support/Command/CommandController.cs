/*
 * **************************************************************************************************************************
 *     _    ____  _   _ _____ 
 *    / \  / ___|| | | |  ___|
 *   / _ \ \___ \| | | | |_   
 *  / ___ \ ___) | |_| |  _|  
 * /_/   \_\____/ \___/|_|  
 * 
 * Octoller.ASUF
 * Library
 * 27.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.Processor;
using Octoller.ASUF.Kernel.ServiceObjects;

namespace Octoller.ASUF.DesktopApp.Support.Command {
    public class CommandController {

        private readonly SettingsBuilder settingsBuilder;
        private readonly Watcher watcher;

        private CommandBase addFilterCommand;
        private CommandBase defaultSettingsCommand;
        private CommandBase saveSettingsCommand;

        private CommandBase watchedFolderSelectCommand;
        private CommandBase folderNotFilterSelectCommand;
        private CommandBase filterRootFolderSelectCommand;


        private CommandBase deleteFilterCommand;

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

        public CommandBase WatchedFolderSelectCommand {
            get => watchedFolderSelectCommand ??=
                new PathFolderSelectCommand<SettingsContainer>(
                    (scw, t) => scw.WatchedFolder = t);
        }

        public CommandBase FolderNotFilterSelectCommand {
            get => folderNotFilterSelectCommand ??=
                new PathFolderSelectCommand<SettingsContainer>(
                    (scw, t) => scw.FolderNotFilter = t);
        }

        public CommandBase FilterRootFolderSelectCommand {
            get => filterRootFolderSelectCommand ??=
                new PathFolderSelectCommand<SortFilter>(
                    (sfw, t) => sfw.RootFolderPatch = t);
        }

        public CommandBase DeleteFilterCommand {
            get => deleteFilterCommand ??=
                new DeleteFilterCommand();
        }

        public CommandController(SettingsBuilder settingsBuilder, Watcher watcher) {
            this.settingsBuilder = settingsBuilder;
            this.watcher = watcher;
        }
    }
}
