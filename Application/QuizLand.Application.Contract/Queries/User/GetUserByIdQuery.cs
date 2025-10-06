using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.User;

public class GetUserByIdQuery : IQuery
{
    public Guid Id { get; set; }
}