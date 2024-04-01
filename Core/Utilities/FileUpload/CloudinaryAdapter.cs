using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.FileUpload;
public class CloudinaryAdapter : IFileUploadAdapter
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryAdapter(IConfiguration configuration)
    {
        Account account = configuration.GetSection("CloudinaryAccount").Get<Account>();
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        var fileUploadResponse = new ImageUploadResult();

        using (var stream = file.OpenReadStream())
        {
            var parameters = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = false,
                UniqueFilename = true,
                Overwrite = false

            };
            fileUploadResponse = await _cloudinary.UploadAsync(parameters);
        }
        return fileUploadResponse.SecureUri.AbsoluteUri;



        //var parameters = new ImageUploadParams()
        //{
        //    File = new FileDescription(file.FileName, stream: file.OpenReadStream()),
        //    UseFilename = false,
        //    UniqueFilename = true,
        //    Overwrite = false
        //};
        //var fileUploadResponse = await _cloudinary.UploadAsync(parameters);

        //return fileUploadResponse.SecureUri.AbsoluteUri;
    }


    public async Task DeleteFile(string filePath)
    {
        DeletionParams deletionParams = new(GetPublicId(filePath));
        await _cloudinary.DestroyAsync(deletionParams);
    }

    public async Task<string> UpdateFile(IFormFile formFile, string filePath)
    {
        await DeleteFile(filePath);
        return await UploadFile(formFile);
    }
    private string GetPublicId(string filePath)
    {
        //https://res.cloudinary.com/drlp7ruvm/image/upload/v1705681215/fg5e9ta86hnfnmxw7vjk.jpg
        int startIndex = filePath.LastIndexOf('/') + 1;
        int endIndex = filePath.LastIndexOf('.');
        int length = endIndex - startIndex;
        return filePath.Substring(startIndex, length);
    }
}