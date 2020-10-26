using Octoller.ASUF.Kernel.Processor;
using System.IO;
using System;

namespace Octoller.ASUF {
    class Program {
        static void Main(string[] args) {

            SettingsBuilder settings = new SettingsBuilder();
            Watcher watcher = new Watcher();

            try {
                watcher.Subscrible();
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
