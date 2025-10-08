using QuizLand.Domain.Models.Rands;

namespace QuizLand.Application.Contract.QueryResults.Game;

public class GetGameStateQueryResult
{
    public int CurrentRound { get; set; }              // 1..4
    public RoundStatus RoundStatus { get; set; }       // PendingCourse/AwaitingP1/AwaitingP2/Completed
    public long SelectingGamerId { get; set; }         // نوبت انتخاب درس
    public long FirstAnswerGamerId { get; set; }       // نوبت پاسخ‌دهنده اول
    public bool IsYourTurnToPick { get; set; }         // برای کالر
    public bool IsYourTurnToAnswer { get; set; }       // برای کالر
}