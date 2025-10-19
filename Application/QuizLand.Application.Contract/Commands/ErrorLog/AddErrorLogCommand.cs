using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.ErrorLog;

public class AddErrorLogCommand : ICommand
{
    public string Message { get; init; } = default!;
    public string? Details { get; set; }
    public Exception? Exception { get; init; }
    public int? StatusCode { get; init; }
    public string? Path { get; init; }
    public string? Method { get; init; }
    public string? UserId { get; init; }
    public string? ClientIp { get; init; }
    public string? UserAgent { get; init; }
    public string? CorrelationId { get; init; }
    public string? RequestBody { get; init; }
    public string? ExtraJson { get; init; }
    public string Level { get; init; } = "Error";
}