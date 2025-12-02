namespace QuizLand.Application.Contract.QueryResults.RoundQuestion;

public class GetAllRoundQuestionQueryResult
{
    public long Id { get; set; }
    public int  QuestionNumber { get; set; }      // 1..3
    public string Text { get; set; } = default!;
    public string FirstOption { get; set; }
    public string SecondOption { get; set; }
    public string ThirdOption { get; set; }
    public string FourthOption { get; set; }
    public int CorrectOption { get; set; } // 1 2 3 4
    public string Source { get; set; }
    public string DescriptiveAnswer { get; set; }
    public long? CountClickFirstOption { get; set; }
    public long? CountClickSecondOption { get; set; }
    public long? CountClickThirdOption { get; set; }
    public long? CountClickFourthOption { get; set; }
}