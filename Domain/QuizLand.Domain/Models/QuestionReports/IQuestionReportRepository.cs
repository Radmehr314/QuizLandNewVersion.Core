namespace QuizLand.Domain.Models.QuestionReports;

public interface IQuestionReportRepository
{
    Task Add(QuestionReport questionReport);
    Task<List<QuestionReport>> GetAll();
    Task<List<QuestionReport>> GetAllBy(long id);
    
}