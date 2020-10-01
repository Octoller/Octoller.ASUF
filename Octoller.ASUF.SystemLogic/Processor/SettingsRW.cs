using Octoller.ASUF.SystemLogic.ServiceObjects;
using Octoller.ASUF.SystemLogic.ServiceObjects.Extension;
using System.IO;
using System.Text;
using System.Text.Json;

using static Octoller.ASUF.SystemLogic.Resource.DefaultPath;

namespace Octoller.ASUF.SystemLogic.Processor {
    public class SettingsRW {

        private JsonSerializerOptions jsonOptions;
        private string filePath;

        public SettingsRW() : this (Directory.GetCurrentDirectory()){ /*   */}

        private SettingsRW(string fileSettingPath) {
            jsonOptions = new JsonSerializerOptions {
                WriteIndented = true,
                AllowTrailingCommas = true
            };

            filePath = GetFilePath(fileSettingPath);
        }

        public SettingUnit ReadSettingFile() {
            using (var fs = File.Open(filePath, FileMode.OpenOrCreate)) {
                if (fs.Length > 0) {
                    byte[] fsArray = new byte[fs.Length];
                    fs.Read(fsArray, 0, fsArray.Length);

                    string jsonString = Encoding.Default.GetString(fsArray);
                    return JsonSerializer.Deserialize<SettingUnit>(jsonString, jsonOptions);
                } else {
                    return new SettingUnit();
                }
            }
        }

        public void WriteSettingFile(SettingUnit unit) {
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
            return folder + settingFolderName;
        }

        private string GetFolder(string directoryPath) {
            string patch = directoryPath + settingFileName;
            if (!Directory.Exists(patch)) {
                var d = new DirectoryInfo(patch);
                d.Create();
            }
            return patch;
        }

    }
}
