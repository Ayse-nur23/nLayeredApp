using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Business.Dtos.Customers;
using Business.Dtos.FileUploads;
using Business.Rules;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.FileUpload;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;

namespace Business.Concrete;

public class FileUploadManager : IFileUploadService
{
    private readonly IMapper _mapper;
    private readonly IFileUploadDal _fileUploadDal;
    private readonly IFileUploadAdapter _fileUploadAdapter;
    private readonly FileUploadBusinessRules _fileUploadBusinessRules;
    public FileUploadManager(IMapper mapper, IFileUploadDal fileUploadDal, IFileUploadAdapter fileUploadAdapter, FileUploadBusinessRules fileUploadBusinessRules)
    {
        _mapper = mapper;
        _fileUploadDal = fileUploadDal;
        _fileUploadAdapter = fileUploadAdapter;
        _fileUploadBusinessRules = fileUploadBusinessRules;
    }

    [TransactionScopeAspectAsync]
    public async Task<CreatedFileUploadResponse> Add(CreateFileUploadRequest createFileUploadRequest)
    {
        foreach (var item in createFileUploadRequest.Files)
        {
            FileUpload fileUpload = _mapper.Map<FileUpload>(createFileUploadRequest);
            fileUpload.FileName = item.FileName;
            fileUpload.FilePath = await _fileUploadAdapter.UploadFile(item);
            
            FileInfo fileInfo = new FileInfo(item.FileName);
            //  createFileUploadRequest.Extension = string.IsNullOrEmpty(createFileUploadRequest.Extension) ? fileInfo.Extension : createFileUploadRequest.Extension;
            await _fileUploadDal.AddAsync(fileUpload);
        }

        CreatedFileUploadResponse response = _mapper.Map<CreatedFileUploadResponse>(null);
        return response;
    }
    public async Task<DeletedFileUploadResponse> Delete(DeleteFileUploadRequest deleteFileUploadRequest)
    {
        FileUpload? fileUpload = await _fileUploadDal.GetAsync(f => f.Id == deleteFileUploadRequest.Id);

        await _fileUploadBusinessRules.FileUploadsShouldExistWhenSelected(fileUpload);
        await _fileUploadAdapter.DeleteFile(fileUpload.FilePath);
        await _fileUploadDal.DeleteAsync(fileUpload);
        DeletedFileUploadResponse deletedFileUploadResponse = _mapper.Map<DeletedFileUploadResponse>(fileUpload);
        return deletedFileUploadResponse;
    }

    public async Task<UpdatedFileUploadResponse> Update(UpdateFileUploadRequest updateFileUploadRequest)
    {
        FileUpload? fileUpload = await _fileUploadDal.GetAsync(predicate: f => f.Id == updateFileUploadRequest.Id);
        await _fileUploadBusinessRules.FileUploadsShouldExistWhenSelected(fileUpload);
        _mapper.Map(updateFileUploadRequest, fileUpload);
        // fileUpload = _mapper.Map<FileUpload>(updateFileUploadRequest);

        fileUpload.FilePath = await _fileUploadAdapter.UpdateFile(updateFileUploadRequest.File, fileUpload.FilePath);
        fileUpload.FileName = updateFileUploadRequest.File.FileName;

        await _fileUploadDal.UpdateAsync(fileUpload);
        UpdatedFileUploadResponse updatedFileUploadResponse = _mapper.Map<UpdatedFileUploadResponse>(fileUpload);
        return updatedFileUploadResponse;
    }

    public string Upload(IFormFile file, string extension = "")
    {
        FileInfo fileInfo = new(file.FileName);
        extension = string.IsNullOrEmpty(extension) ? fileInfo.Extension : extension;
        var fileName = fileInfo.Name + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-ff") + extension;
        var filePath = Environment.CurrentDirectory + @"\wwwroot\Users\" + fileName;
        using (FileStream fs = new(filePath, FileMode.Create))
        {
            file.CopyTo(fs);
        }
        return "Users/" + fileName;
    }
}
