namespace QuizLand.Application.Contract.QueryResults.Suporter;

public class GetAllSupporterQueryResult
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public bool IsBan { get; set; }
}