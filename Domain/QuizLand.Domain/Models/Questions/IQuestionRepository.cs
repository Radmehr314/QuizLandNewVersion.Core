namespace QuizLand.Domain.Models.Questions;

public interface IQuestionRepository
{
    Task Add(Question question);
    Task AddRange(List<Question> questions);
    Task<List<Question>> GetAllPagination(long courseid, int pageNumber, int size);
    Task<List<Question>> GetAll(long courseid);
    Task Delete(long id);
    Task<Question> GetById(long id);
    Task<long> Count();
    Task<bool> HasAtLeastAsync(long courseId,int countOfQuestion);
    Task<List<Question>> PickRandomAsync(long courseId,int countOfQuestion);
}