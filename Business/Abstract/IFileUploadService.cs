using Business.Dtos.FileUploads;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface IFileUploadService
{
    //Task<IPaginate<GetListFileUploadsResponse>> GetListAsync(PageRequest pageRequest);
   // Task<GetListProductResponse> GetAsync(int id);
    Task<CreatedFileUploadResponse> Add(CreateFileUploadRequest CreateFileUploadsRequest);
    string Upload(IFormFile file, string extension = "");
    Task<DeletedFileUploadResponse> Delete(DeleteFileUploadRequest deleteFileUploadsRequest);
    Task<UpdatedFileUploadResponse> Update(UpdateFileUploadRequest updateFileUploadsRequest);
}