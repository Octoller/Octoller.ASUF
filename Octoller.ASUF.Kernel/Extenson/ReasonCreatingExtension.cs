/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */

using Octoller.ASUF.Kernel.ServiceObjects;
using System;
using System.IO;

namespace Octoller.ASUF.SystemLogic.Extension {
    public static class ReasonCreatingExtension {
        public static string Create(this ReasonCreatingFolder reason, string currentPath, int limit = 200) {
            switch (reason) {
                case ReasonCreatingFolder.OverflowAmount:
                    var folder = new DirectoryInfo(currentPath);
                    var files = folder.GetFiles();
                    if (files.Length > limit) {
                        return currentPath + DateTime.Now.ToString("dd.MM.yy hh_mm_ss");
                    } else {
                        return currentPath;
                    }
                default: return currentPath;
            }
        }
    }
}
