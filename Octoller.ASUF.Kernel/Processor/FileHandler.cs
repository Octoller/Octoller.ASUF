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
    /// 
    /// </summary>
    public class FileHandler {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="destination"></param>
        public static void MovedFile(FileInfo file, string destination) {

            if (File.Exists(destination + file.Name)) {
                string newFullName = destination + Guid.NewGuid().ToString() + file.Extension;
                File.Move(file.FullName, newFullName);
            } else {
                File.Move(file.FullName, destination + file.Name);
            }
        }
    }
}
