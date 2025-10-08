using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Game;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class CourseCommandHandler : ICommandHandler<AddCourseCommand>,ICommandHandler<UpdateCourseCommand>,ICommandHandler<DeleteCourseCommand>,ICommandHandler<PickCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public CourseCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
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

    public async Task<CommandResult> Handle(PickCourseCommand command)
    {
        var userId = _userInfoService.GetUserIdByToken();
        var caller = await _unitOfWork.GamerRepository.GetGamerByClientandGame(command.GameId,userId )
                     ?? throw new UserAccessException("کاربر مجاز نیست!!!");
        
        var round = await _unitOfWork.RoundRepository.GetByGameAndNumber(command.GameId, command.RoundNumber)
                    ?? throw new NotFoundException("راند یافت نشد.");
        
        var userid = _userInfoService.GetUserIdByToken();
        throw new NotImplementedException();

        
    }
    /*public Task<CommandResult> Handle(PickCourseCommand command)
    {
        throw new NotImplementedException();
    }*/
}