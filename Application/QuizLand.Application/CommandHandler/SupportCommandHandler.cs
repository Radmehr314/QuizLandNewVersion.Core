using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuizLand.Application.Contract.Commands.Supporter;
using QuizLand.Application.Contract.Commands.TicketMessage;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Application.CommandHandler;

public class SupportCommandHandler : ICommandHandler<AddSupporterCommand> , ICommandHandler<SupporterStatusCommand> 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;

    public SupportCommandHandler(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }

    public async Task<CommandResult> Handle(AddSupporterCommand command)
    {
        var exist = await _unitOfWork.SupporterRepository.UsernameExists(command.Username);
        if(exist) throw new ValidationException("کاربر تکراری است!!!");
        var pepper = _config["Security:Pepper"]
                     ?? throw new InvalidOperationException("Missing Security:Pepper");
        
        var (hashPassword, salt) = HashMaker.HashPassword(command.Password, pepper);
        
        var data = command.Factory(hashPassword, salt);
        await _unitOfWork.SupporterRepository.Add(data);
        await _unitOfWork.Save();
        return new CommandResult()
        {
            Id = data.Id
        };
        
    }

    public async Task<CommandResult> Handle(SupporterStatusCommand command)
    {
        var supporter = await _unitOfWork.SupporterRepository.GetById(command.Id);
        supporter.IsBan = !supporter.IsBan;
        await _unitOfWork.Save();
        return new CommandResult(){Id = supporter.Id};
    }


}