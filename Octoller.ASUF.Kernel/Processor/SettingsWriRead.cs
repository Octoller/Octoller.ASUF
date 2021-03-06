﻿/*
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

using Octoller.ASUF.Kernel.ServiceObjects;
using Octoller.ASUF.Kernel.Extension;
using System.IO;
using System.Text;
using System.Text.Json;
using System;

namespace Octoller.ASUF.Kernel.Processor {

    using static Octoller.ASUF.Kernel.Resource.DefaultPath;

    /// <summary>
    /// Class for reading settings from file and writing.
    /// </summary>
    public sealed class SettingsWriRead {

        private JsonSerializerOptions jsonOptions;

        /// <summary>
        /// Default construction.
        /// </summary>
        public SettingsWriRead() {

            jsonOptions = new JsonSerializerOptions {
                WriteIndented = true,
                AllowTrailingCommas = true
            };
        }

        /// <summary>
        /// Reads settings from a file.
        /// </summary>
        /// <returns> New settings object </returns>
        /// <exception cref="IOException"> Called if the settings file is empty. </exception>
        public SettingsContainer ReadSettingFile() {

            using (var fs = File.Open(GetFilePath(), FileMode.OpenOrCreate)) {

                if (fs.Length <= 0) {
                    throw new IOException("File is empty");
                }

                byte[] fsArray = new byte[fs.Length];
                fs.Read(fsArray, 0, fsArray.Length);

                string jsonString = Encoding.Default.GetString(fsArray);
                return JsonSerializer.Deserialize<SettingsContainer>(jsonString, jsonOptions);
            }
        }

        /// <summary>
        /// Writes settings to a file.
        /// </summary>
        /// <param name="newSettings"> New settings object. </param>
        public void WriteSettingFile(SettingsContainer newSettings) {

            using (var fs = File.CreateText(GetFilePath())) {
                string jsonString = JsonSerializer.Serialize(newSettings, jsonOptions);
                fs.Write(jsonString);
            }
        }

        private string GetFilePath() {

            string folder = GetFolder(Directory.GetCurrentDirectory());
            return Path.Combine(folder, settingFileName);
        }

        private string GetFolder(string directoryPath) {

            string patch = Path.Combine(directoryPath, settingFolderName);

            if (!Directory.Exists(patch)) {

                var d = new DirectoryInfo(patch);
                d.Create();
            }
            return patch;
        }
    }
}
