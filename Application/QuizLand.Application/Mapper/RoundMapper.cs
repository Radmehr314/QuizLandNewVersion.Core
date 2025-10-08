using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Application.Mapper;

public static class RoundMapper
{
    public static Round RoundFactory(this Game game,Gamer gamer)
    {
        return new Round()
        {
            Game = game,
            RoundNumber = 1,
            CourseId = null,
            Gamer = gamer,
            FirstAnswerGamer = gamer,
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