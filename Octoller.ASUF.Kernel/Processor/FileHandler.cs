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
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using System.IO;
using System;

namespace Octoller.ASUF.Kernel.Processor {

    /// <summary>
    /// Static class for working with files.
    /// </summary>
    public static class FileHandler {

        /// <summary>
        /// Moves a folder to the specified directory.
        /// </summary>
        /// <param name="file"> Relocatable file. </param>
        /// <param name="destination"> Destination directory address. </param>
        public static FileInfo MovedFile(FileInfo file, string destination) {

            string newFullName;
            if (File.Exists(Path.Combine(destination, file.Name))) {
                newFullName = Path.Combine(destination, Guid.NewGuid().ToString() + file.Extension);
            } else {
                newFullName = Path.Combine(destination, file.Name);
            }

            File.Move(file.FullName, newFullName);

            return new FileInfo(newFullName);
        }
    }
}
