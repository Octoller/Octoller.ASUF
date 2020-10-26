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
using System;
using System.IO;
using System.Linq;

namespace Octoller.ASUF.Kernel.Processor {
    public class FolderHandler {

        public Action<SortFilter> newSubFolderCreate;

        public FolderHandler() { } 

        public static void CheckFoldersByPaths(SettingsContainer settings) {

            CreateDirectoryIfNotFound(settings.WatchedFolder);
            CreateDirectoryIfNotFound(settings.FolderNotFilter);

            foreach (var f in settings.Filter) {
                CreateDirectoryIfNotFound(f.RootFolderPatch);
            }
        }

        public static string CreateDirectoryIfNotFound(string patch) {

            if (!Directory.Exists(patch)) {
                Directory.CreateDirectory(patch);
            }

            return patch;
        }

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
                var newFolder = rootPatch + 
                    DateTime.Now.ToString("dd.MM.yy hh_mm_ss");
                CreateDirectoryIfNotFound(newFolder);
                return newFolder;
            }
        }

        public static double GetSizeFolder(string patch) {

            long size = 0;
            (new DirectoryInfo(patch)).GetFiles()
                .Where(f => true)
                .Select(f => (size = size + f.Length));

            return Convert.ToDouble(size / Math.Pow(2,20));
        }


        public static int GetLenghtFolder(string patch) =>
            (new DirectoryInfo(patch)).GetFiles().Count();

        public string GetNewSubFolder(string rootFolderPatch) =>
            rootFolderPatch + DateTime.Now.ToString("dd.MM.yy hh_mm_ss");
    }
}
