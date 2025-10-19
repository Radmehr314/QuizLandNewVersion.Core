using QuizLand.Application.Contract.Commands.ErrorLog;
using QuizLand.Application.Contract.Framework;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class ErrorLogCommandHandler : ICommandHandler<AddErrorLogCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ErrorLogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Task<CommandResult> Handle(AddErrorLogCommand command)
    {
        throw new NotImplementedException();
    }
}