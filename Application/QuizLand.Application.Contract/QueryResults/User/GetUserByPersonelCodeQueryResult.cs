namespace QuizLand.Application.Contract.QueryResults.User;

public class GetUserByPersonelCodeQueryResult
{
    public Guid Id { get; set; }
    public string PersonelCode { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string WorkArea { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsBan { get; set; }
}