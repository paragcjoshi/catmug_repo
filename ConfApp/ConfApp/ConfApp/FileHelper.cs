using System;
using System.Threading.Tasks;
using PCLStorage;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConfApp
{
    public class FileHelper
    {
        private const string subFolderName = "ConfApp";

        public async Task CreateFile(string fileName, string fileContent)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync(subFolderName, CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(fileContent);
        }

        public async Task DeleteFile(string fileName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.GetFolderAsync(subFolderName);
            var file = await folder.GetFileAsync(fileName);
            await file.DeleteAsync();
        }

        public async Task<string> LoadFile(string fileName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.GetFolderAsync(subFolderName);
            var file = await folder.GetFileAsync(fileName);
            return await file.ReadAllTextAsync();
        }

        public async Task<bool> CheckifFileExists(string fileName)
        {
            var result = false;
            var rootFolder = FileSystem.Current.LocalStorage;
            if (await rootFolder.CheckExistsAsync(subFolderName) == ExistenceCheckResult.FolderExists)
            {
                var folder = await rootFolder.GetFolderAsync(subFolderName);
                result = await folder.CheckExistsAsync(fileName) == ExistenceCheckResult.FileExists;
            }
            return result;
        }

    }
}
