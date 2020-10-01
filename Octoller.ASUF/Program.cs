using Octoller.ASUF.SystemLogic.Processor;
using System.IO;
using System;

namespace Octoller.ASUF {
    class Program {
        static void Main(string[] args) {

            FolderSettings settings = new FolderSettings();
            Watcher watcher = new Watcher(settings.SettingUnit);

            try {
                watcher.Subscribe();
            } catch (DirectoryNotFoundException ex) {
                Console.WriteLine(ex.Message);
            }

            watcher.StartWatching();

            while (true)
                ;
        }
    }
}
