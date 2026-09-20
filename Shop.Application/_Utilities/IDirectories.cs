using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Application._Utilities
{
    public interface IDirectories
    {
        void CreateDirectory(string directoryPath);
        bool DirectoryExists(string directoryPath);
        void DeleteDirectory(string directoryPath);
        void ClearDirectory(string directoryPath);

        Task SaveFile(IFormFile file, string directoryPath);
        Task<string> SaveFileAndGenerateName(IFormFile file, string directoryPath);
        void DeleteFile(string path, string fileName);
        void DeleteFile(string filePath);

        List<string> GetFiles(string directoryPath);
        List<string> GetDirectories(string directoryPath);
        string GetFullPath(string relativePath);
        long GetDirectorySize(string directoryPath);
    }
}