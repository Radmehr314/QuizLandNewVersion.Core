using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.CodeLog;

public class GetAllCodeLogByPersonalCodePaginationQuery : IQuery
{
    public string Username { get; set; }
    public int size { get; set; }
    public int pageNumber { get; set; }
}