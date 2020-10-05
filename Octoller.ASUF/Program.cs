using Octoller.ASUF.SystemLogic.Processor;
using System.IO;
using System;

namespace Octoller.ASUF {
    class Program {
        static void Main(string[] args) {

            FolderSettings settings = new FolderSettings();
            Watcher watcher = new Watcher(settings.GetSettings());

            try {
                watcher.Subscribe();
            } catch (DirectoryNotFoundException ex) {
                Console.WriteLine(ex.Message);
                return;
            }

            watcher.StartWatching();

            while (true)
                ;
        }
    }
}
