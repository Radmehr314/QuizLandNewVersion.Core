using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.User;

public class GetUserByPersonelCodeQuery : IQuery
{
    public string PersonelCode { get; set; }
}