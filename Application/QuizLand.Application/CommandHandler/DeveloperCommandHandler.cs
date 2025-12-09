using QuizLand.Application.Contract.Commands.Developer;
using QuizLand.Application.Contract.Framework;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class DeveloperCommandHandler : ICommandHandler<DeleteAllGamesCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeveloperCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CommandResult> Handle(DeleteAllGamesCommand command)
    {
        await _unitOfWork.RoundRepository.DeleteAll();
        await _unitOfWork.Save();
        await _unitOfWork.GamerRepository.DeleteAll();
        await _unitOfWork.Save();
        await _unitOfWork.GameRepository.DeleteAll();
        await _unitOfWork.Save();
        return new CommandResult();

    }
}