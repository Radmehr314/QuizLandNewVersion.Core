using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.CodeLog;

public class GetAllByPersonelCodeQuery : IQuery
{
    public string PersonelCode { get; set; }
}