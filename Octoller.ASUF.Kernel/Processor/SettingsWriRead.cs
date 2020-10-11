/*
 * **************************************************************************************************************************
 * 
 * Octoller.ASUF
 * 05.10.2020
 * 
 * ************************************************************************************************************************** 
 */


using Octoller.ASUF.Kernel.ServiceObjects;
using Octoller.ASUF.Kernel.Extension;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Octoller.ASUF.Kernel.Processor {

    using static Octoller.ASUF.Kernel.Resource.DefaultPath;

    public sealed class SettingsWriRead {

        private JsonSerializerOptions jsonOptions;
        private string filePath;

        public SettingsWriRead() : this(Directory.GetCurrentDirectory()) 
            { /*   */}

        private SettingsWriRead(string fileSettingPath) {

            jsonOptions = new JsonSerializerOptions {
                WriteIndented = true,
                AllowTrailingCommas = true
            };

            filePath = GetFilePath(fileSettingPath);
        }

        public SettingsContainer ReadSettingFile() {

            using (var fs = File.Open(filePath, FileMode.OpenOrCreate)) {

                if (fs.Length > 0) {

                    byte[] fsArray = new byte[fs.Length];
                    fs.Read(fsArray, 0, fsArray.Length);

                    string jsonString = Encoding.Default.GetString(fsArray);
                    return JsonSerializer.Deserialize<SettingsContainer>(jsonString, jsonOptions);
                
                } else {

                    return new SettingsContainer();
                }
            }
        }

        public void WriteSettingFile(SettingsContainer unit) {

            if (!unit.Empty()) {

                using (var fs = File.Open(filePath, FileMode.Open)) {

                    string jsonString = JsonSerializer.Serialize(unit, jsonOptions);
                    byte[] bytes = Encoding.Default.GetBytes(jsonString);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        private string GetFilePath(string directoryPath) {

            string folder = GetFolder(directoryPath);
            return folder + settingFileName;
        }

        private string GetFolder(string directoryPath) {

            string patch = directoryPath + settingFolderName;
            if (!Directory.Exists(patch)) {

                var d = new DirectoryInfo(patch);
                d.Create();
            }
            return patch;
        }
    }
}
