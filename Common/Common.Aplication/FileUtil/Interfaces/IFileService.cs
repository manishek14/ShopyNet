using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Common.Aplication.FileUtil.Interfaces
{
    public interface IFileService
    {
        Task SaveFile(IFormFile file,string directoryPath);
        Task<string> SaveFileAndGenerateName(IFormFile file,string directoryPath);
        void DeleteFile(string path, string fileName);
        void DeleteFile(string filePath);
        void DeleteDirectory(string directoryPath);
    }
}
