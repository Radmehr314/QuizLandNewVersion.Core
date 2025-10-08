namespace QuizLand.Domain.Models.Avatars;

public interface IAvatarRepository
{
    Task Add(Avatar avatar);
    Task<Avatar> GetById(long id);
    Task<Avatar> GetFirst();
    Task Delete(long id);
    Task<List<Avatar>> GetAll();
    Task<List<Avatar>> GetAllPagination(int pageNumber, int size);
    Task<long> Count();
}