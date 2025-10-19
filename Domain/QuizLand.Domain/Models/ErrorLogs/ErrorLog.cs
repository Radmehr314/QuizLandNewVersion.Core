namespace QuizLand.Domain.Models.ErrorLogs;

public class ErrorLog : BaseEntity<long>
{
    public DateTime OccurredAtUtc { get; set; }
    public string Level { get; set; } = "Error"; // Error, Warning, Critical...
    public string Message { get; set; } = default!;
    public string? ExceptionType { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }          // لایه/سرویس
    public string? Path { get; set; }            // /api/...
    public string? Method { get; set; }          // GET/POST
    public int? StatusCode { get; set; }         // 400/500...
    public string? UserId { get; set; }          // Guid به string
    public string? ClientIp { get; set; }
    public string? UserAgent { get; set; }
    public string? CorrelationId { get; set; }   // traceId
    public string? RequestBody { get; set; }     // مراقب PII باش
    public string? Extra { get; set; }           // Json (route values, headers خلاصه)
    public string? Details { get; set; }
    
}