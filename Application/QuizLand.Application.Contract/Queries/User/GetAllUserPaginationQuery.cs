using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.User;

public class GetAllUserPaginationQuery : IQuery
{
    public int size { get; set; }
    public int pageNumber { get; set; }
}