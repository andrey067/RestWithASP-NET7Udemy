using Application.Dtos;
using Application.Enums;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _contextAccessor;

        public FileService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            var targetDirectory = Directory.GetCurrentDirectory() + "\\UploadFiles\\";

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            _basePath = targetDirectory;
        }

        public Task<byte[]> GetFile(string path)
        {
            var filePath = _basePath + path;
            return File.ReadAllBytesAsync(filePath);
        }

        public async Task<FileDetailDto> SaveFileToDisk(IFormFile file)
        {
            var fileType = Path.GetExtension(file.FileName).TrimStart('.').ToUpper();
            var baseUrl = _contextAccessor.HttpContext!.Request.Host.Value;
            FileType parsedFileType = GetFileType(fileType);

            ValidateType(parsedFileType);

            var fileName = Guid.NewGuid().ToString() + "." + fileType;
            var filePath = Path.Combine(_basePath, fileName);

            var fileUrl = Path.Combine(baseUrl + "/api/file/", fileName);

            var fileDetail = new FileDetailDto(fileName, parsedFileType.ToString(), fileUrl);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileDetail;
        }

        public async Task<IEnumerable<FileDetailDto>> SaveMultipleFileToDisk(IFormFileCollection files)
        {
            var fileDetails = new List<FileDetailDto>();

            foreach (var file in files)
            {
                var fileDetail = await SaveFileToDisk(file);
                fileDetails.Add(fileDetail);
            }

            return fileDetails;
        }

        private static void ValidateType(FileType parsedFileType)
        {
            FileType[] allowedFileTypes = { FileType.PDF, FileType.JPG, FileType.PNG, FileType.JPEG };

            if (!allowedFileTypes.Contains(parsedFileType))
                throw new ArgumentException("File type not allowed.");
        }

        private static FileType GetFileType(string fileType)
        {
            if (!Enum.TryParse(fileType, out FileType parsedFileType) || !Enum.IsDefined(typeof(FileType), parsedFileType))
                throw new ArgumentException("Invalid file type.");

            return parsedFileType;
        }
    }
}
