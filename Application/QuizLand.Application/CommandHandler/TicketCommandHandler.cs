using System.Threading.Tasks;
using QuizLand.Application.Contract.Commands.Ticket;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class TicketCommandHandler : ICommandHandler<SubmitNewTicketCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public TicketCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<CommandResult> Handle(SubmitNewTicketCommand command)
    {
     
        
        
        var userId = _userInfoService.GetUserIdByToken();
        var ticket = command.Factory(userId);                 
        var ticketMessage = command.Factory(ticket, userId);  

        await _unitOfWork.TicketRepository.Add(ticket);
        /*
        await _unitOfWork.TicketMessageRepository.Add(ticketMessage);
        */
        await _unitOfWork.Save();

        return new CommandResult { Id = ticket.Id };
    }
}