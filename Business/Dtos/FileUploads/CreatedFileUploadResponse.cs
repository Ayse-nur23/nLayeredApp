using Microsoft.AspNetCore.Http;

namespace Business.Dtos.FileUploads;

public class CreatedFileUploadResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Destination { get; set; }
    public string Description { get; set; }
    public string? Extension { get; set; }
}
