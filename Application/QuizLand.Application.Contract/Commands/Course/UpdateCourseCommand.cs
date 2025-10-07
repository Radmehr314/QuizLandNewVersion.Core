using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Course;

public class UpdateCourseCommand : ICommand
{
    public long Id { get; set; }
    public string Title { get; set; }
}