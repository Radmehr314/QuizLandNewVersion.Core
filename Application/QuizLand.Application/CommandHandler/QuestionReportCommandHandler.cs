using QuizLand.Application.Contract.Commands.QuestionReport;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class QuestionReportCommandHandler : ICommandHandler<AddQuestionReportCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public QuestionReportCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<CommandResult> Handle(AddQuestionReportCommand command)
    {
        var data = command.Factory(_userInfoService.GetUserIdByToken());
        await _unitOfWork.QuestionReportRepository.Add(data);
        await _unitOfWork.Save();
        return new CommandResult()
        {
            Id = data.Id,
        };
    }
}