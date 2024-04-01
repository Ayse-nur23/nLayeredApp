using Business.Abstract;
using Business.Dtos.Categories;
using Business.Dtos.FileUploads;
using Business.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadsController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadsController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CreateFileUploadRequest createFileUploadRequest)
        {
             var result = await _fileUploadService.Add(createFileUploadRequest);
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteFileUploadRequest deleteFileUploadRequest)
        {
            var result =  await _fileUploadService.Delete(deleteFileUploadRequest);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] UpdateFileUploadRequest updateFileUploadsRequest)
        {
            var result = await _fileUploadService.Update(updateFileUploadsRequest);
            return Ok(result);
        }
    }
}
