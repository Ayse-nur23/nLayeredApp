using AutoMapper;
using Business.Dtos.FileUploads;
using Core.DataAccess.Dynamic;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles;

public class FileUploadProfile : Profile
{
    public FileUploadProfile()
    {
        CreateMap<FileUpload, CreateFileUploadRequest>().ReverseMap();
        CreateMap<FileUpload, CreatedFileUploadResponse>().ReverseMap();

        CreateMap<FileUpload, DeleteFileUploadRequest>().ReverseMap();
        CreateMap<FileUpload, DeletedFileUploadResponse>().ReverseMap();

        CreateMap<FileUpload, UpdateFileUploadRequest>().ReverseMap();
        CreateMap<FileUpload, UpdatedFileUploadResponse>().ReverseMap();
    }
}
