namespace QuizLand.Domain.Models.Supporters;

public interface ISupporterRepository
{
    Task Add(Supporter supporter);
    Task<Supporter> GetById(Guid id);
    Task<Supporter> GetByUsername(string username);
    Task<List<Supporter>> All();
    Task<Supporter> CheckSupporterByUsernameAndPassword(string username, string password);
    Task<bool> UsernameExists(string username);

}