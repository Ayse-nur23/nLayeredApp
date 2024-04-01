using Microsoft.AspNetCore.Http;

namespace Business.Dtos.FileUploads;

public class UpdateFileUploadRequest
{
    public Guid Id { get; set; }
   // public string? FileName { get; set; }
  //  public string? Description { get; set; }
    public IFormFile? File { get; set; }
}