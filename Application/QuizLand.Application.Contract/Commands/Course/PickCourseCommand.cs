using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Course;

public class PickCourseCommand : ICommand
{
    public Guid GameId { get; set; }
    public int RoundNumber { get; set; }
    public long CourseId { get; set; }
}