using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Application.Mapper;

public static class RoundMapper
{
    public static Round RoundFactory(this Game game,Guid OwnerId)
    {
        return new Round()
        {
            GameId = game.Id,
            RoundNumber = 1,
            CourseId = null,
            SelectingGamerId = OwnerId,
            FirstAnswerGamerId = OwnerId,
            RoundStatus = RoundStatus.PendingCourse,
            CreateAt = DateTime.Now,
            StartedAt = null,
            CompletedAt = null,
            FirstRandQuestionId = null,
            SecondRandQuestionId = null,
            ThirdRandQuestionId = null
        };
    }
}