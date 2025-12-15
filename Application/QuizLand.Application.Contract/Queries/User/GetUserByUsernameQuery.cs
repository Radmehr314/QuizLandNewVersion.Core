using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.User;

public class GetUserByUsernameQuery : IQuery
{
    public string Username { get; set; }
}