using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.Friends;

public class Friend : BaseEntity<long>
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public User FirstUser { get; set; }
    public User SecondUser { get; set; }
}