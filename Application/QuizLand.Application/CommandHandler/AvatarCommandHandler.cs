using QuizLand.Application.Contract.Commands.Avatar;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class AvatarCommandHandler : ICommandHandler<AddAvatarCommand>,ICommandHandler<DeleteAvatarCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUploadFileService _uploadFileService;


    public AvatarCommandHandler(IUnitOfWork unitOfWork, IUploadFileService uploadFileService)
    {
        _unitOfWork = unitOfWork;
        _uploadFileService = uploadFileService;
    }
    public async Task<CommandResult> Handle(AddAvatarCommand command)
    {
        command.UploadFile.DirectoryName = "avatars";
        var path = await _uploadFileService.UploadFile(command.UploadFile);
        var avatar = path.AddAvatarFactory();
        await _unitOfWork.AvatarRepository.Add(avatar);
        await _unitOfWork.Save();
        return new CommandResult(){Id = avatar.Id};
    }

    public async Task<CommandResult> Handle(DeleteAvatarCommand command)
    {
        var avatar =  await _unitOfWork.AvatarRepository.GetById(command.Id);
        await _uploadFileService.DeleteFile(avatar.FilePath);
        await _unitOfWork.AvatarRepository.Delete(command.Id);
        await _unitOfWork.Save();
        return new CommandResult(){Id = avatar.Id};
    }
}