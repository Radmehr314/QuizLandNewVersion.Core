using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.CodeLog;

public class GetAllCodeLogPaginationQuery : IQuery
{
    public int size { get; set; }
    public int pageNumber { get; set; }
}