using QuizLand.Application.Contract.Commands.RoundQuestionAnswers;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Domain;
using QuizLand.Domain.Models.RandQuestionAnswers;
using QuizLand.Domain.Models.RandQuestions;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Application.CommandHandler;

public class RoundQuestionAnswerCommandHandler : ICommandHandler<SubmitRoundQuestionAnswersCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public RoundQuestionAnswerCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<CommandResult> Handle(SubmitRoundQuestionAnswersCommand command)
    {
        var userId = _userInfoService.GetUserIdByToken();
        var caller = await _unitOfWork.GamerRepository.GetGamerByClientandGame(command.GameId, userId)
                     ?? throw new UserAccessException("کاربر مجاز نیست.");
        var round = await _unitOfWork.RoundRepository.GetByGameAndNumber(command.GameId, command.RoundNumber)
                    ?? throw new NotFoundException("راند یافت نشد.");
        
        // گارد وضعیت/نوبت
        if (round.RoundStatus == RoundStatus.PendingCourse)
            throw new ValidationException("ابتدا درس این راند باید انتخاب شود.");
        if (round.RoundStatus == RoundStatus.AwaitingP1 && round.FirstAnswerGamerId != caller.Id)
            throw new ValidationException("نوبت پاسخ شما نیست.");
        if (round.RoundStatus == RoundStatus.AwaitingP2 && round.FirstAnswerGamerId == caller.Id)
            throw new ValidationException("نوبت حریف است.");
        if (round.RoundStatus == RoundStatus.Completed)
            throw new ValidationException("این راند کامل شده است.");
        
        
        
        
        
        // اعتبارسنجی 3 پاسخ
        if (command.Answers == null || command.Answers.Count != 3)
            throw new ValidationException("تعداد پاسخ‌ها باید ۳ باشد.");
        
        
        var rqMap = new[]
            {
                round.FirstRoundQuestion,
                round.SecondRoundQuestion,
                round.ThirdRoundQuestion
            }
            .Where(rq => rq != null)
            .ToDictionary(rq => rq!.Id, rq => rq!);
        
        
        if (!command.Answers.All(a => rqMap.ContainsKey(a.RoundQuestionId)))
            throw new ValidationException("پاسخ‌ها با سؤالات این راند هم‌خوان نیست.");
        
        if (await _unitOfWork.RoundQuestionAnswerRepository.AnswerExist(round.Id, caller.Id))
            throw new ValidationException("شما قبلاً پاسخ‌های این راند را ارسال کرده‌اید.");

        try
        {
            int correct = 0;
            var answerEntities = new List<RoundQuestionAnswer>(3);
            
            foreach (var a in command.Answers)
            {
                var rq = rqMap[a.RoundQuestionId];      // RoundQuestion همین راند
                var isCorrect = Evaluate(rq, a);        // تصحیح (rq.Question باید قبلاً include شده باشه اگر لازم داری)
                if (isCorrect) correct++;

                answerEntities.Add(new RoundQuestionAnswer
                {
                    RoundQuestionId = rq.Id,
                    GamerId = caller.Id,
                    SelectedOption = a.SelectedOption,
                    IsCorrect = isCorrect,
                    SubmitedAt = DateTime.Now
                });
            }
            
            await _unitOfWork.RoundQuestionAnswerRepository.AddRange(answerEntities);
            bool roundCompleted = false;
            bool gameCompleted  = false;
            int? nextRoundNo    = null;
            if (round.RoundStatus == RoundStatus.AwaitingP1)
            {
                round.RoundStatus = RoundStatus.AwaitingP2;
            }
            else
            {
                round.RoundStatus = RoundStatus.Completed;
                round.CompletedAt = DateTime.Now;
                roundCompleted = true;
                if (round.RoundNumber < 4)
                {
                    // ساخت راند بعد و تغییر SelectingGamer
                    var opponentId = await _unitOfWork.GamerRepository.GetOpponentId(command.GameId, caller.Id);
                    var next = new Round
                    {
                        GameId = round.GameId,
                        RoundNumber = round.RoundNumber + 1,
                        SelectingGamerId = opponentId,
                        RoundStatus = RoundStatus.PendingCourse,
                        CreateAt = DateTime.Now
                    };
                    await _unitOfWork.RoundRepository.Add(next);
                    nextRoundNo = next.RoundNumber;
                }
                else
                {
                    // راند ۴ تمام ⇒ پایان بازی
                    var (owner, guest) = await _unitOfWork.GamerRepository.GetPlayersAsync(command.GameId);
                    bool callerIsP1 = caller.IsOwner;          
                    Guid opponentId = callerIsP1 ? guest.Id : owner.Id;
                    Guid? winnerUserId = guest.UserId;
                    var game  = await _unitOfWork.GameRepository.GetGameById(command.GameId);
                    game.WinnerUserId = winnerUserId;
                    game.EndedAt = DateTime.Now;
                    gameCompleted = true;
                }
            }

            await _unitOfWork.Save();
            if (!gameCompleted)
                return new CommandResult() { Id = command.GameId };
            else
                return new CommandResult() {   };

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        throw new NotImplementedException();
    }
    
    private static bool Evaluate(RoundQuestion rq, RoundAnswerItem a)
    {

        return rq.Question.CorrectOption == a.SelectedOption;

    }


}