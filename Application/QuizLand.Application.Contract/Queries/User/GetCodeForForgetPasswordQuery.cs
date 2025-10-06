using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.User;

public class GetCodeForForgetPasswordQuery : IQuery
{
    public string PhoneNumber { get; set; }
    public string Device { get; set; }

}