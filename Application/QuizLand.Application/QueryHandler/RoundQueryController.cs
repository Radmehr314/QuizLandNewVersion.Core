using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Round;
using QuizLand.Application.Contract.QueryResults.Round;
using QuizLand.Application.Contract.QueryResults.RoundQuestion;
using QuizLand.Domain;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Application.QueryHandler;

public class RoundQueryController : IQueryHandler<GetRoundQuestionsQuery,GetRoundQuestionsQueryResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public RoundQueryController(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<GetRoundQuestionsQueryResult> Handle(GetRoundQuestionsQuery query)
    {
        var userId = _userInfoService.GetUserIdByToken();
        var caller = await _unitOfWork.GamerRepository.GetGamerByClientandGame(query.GameId,userId)
                     ?? throw new UserAccessException("شما عضو این بازی نیستید.");
        
        var round = await _unitOfWork.RoundRepository.GetByGameAndNumber(query.GameId, query.RoundNumber)
                    ?? throw new NotFoundException("راند یافت نشد.");
        
        if (round.RoundStatus == RoundStatus.PendingCourse || !round.CourseId.HasValue)
            throw new ValidationException("برای این راند هنوز درسی انتخاب نشده است.");

        var isP1Turn = round.RoundStatus == RoundStatus.AwaitingP1 && round.FirstAnswerGamerId == caller.Id;
        var isP2Turn = round.RoundStatus == RoundStatus.AwaitingP2 && round.FirstAnswerGamerId != caller.Id;
        
        var canSeeNow =
            isP1Turn || isP2Turn || round.RoundStatus == RoundStatus.Completed;
        
        if (!canSeeNow)
            throw new ValidationException("هنوز نوبت مشاهدهٔ سؤالات شما نیست.");
        
        var questions = await _unitOfWork.RoundQuestionRepository.LoadQuestionsAsync(round.Id); // 3 آیتم
        var vms = questions.OrderBy(x => x.QuestionNumber).Select(x => new GetAllRoundQuestionQueryResult
        {
            Id = x.Id,
            QuestionNumber = x.QuestionNumber,
            Text = x.Question.QuestionTitle,
            FirstOption = x.Question.FirstOption,
            SecondOption = x.Question.SecondOption,
            ThirdOption = x.Question.ThirdOption,
            CorrectOption = x.Question.CorrectOption,
            Source = x.Question.Source,
            DescriptiveAnswer = x.Question.DescriptiveAnswer,
            CountClickFirstOption = x.Question.CountClickFirstOption,
            CountClickSecondOption = x.Question.CountClickSecondOption,
            CountClickThirdOption = x.Question.CountClickThirdOption,
            CountClickFourthOption = x.Question.CountClickFourthOption,
        }).ToList();
        
        var isYourTurn = isP1Turn || isP2Turn;

        return new GetRoundQuestionsQueryResult
        {
            RoundId   = round.Id,
            CourseId  = round.CourseId,
            RoundNumber   = round.RoundNumber,
            IsYourTurn = isYourTurn,
            Questions = vms
        };
    }
}