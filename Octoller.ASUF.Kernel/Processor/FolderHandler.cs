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
 * 06.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using System.Linq;
using System.IO;
using System;

namespace Octoller.ASUF.Kernel.Processor {

    /// <summary>
    /// Static class for working with directories.
    /// </summary>
    public static class FolderHandler {


        /// <summary>
        /// Checks the specified path, if the directory does not exist - creates it.
        /// </summary>
        /// <param name="patch"> Directory path. </param>
        /// <returns> Return directory path. </returns>
        public static string CreateDirectoryIfNotFound(string patch) =>
            !Directory.Exists(patch) ? Directory.CreateDirectory(patch).FullName : patch;

        /// <summary>
        /// Returns the path to the last timed subfolder in the sort root folder.
        /// </summary>
        /// <param name="rootPatch"> Root directory path. </param>
        /// <returns> Path of the last created directory. </returns>
        public static string GetLastFolder(string rootPatch) {

            try {

                return (new DirectoryInfo(rootPatch)).GetDirectories()
                    .OrderByDescending(di => di.CreationTime)
                    .First()
                    .FullName;

            } catch {

                string newFolder = CreatePatchNewSubFolder(rootPatch);
                return CreateDirectoryIfNotFound(newFolder);
            }
        }

        /// <summary>
        /// Returns the size of a directory in megabytes.
        /// </summary>
        /// <param name="patch"> Directory path. </param>
        /// <returns> Size of a directory in megabytes. </returns>
        public static double GetSizeFolder(string patch) {

            long size = 0;
            (new DirectoryInfo(patch)).GetFiles().Where(f => true)
                .Select(f => (size = size + f.Length));

            return Convert.ToDouble(size / Math.Pow(2,20));
        }

        /// <summary>
        /// Returns the number of files in a directory.
        /// </summary>
        /// <param name="patch"> Directory path. </param>
        /// <returns> Number of files. </returns>
        public static int GetLenghtFolder(string patch) =>
            (new DirectoryInfo(patch)).GetFiles().Count();

        /// <summary>
        /// Creates a new subfolder in the root folder. 
        /// The current date and time in the format dd.MM.yy hh_mm_ss.
        /// </summary>
        /// <param name="rootFolderPatch"> Root directory path. </param>
        /// <returns> Full name of the new subfolder. </returns>
        public static string CreatePatchNewSubFolder(string rootFolderPatch) =>
           Path.Combine(rootFolderPatch, 
               DateTime.Now.ToString("dd.MM.yy hh_mm_ss"));
    }
}
