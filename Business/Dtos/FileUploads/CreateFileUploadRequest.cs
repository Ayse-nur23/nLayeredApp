using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.FileUploads;

public class CreateFileUploadRequest
{
  //  public IFormFile File { get; set; }
    //public string Description { get; set; }
    //public string? Extension { get; set; }
    public IList<IFormFile> Files { get; set; }
}
