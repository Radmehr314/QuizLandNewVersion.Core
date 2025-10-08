using QuizLand.Application.Contract.DTOs.Upload;

namespace QuizLand.Application.Contract.Contracts;

public interface IUploadFileService
{
    Task<string> UploadFile(UploadFileDto uploadFileDto);
    Task DeleteFile(string path);
}