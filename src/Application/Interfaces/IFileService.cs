using Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IFileService
    {
        public Task<byte[]> GetFile(string path);
        public Task<FileDetailDto> SaveFileToDisk(IFormFile file);
        Task<IEnumerable<FileDetailDto>> SaveMultipleFileToDisk(IFormFileCollection files);
    }
}
