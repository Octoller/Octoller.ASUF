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

using Octoller.ASUF.Kernel.ServiceObjects;
using System.Linq;
using System.IO;
using System;

namespace Octoller.ASUF.Kernel.Processor {

    /// <summary>
    /// 
    /// </summary>
    public class FolderHandler {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public static void CreateFoldersIfNotFound(SettingsContainer settings) {

            CreateDirectoryIfNotFound(settings.WatchedFolder);
            CreateDirectoryIfNotFound(settings.FolderNotFilter);

            foreach (var f in settings.Filters) {
                CreateDirectoryIfNotFound(f.RootFolderPatch);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        public static string CreateDirectoryIfNotFound(string patch) =>
            (!Directory.Exists(patch)) ? Directory.CreateDirectory(patch).FullName : patch;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootPatch"></param>
        /// <returns></returns>
        public static string GetLastFolder(string rootPatch) {

            DirectoryInfo[] list;

            try {
                list = (new DirectoryInfo(rootPatch)).GetDirectories();
            } catch {
                list = new DirectoryInfo[] { };
            }

            if (list.Any()) {

                return list.OrderByDescending(di => di.CreationTime)
                    .First()
                    .FullName;
            } else {

                string newFolder = GetNewSubFolder(rootPatch);
                CreateDirectoryIfNotFound(newFolder);
                return newFolder;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        public static double GetSizeFolder(string patch) {

            long size = 0;
            (new DirectoryInfo(patch)).GetFiles().Where(f => true)
                .Select(f => (size = size + f.Length));

            return Convert.ToDouble(size / Math.Pow(2,20));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        public static int GetLenghtFolder(string patch) =>
            (new DirectoryInfo(patch)).GetFiles().Count();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootFolderPatch"></param>
        /// <returns></returns>
        public static string GetNewSubFolder(string rootFolderPatch) =>
           Path.Combine(rootFolderPatch, 
               DateTime.Now.ToString("dd.MM.yy hh_mm_ss"));
    }
}
