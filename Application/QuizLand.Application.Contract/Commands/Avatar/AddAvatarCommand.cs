using QuizLand.Application.Contract.DTOs.Upload;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Avatar;

public class AddAvatarCommand : ICommand
{
    public UploadFileDto UploadFile { get; set; }
}