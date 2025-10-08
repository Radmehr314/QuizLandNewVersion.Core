using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Round;

public class GetRoundQuestionsQuery: IQuery
{
    public Guid GameId { get; set; }
    public int  RoundNo { get; set; }
}