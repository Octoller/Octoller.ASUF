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
 * 01.11.2020
 * 
 * ************************************************************************************************************************** 
 */

using System.IO;
using System;

namespace Octoller.ASUF.Kernel.ServiceObjects {

    public class WatcherMovedEventArg {

        public string FileName {
            get; private set;
        }

        public string FileExtension {
            get;
        }

        public string FilePath {
            get;
        }

        public DateTime MovedTime {
            get;
        }

        public WatcherMovedEventArg(FileInfo fileInfo) {
            FileExtension = fileInfo.Extension;
            FileName = fileInfo.Name.Replace(FileExtension, "");
            FilePath = fileInfo.FullName.Replace(fileInfo.Name, "");
            MovedTime = DateTime.Now;
        }
    }
}
