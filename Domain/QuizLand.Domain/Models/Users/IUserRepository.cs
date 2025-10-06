namespace QuizLand.Domain.Models.Users;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> GetById(Guid id);
    Task<List<User>> All();
    Task Delete(Guid id);
    Task<User> CheckUserByUsernameAndPassword(string username, string password);
    Task<User> GetByUsername(string username);
    Task<User> GetByPhoneNumber(string phoneNumber);
    Task<List<User>> AllPagination(int pageNumber, int size);
    Task<long> Count();
    Task<bool> UserExists(string username);
    Task<long> CountByOnlineUsers();
    
    
}