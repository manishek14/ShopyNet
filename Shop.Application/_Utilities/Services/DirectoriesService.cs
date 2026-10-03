using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application._Utilities
{
    public class DirectoriesService : IDirectories
    {
        public void CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return;

            var folderName = GetFullPath(directoryPath);
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
        }

        public bool DirectoryExists(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return false;

            return Directory.Exists(GetFullPath(directoryPath));
        }

        public void DeleteDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return;

            var folderName = GetFullPath(directoryPath);
            if (Directory.Exists(folderName))
                Directory.Delete(folderName, recursive: true);
        }

        public void ClearDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return;

            var folderName = GetFullPath(directoryPath);
            if (!Directory.Exists(folderName))
                return;

            foreach (var file in Directory.GetFiles(folderName))
                File.Delete(file);

            foreach (var directory in Directory.GetDirectories(folderName))
                Directory.Delete(directory, recursive: true);
        }

        public async Task SaveFile(IFormFile file, string directoryPath)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("Directory path is required.", nameof(directoryPath));

            var folderName = GetFullPath(directoryPath);
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var path = Path.Combine(folderName, file.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        public async Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("Directory path is required.", nameof(directoryPath));

            var fileName = Guid.NewGuid().ToString("N") + "_" +
                           DateTime.Now.ToString("yyyyMMddHHmmss") +
                           Path.GetExtension(file.FileName);

            var folderName = GetFullPath(directoryPath);
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var path = Path.Combine(folderName, fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }

        public void DeleteFile(string path, string fileName)
        {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(fileName))
                return;

            var filePath = Path.Combine(GetFullPath(path), fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public List<string> GetFiles(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return new List<string>();

            var folderName = GetFullPath(directoryPath);
            if (!Directory.Exists(folderName))
                return new List<string>();

            return Directory.GetFiles(folderName).ToList();
        }

        public List<string> GetDirectories(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return new List<string>();

            var folderName = GetFullPath(directoryPath);
            if (!Directory.Exists(folderName))
                return new List<string>();

            return Directory.GetDirectories(folderName).ToList();
        }

        public string GetFullPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new ArgumentException("Relative path is required.", nameof(relativePath));

            var cleanedPath = relativePath
                .TrimStart('/', '\\')
                .Replace("/", Path.DirectorySeparatorChar.ToString())
                .Replace("\\", Path.DirectorySeparatorChar.ToString());

            return Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                cleanedPath
            );
        }

        public long GetDirectorySize(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return 0;

            var folderName = GetFullPath(directoryPath);
            if (!Directory.Exists(folderName))
                return 0;

            var directoryInfo = new DirectoryInfo(folderName);
            return directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories)
                .Sum(file => file.Length);
        }
    }
}