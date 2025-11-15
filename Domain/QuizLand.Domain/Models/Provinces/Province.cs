using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.Provinces;

public class Province : BaseEntity<long>
{
    public string Title { get; set; }
    public IEnumerable<User> Users { get; set; }
}