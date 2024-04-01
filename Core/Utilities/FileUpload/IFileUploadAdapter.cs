using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileUpload;

public interface IFileUploadAdapter
{
    Task<string> UploadFile(IFormFile file);

    Task DeleteFile(string filePath);
    Task<string> UpdateFile(IFormFile formFile, string filePath);
}
