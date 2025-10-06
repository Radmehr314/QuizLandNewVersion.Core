using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Application.Contract.Framework;

public static class TicketStatusFa
{
    public static string ToFa(this TicketStatus s) => s switch
    {
        TicketStatus.Open                => "در حال بررسی",
        TicketStatus.Pending             => "در انتظار پاسخ پشتیبان",
        TicketStatus.WaitingForCustomer  => "در انتظار پاسخ کاربر",
        TicketStatus.Resolved            => "حل شده",
        TicketStatus.Reopened            => "دوباره باز شده",
        TicketStatus.Rejected            => "رد شده",
        TicketStatus.Closed              => "بسته شده",
        _              => "نامشخص"
    };
}