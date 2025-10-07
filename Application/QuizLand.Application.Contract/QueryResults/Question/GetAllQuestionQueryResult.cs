namespace QuizLand.Application.Contract.QueryResults.Question;

public class GetAllQuestionQueryResult
{
    public long Id { get; set; }
    public string QuestionTitle { get; set; }
    public string FirstOption { get; set; }
    public string SecondOption { get; set; }
    public string ThirdOption { get; set; }
    public string FourthOption { get; set; }
    public int CorrectOption { get; set; }
    public long? CountClickFirstOption { get; set; }
    public long? CountClickSecondOption { get; set; } 
    public long? CountClickThirdOption { get; set; } 
    public long? CountClickFourthOption { get; set; } 
    public string? DescriptiveAnswer { get; set; }
    public string Source { get; set; }
}