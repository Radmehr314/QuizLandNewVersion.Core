using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.Commands.Question;
using QuizLand.Application.Contract.QueryResults.Question;
using QuizLand.Domain.Models.Courses;
using QuizLand.Domain.Models.Questions;

namespace QuizLand.Application.Mapper;

public static class QuestionMapper
{
    public static Question Factory(this AddQuestionCommand command)
    {
        return new Question()
        {
            QuestionTitle = command.QuestionTitle,
            FirstOption = command.FirstOption,
            SecondOption = command.SecondOption,
            ThirdOption = command.ThirdOption,
            FourthOption = command.FourthOption,
            CorrectOption = command.CorrectOption,
            CountClickFirstOption = command.CountClickFirstOption,
            CountClickSecondOption = command.CountClickSecondOption,
            CountClickThirdOption = command.CountClickThirdOption,
            CountClickFourthOption = command.CountClickFourthOption,
            CourseId = command.CourseId,
            DescriptiveAnswer = command.DescriptiveAnswer,
            Source = command.Source
        };
    }
    
    
    public static GetByIdQuestionQueryResult GetByIdQuestionMapper(this Question query)
    {
        return new GetByIdQuestionQueryResult()
        {
            Id = query.Id,
            QuestionTitle = query.QuestionTitle,
            FirstOption = query.FirstOption,
            SecondOption = query.SecondOption,
            ThirdOption = query.ThirdOption,
            FourthOption = query.FourthOption,
            CorrectOption = query.CorrectOption,
            CountClickFirstOption = query.CountClickFirstOption,
            CountClickSecondOption = query.CountClickSecondOption,
            CountClickThirdOption = query.CountClickThirdOption,
            CountClickFourthOption = query.CountClickFourthOption,
            CourseId = query.CourseId,
            DescriptiveAnswer = query.DescriptiveAnswer,
            Source = query.Source,
            CourseName = query.Course.Title
        };
    }

    public static List<GetAllQuestionQueryResult> GetAllQuestionMapper(this List<Question> query)
    {
        return query.Select(f=>new GetAllQuestionQueryResult(){Id = f.Id, QuestionTitle = f.QuestionTitle, FirstOption = f.FirstOption,SecondOption = f.SecondOption,ThirdOption = f.ThirdOption,FourthOption = f.FourthOption,CorrectOption = f.CorrectOption,CountClickFirstOption = f.CountClickFirstOption,CountClickSecondOption = f.CountClickSecondOption,CountClickThirdOption = f.CountClickThirdOption,CountClickFourthOption = f.CountClickFourthOption,Source = f.Source,DescriptiveAnswer = f.DescriptiveAnswer}).ToList();
    }
    
    public static GetAllQuestionPaginationQueryResult GetAllQuestionPaginationMapper(this List<Question> questions, int pageSize,long count)
    {
        return new GetAllQuestionPaginationQueryResult()
        {
            AllQuestionPaginationQueryResults = questions.GetAllQuestionMapper(),
            PageSize = pageSize,
            Count = count
        };
    }
}