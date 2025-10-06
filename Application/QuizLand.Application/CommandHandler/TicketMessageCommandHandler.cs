using System.Threading.Tasks;
using QuizLand.Application.Contract.Commands.TicketMessage;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class TicketMessageCommandHandler : ICommandHandler<SubmitTicketMessageCommand>, ICommandHandler<SubmitNewMessageInTicketCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public TicketMessageCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<CommandResult> Handle(SubmitTicketMessageCommand command)
    {
        var message = command.SendMessageMapper(_userInfoService.GetUserIdByToken());
        await _unitOfWork.TicketMessageRepository.Add(message);
        await _unitOfWork.Save();
        return new CommandResult() { Id = message.Id };
    }

    public async Task<CommandResult> Handle(SubmitNewMessageInTicketCommand command)
    {
        var message = command.SendMessageInTicketMapper(_userInfoService.GetUserIdByToken());
        await _unitOfWork.TicketMessageRepository.Add(message);
        await _unitOfWork.Save();
        return new CommandResult() { Id = message.Id };
    }
}