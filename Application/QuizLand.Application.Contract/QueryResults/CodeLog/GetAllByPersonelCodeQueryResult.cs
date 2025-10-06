using QuizLand.Domain.Models.CodeLogs;

namespace QuizLand.Application.Contract.QueryResults.CodeLog;

public class GetAllByPersonelCodeQueryResult
{
    public string PersonelCdoe { get; set; }
    public string Otp { get; set; }
    public string Device { get; set; }
    public string State { get; set; }
    public DateTime SendedAt { get; set; }
    public bool IsActive { get; set; } = true;
}