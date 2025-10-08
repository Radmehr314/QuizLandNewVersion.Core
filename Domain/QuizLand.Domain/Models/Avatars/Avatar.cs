using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.Avatars;

public class Avatar : BaseEntity<long>
{
    public string FilePath { get; set; }
    public IEnumerable<User> Users { get; set; }
}