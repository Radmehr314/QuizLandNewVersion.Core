using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.DTOs.Upload;

namespace QuizLand.Application.Services;

public class UploadFileService : IUploadFileService
{
    public async Task<string> UploadFile(UploadFileDto uploadFileDto)
    {
        if (!System.IO.Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"{uploadFileDto.DirectoryName}")))
        {
            await Task.Run(() => System.IO.Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"{uploadFileDto.DirectoryName}")));
        }
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"{uploadFileDto.DirectoryName}", $"{uploadFileDto.FileName}{uploadFileDto.ExtensionFile}");
        var returnPath = Path.Combine($"\\{uploadFileDto.DirectoryName}", $"{uploadFileDto.FileName}{uploadFileDto.ExtensionFile}");
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await uploadFileDto.File.CopyToAsync(stream);
        }
        return returnPath;
    }

    public async Task DeleteFile(string path)
    {
        try
        {
            var p = "wwwroot" + "\\" + path;
            if (File.Exists(p))
            {
                await Task.Run(() => File.Delete(Path.Combine(p)));
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}