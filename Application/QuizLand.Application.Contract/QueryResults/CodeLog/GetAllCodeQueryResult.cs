using QuizLand.Domain.Models.CodeLogs;

namespace QuizLand.Application.Contract.QueryResults.CodeLog;

public class GetAllCodeQueryResult
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Otp { get; set; }
    public string Device { get; set; }
    public string State { get; set; }
    public DateTime SendedAt { get; set; }
    public string PersianSendedAt { get; set; }
    public bool IsUsed { get; set; } = true;
}