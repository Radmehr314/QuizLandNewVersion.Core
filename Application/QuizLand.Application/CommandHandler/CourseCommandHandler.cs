using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Game;
using QuizLand.Application.Mapper;
using QuizLand.Domain;
using QuizLand.Domain.Models.RandQuestions;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Application.CommandHandler;

public class CourseCommandHandler : ICommandHandler<AddCourseCommand>,ICommandHandler<UpdateCourseCommand>,ICommandHandler<DeleteCourseCommand>,ICommandHandler<PickCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public CourseCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<CommandResult> Handle(AddCourseCommand command)
    {
        var data = command.Factory();
        await _unitOfWork.CourseRepository.AddCourse(command.Factory());
        await _unitOfWork.Save();
        return new CommandResult() { Id = data.Id };
    }

    public async Task<CommandResult> Handle(UpdateCourseCommand command)
    {
        var course = await _unitOfWork.CourseRepository.GetById(command.Id);
        course.Title = command.Title;
        await _unitOfWork.Save();
        return new CommandResult() { Id = course.Id };
    }

    public async Task<CommandResult> Handle(DeleteCourseCommand command)
    {
        await _unitOfWork.CourseRepository.DeleteCourse(command.Id);
        await _unitOfWork.Save();
        return new CommandResult(){Id = command.Id};
    }

    public async Task<CommandResult> Handle(PickCourseCommand command)
    {
        var userId = _userInfoService.GetUserIdByToken();
        var caller = await _unitOfWork.GamerRepository.GetGamerByClientandGame(command.GameId,userId )
                     ?? throw new UserAccessException("کاربر مجاز نیست!!!");
        
        var round = await _unitOfWork.RoundRepository.GetByGameAndNumber(command.GameId, command.RoundNumber)
                    ?? throw new NotFoundException("راند یافت نشد.");
        
        var userid = _userInfoService.GetUserIdByToken();
        if (round.RoundStatus != RoundStatus.PendingCourse)
            throw new ValidationException("این راند در مرحله انتخاب درس نیست.");

        if (round.SelectingGamerId != caller.Id)
            throw new ValidationException("اجازه انتخاب درس این راند را ندارید.");
        
        if (round.CourseId.HasValue ||
            round.FirstRandQuestionId.HasValue ||
            round.SecondRandQuestionId.HasValue ||
            round.ThirdRandQuestionId.HasValue)
            throw new ValidationException("سؤالات این راند قبلاً ست شده‌اند.");
        
        var hasEnough = await _unitOfWork.QuestionRepository.HasAtLeastAsync(command.CourseId, 3);
        if (!hasEnough)
            throw new ValidationException("برای این درس کمتر از ۳ سؤال وجود دارد.");

        var picked = await _unitOfWork.QuestionRepository.PickRandomAsync(command.CourseId, 3);
        
        
        // 9) ساخت سه RoundQuestion برای همین راند با ترتیب 1..3
        var rq1 = new RoundQuestion() { RoundId = round.Id, QuestionId = picked[0].Id, QuestionNumber = 1 };
        var rq2 = new RoundQuestion { RoundId = round.Id, QuestionId = picked[1].Id, QuestionNumber = 2 };
        var rq3 = new RoundQuestion { RoundId = round.Id, QuestionId = picked[2].Id, QuestionNumber = 3 };
        await _unitOfWork.RoundQuestionRepository.Add(rq1);
        await _unitOfWork.RoundQuestionRepository.Add(rq2);
        await _unitOfWork.RoundQuestionRepository.Add(rq3);
        
        round.CourseId   = command.CourseId;
        round.RoundStatus = RoundStatus.AwaitingP1;
        round.StartedAt   = DateTime.Now;
        round.FirstRoundQuestion  = rq1;
        round.SecondRoundQuestion = rq2;
        round.ThirdRoundQuestion  = rq3;
        await _unitOfWork.Save();
        return new CommandResult { Id = round.Id };


        
    }
    /*public Task<CommandResult> Handle(PickCourseCommand command)
    {
        throw new NotImplementedException();
    }*/
}