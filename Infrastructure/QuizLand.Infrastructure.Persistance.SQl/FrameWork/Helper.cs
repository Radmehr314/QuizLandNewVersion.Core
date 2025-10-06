namespace QuizLand.Infrastructure.Persistance.SQl.FrameWork;

public class Helper
{
    public static int CalculateSkip(int pageNumber, int size) => (pageNumber - 1) * size;
}