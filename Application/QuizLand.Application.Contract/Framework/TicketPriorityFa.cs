using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Application.Contract.Framework;

public static class TicketPriorityFa
{
    public static string ToFa(this Priority p) => p switch
    {
        Priority.Low     => "کم‌اهمیت",
        Priority.Normal  => "عادی",
        Priority.High    => "مهم",
        Priority.Urgent  => "فوری",
        _                => "نامشخص"
    };
}