namespace QuizLand.Domain.Models.CodeLogs;

public class CodeLogs : BaseEntity<long>
{
    public string Username { get; set; }
    public string Otp { get; set; }
    public string Device { get; set; }
    public State State { get; set; }
    public DateTime SendedAt { get; set; }
    public bool IsUsed { get; set; } = true;

}

public enum State
{
    Register = 1,
    Forgot = 2,
}