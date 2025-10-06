using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Supporter;

public class GetSupporterByIdQuery : IQuery
{
    public Guid Id { get; set; }
}