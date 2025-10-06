using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Login;

public class ForgetPhoneNumberQuery : IQuery
{
    public string PersonelCode { get; set; }
}