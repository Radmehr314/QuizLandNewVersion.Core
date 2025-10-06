using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.User;

public class AddUserCommand : ICommand
{
    public string PersonelCode { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string WorkArea { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public string PhoneNumber { get; set; }
}