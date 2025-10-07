using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class CourseCommandHandler : ICommandHandler<AddCourseCommand>,ICommandHandler<UpdateCourseCommand>,ICommandHandler<DeleteCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CommandResult> Handle(AddCourseCommand command)
    {
        var data = command.Factory();
        await _unitOfWork.CourseRepository.AddCourse(command.Factory());
        await _unitOfWork.Save();
        return new CommandResult() { Id = data.Id };
    }

    public async Task<CommandResult> Handle(UpdateCourseCommand command)
    {
        var course = await _unitOfWork.CourseRepository.GetById(command.Id);
        course.Title = command.Title;
        await _unitOfWork.Save();
        return new CommandResult() { Id = course.Id };
    }

    public async Task<CommandResult> Handle(DeleteCourseCommand command)
    {
        await _unitOfWork.CourseRepository.DeleteCourse(command.Id);
        await _unitOfWork.Save();
        return new CommandResult(){Id = command.Id};
    }
}