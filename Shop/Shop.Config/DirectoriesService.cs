using Common.Aplication.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Config
{
    // Adapter in Shop.Config so we don't create a project reference cycle
    public class DirectoriesService : IDirectories
    {
        private readonly IFileService _fileService;

        public DirectoriesService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void ClearDirectory(string directoryPath)
        {
            var full = GetFullPath(directoryPath);
            if (!Directory.Exists(full)) return;

            foreach (var file in Directory.GetFiles(full))
                File.Delete(file);

            foreach (var dir in Directory.GetDirectories(full))
                Directory.Delete(dir, true);
        }

        public void CreateDirectory(string directoryPath)
        {
            var full = GetFullPath(directoryPath);
            if (!Directory.Exists(full))
                Directory.CreateDirectory(full);
        }

        public void DeleteDirectory(string directoryPath)
        {
            var full = GetFullPath(directoryPath);
            if (Directory.Exists(full))
                Directory.Delete(full, true);
        }

        public void DeleteFile(string path, string fileName)
        {
            _fileService.DeleteFile(path, fileName);
        }

        public void DeleteFile(string filePath)
        {
            _fileService.DeleteFile(filePath);
        }

        public List<string> GetDirectories(string directoryPath)
        {
            var full = GetFullPath(directoryPath);
            if (!Directory.Exists(full)) return new List<string>();
            return Directory.GetDirectories(full).Select(d => Path.GetFileName(d) ?? d).ToList();
        }

        public List<string> GetFiles(string directoryPath)
        {
            var full = GetFullPath(directoryPath);
            if (!Directory.Exists(full)) return new List<string>();
            return Directory.GetFiles(full).Select(f => Path.GetFileName(f) ?? f).ToList();
        }

        public string GetFullPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Directory.GetCurrentDirectory();

            var safe = relativePath.Replace("/", "\\");
            return Path.Combine(Directory.GetCurrentDirectory(), safe);
        }

        public long GetDirectorySize(string directoryPath)
        {
            var full = GetFullPath(directoryPath);
            if (!Directory.Exists(full)) return 0;
            return Directory.GetFiles(full, "*", SearchOption.AllDirectories).Sum(f => new FileInfo(f).Length);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(GetFullPath(directoryPath));
        }

        public async Task SaveFile(IFormFile file, string directoryPath)
        {
            await _fileService.SaveFile(file, directoryPath);
        }

        public async Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath)
        {
            return await _fileService.SaveFileAndGenerateName(file, directoryPath);
        }
    }
}
