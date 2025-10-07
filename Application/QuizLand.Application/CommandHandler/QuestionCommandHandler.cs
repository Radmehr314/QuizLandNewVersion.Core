using QuizLand.Application.Contract.Commands.Question;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class QuestionCommandHandler : ICommandHandler<AddQuestionCommand>, ICommandHandler<UpdateQuestionCommand>, ICommandHandler<DeleteQuestionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CommandResult> Handle(AddQuestionCommand command)
    {
        var question = command.Factory();
        await _unitOfWork.QuestionRepository.Add(question);
        await _unitOfWork.Save();
        return new CommandResult(){Id = question.Id};
    }

    public async Task<CommandResult> Handle(UpdateQuestionCommand command)
    {
        var question =  await _unitOfWork.QuestionRepository.GetById(command.Id);
        question.QuestionTitle = command.QuestionTitle;
        question.FirstOption = command.FirstOption;
        question.SecondOption = command.SecondOption;
        question.ThirdOption = command.ThirdOption;
        question.FourthOption = command.FourthOption;
        question.CorrectOption = command.CorrectOption;
        question.CountClickFirstOption = command.CountClickFirstOption;
        question.CountClickSecondOption = command.CountClickSecondOption;
        question.CountClickThirdOption = command.CountClickThirdOption;
        question.CountClickFourthOption = command.CountClickFourthOption;
        question.DescriptiveAnswer = command.DescriptiveAnswer;
        question.Source = command.Source;
        question.CourseId = command.CourseId;
        await _unitOfWork.Save();
        return new CommandResult(){Id = question.Id};
    }

    public async Task<CommandResult> Handle(DeleteQuestionCommand command)
    {
        await _unitOfWork.QuestionRepository.Delete(command.Id);
        await _unitOfWork.Save();
        return new CommandResult(){Id = command.Id};
    }
}