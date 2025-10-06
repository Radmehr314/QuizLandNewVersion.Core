using QuizLand.Application.Contract.Framework;
using QuizLand.Domain.Models.TicketMessages;

namespace QuizLand.Application.Contract.Commands.TicketMessage;

public class SubmitNewMessageInTicketCommand : ICommand
{
    public long TicketId { get; set; }
    public string Content { get; set; }
    public bool IsSupporter { get; set; }
    public Visibility Visibility { get; set; }
    public long? ReplyTo { get; set; }
}