namespace QuizLand.Application.Contract.QueryResults.Suporter;

public class GetSupporterByIdQueryResult
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public bool IsBan { get; set; }
}