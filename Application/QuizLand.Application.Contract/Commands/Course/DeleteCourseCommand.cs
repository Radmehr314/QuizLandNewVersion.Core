using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Course;

public class DeleteCourseCommand : ICommand
{
    public long Id { get; set; }
}