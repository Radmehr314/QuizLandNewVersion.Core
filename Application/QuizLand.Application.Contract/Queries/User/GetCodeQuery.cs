using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.User;

public class GetCodeQuery : IQuery
{
    public string PhoneNumber { get; set; }
    public string Username { get; set; }
    public string Device { get; set; }
}