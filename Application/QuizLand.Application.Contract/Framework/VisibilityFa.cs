using QuizLand.Domain.Models.TicketMessages;

namespace QuizLand.Application.Contract.Framework;

public static class VisibilityFa
{
    public static string ToFa(this Visibility p) => p switch
    {
        Visibility.Public => "عمومی",
        Visibility.InternalNote => "داخلی",
        _                => "نامشخص"
    };
}