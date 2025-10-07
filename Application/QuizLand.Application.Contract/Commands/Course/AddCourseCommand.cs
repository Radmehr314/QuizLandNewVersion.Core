using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Course;

public class AddCourseCommand : ICommand
{
    public string Title { get; set; }
}