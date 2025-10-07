namespace QuizLand.Domain.Models.Questions;

public interface IQuestionRepository
{
    Task Add(Question question);
    Task<List<Question>> GetAllPagination(long courseid, int pageNumber, int size);
    Task<List<Question>> GetAll(long courseid);
    Task Delete(long id);
    Task<Question> GetById(long id);
    Task<long> Count();
}