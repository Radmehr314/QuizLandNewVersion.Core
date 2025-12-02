namespace QuizLand.Application.Contract.DTOs;

public class LevelInfoDto
{
    public long TotalXp { get; set; }              // کل XP کاربر
    public int Level { get; set; }                // لول فعلی
    public long XpInCurrentLevel { get; set; }     // XP داخل لول فعلی
    public long XpNeedForNextLevel { get; set; }   // کل XP لازم برای رفتن به لول بعد
    public long RemainingForNextLevel { get; set; }// چقدر مونده تا لول بعد
}