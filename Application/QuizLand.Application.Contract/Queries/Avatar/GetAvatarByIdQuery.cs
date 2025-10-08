using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Avatar;

public class GetAvatarByIdQuery : IQuery
{
    public long Id { get; set; }
}